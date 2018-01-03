using System;
using System.Collections.Generic;
using System.Linq;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Organizations
{
    public class OrganizationManager : BaseManager<Organization>
    {
        public OrganizationManager(EmsDBContext db) : base(db)
        {
        }


        /// <summary>
        /// 获取全部同层级结点相关组织结构信息
        /// </summary>
        /// <param name="parentId">同层级结点的父节点id</param>
        /// <returns></returns>
        public List<Organization> GetHierarchyRelevantOrganizations(int? parentId)
        {
            return Finds(o => o.ParentId == parentId).ToList();
        }

        /// <summary>
        /// 迁移组织结构
        /// </summary>
        /// <param name="id">要迁移的组织结构id</param>
        /// <param name="parentId">要迁移到的组织结构id</param>
        public void Migrate(int id, int? parentId)
        {
            var organization = Find(id);

            if (parentId == organization.ParentId)
                throw new UserFriendlyException("无效的迁移操作！");

            var rootOrganization = Find(parentId);
            if (rootOrganization == null && parentId != null)
                throw new UserFriendlyException("您要移动到的部门不存在");

            var hierarchyRelevantOrganizations = GetHierarchyRelevantOrganizations(parentId);
            if (hierarchyRelevantOrganizations.Any(o => o.Name == organization.Name))
                throw new UserFriendlyException($"此分支中已存在 {organization.Name} 部门，请重新命名！");

            var originalPath = organization.Path;
            var childOrganizations = Finds(d => d.Path.StartsWith(originalPath)).ToList();
            organization.Path = rootOrganization?.Path + $"{organization.Id},";
            childOrganizations.ForEach(child => child.Path.Replace(originalPath, organization.Path));
            organization.ParentId = parentId;
            organization.LastModificationTime = DateTime.Now;

            Save();
        }

        public Organization Find(int? id)
        {
            var organization = FirstOrDefault(m => m.Id == id);
            return organization ?? throw new UserFriendlyException("您要移动的部门不存在");
        }

        /// <summary>
        /// 更新组织结构
        /// </summary>
        /// <param name="organization">要更新的组织结构对象</param>
        /// <returns></returns>
        public void Update(Organization organization)
        {
            var originalOrganization = Find(organization.Id);

            var hierarchyRelevantOrganizations = GetHierarchyRelevantOrganizations(originalOrganization.ParentId);
            if (hierarchyRelevantOrganizations.Any(o => o.Name == organization.Name))
                throw new UserFriendlyException($"此分支中已存在 {organization.Name} 部门，请重新命名！");

            originalOrganization.Name = organization.Name;
            originalOrganization.LastModificationTime = DateTime.Now;
            Save();
        }

        /// <summary>
        /// 删除组织结构
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var organization = Find(id);

            var childOrganizationCount = _db.Organization
                .Count(o => o.ParentId == organization.Id);
            if (childOrganizationCount > 0)
                throw new UserFriendlyException("您要删除的部门下存在子部门，不可删除！");
            var now = DateTime.Now;
            organization.Delete();
            var userOrganization = _db.UserOrganization.Where(m => m.OrganizationId == organization.Id);
            _db.RemoveRange(userOrganization);
            Save();
        }


        //创建组织结构节点
        public void Create(Organization organization)
        {
            if (string.IsNullOrWhiteSpace(organization.Name))
                throw new UserFriendlyException("部门名称不能为空");
            Organization parentOrganization = null;

            if (organization.ParentId != null)
            {
                parentOrganization = _db.Organization
                    .SingleOrDefault(o => o.Id == organization.ParentId);
                if (parentOrganization == null)
                    throw new UserFriendlyException("您设置的部门可能已被删除，添加新部门失败");
            }

            var hierarchyRelevantOrganizations = GetHierarchyRelevantOrganizations(organization.ParentId);
            if (hierarchyRelevantOrganizations.Any(o => o.Name == organization.Name))
                throw new UserFriendlyException($"此分支中已存在 {organization.Name} 部门，请重新命名！");

            using (var scope = _db.Database.BeginTransaction())
            {
                
                _db.Organization.Add(organization);
                Save();
                organization.Path = parentOrganization?.Path + $"{organization.Id},";
                Save();
                scope.Commit();
            }
        }
    }
}
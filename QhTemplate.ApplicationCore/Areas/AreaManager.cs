using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Areas
{
    public class AreaManager : BaseManager<Area>
    {
        public AreaManager(EmsDBContext db) : base(db)
        {
        }

        public List<Area> GetHierarchyRelevantAreas(int? parentId)
        {
            return Finds(o => o.ParentId == parentId).ToList();
        }

//        public void Migrate(int id, int parentId)
//        {
//            var area = Find(id);
//
//            if (parentId == area.ParentId)
//                throw new UserFriendlyException("无效的迁移操作！");
//
//            var rootOrganization = Find(parentId);
//            if (rootOrganization == null && parentId != 0)
//                throw new UserFriendlyException("您要移动到的地域不存在");
//
//            var hierarchyRelevantOrganizations = GetHierarchyRelevantAreas(parentId);
//            if (hierarchyRelevantOrganizations.Any(o => o.Name == area.Name))
//                throw new UserFriendlyException($"此分支中已存在 {area.Name} 地域，请重新命名！");
//
//            var originalPath = area.Path;
//            var childOrganizations = Finds(d => d.Path.StartsWith(originalPath)).ToList();
//            area.Path = rootOrganization?.Path + $"{area.Id},";
//            childOrganizations.ForEach(child => child.Path.Replace(originalPath, area.Path));
//            area.ParentId = parentId;
//            
//            Save();
//        }

        public Area Find(int? id)
        {
            var area = FirstOrDefault(m => m.Id == id);
            return area ?? throw new UserFriendlyException("您要移动的地域不存在");
        }

        public void Update(Organization organization)
        {
            var originalOrganization = Find(organization.Id);

            var hierarchyRelevantOrganizations = GetHierarchyRelevantAreas(originalOrganization.ParentId);
            if (hierarchyRelevantOrganizations.Any(o => o.Name == organization.Name))
                throw new UserFriendlyException($"此分支中已存在 {organization.Name} 部门，请重新命名！");

            originalOrganization.Name = organization.Name;
            Save();
        }

        /// <summary>
        /// 删除组织结构
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var area = Find(id);

            var childOrganizationCount = _db.Area
                .Count(o => o.ParentId == area.Id);
            if (childOrganizationCount > 0)
                throw new UserFriendlyException("您要删除的部门下存在子部门，不可删除！");

            var school = _db.SchoolArea.Where(m => m.Path.Contains(area.Id.ToString()));
            if (!school.Any())
            {
                throw  new UserFriendlyException("您要删除的地域下面有学校，不可删除");
            }

            _db.Area.Remove(area);
            Save();
        }


        //创建组织结构节点
        public void Create(Area area)
        {
            if (string.IsNullOrWhiteSpace(area.Name))
                throw new UserFriendlyException("部门名称不能为空");
            Organization parentOrganization = null;

            if (area.ParentId != 0)
            {
                parentOrganization = _db.Organization
                    .SingleOrDefault(o => o.Id == area.ParentId);
                if (parentOrganization == null)
                    throw new UserFriendlyException("您设置的部门可能已被删除，添加新部门失败");
            }

            var hierarchyRelevantOrganizations = GetHierarchyRelevantAreas(area.ParentId);
            if (hierarchyRelevantOrganizations.Any(o => o.Name == area.Name))
                throw new UserFriendlyException($"此分支中已存在 {area.Name} 部门，请重新命名！");

            using (var scope = _db.Database.BeginTransaction())
            {
                
                _db.Area.Add(area);
                Save();
                area.Path = parentOrganization?.Path + $"{area.Id},";
                Save();
                scope.Commit();
            }
        }
    }
}
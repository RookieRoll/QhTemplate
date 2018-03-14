using System;
using System.Collections.Generic;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Areas
{
    public interface IAreaAppService
    {
        /// <summary>
        /// 通过id查找组织结构
        /// </summary>
        /// <param name="orgId">组织结构Id</param>
        Area GetAreaById(int orgId);

        /// <summary>
        /// 通过用户id查找所在组织
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Area> GetAreasByUserId(int userId);

        /// <summary>
        /// 创建组织结构
        /// </summary>
        /// <param name="orgName">组织结构名</param>
        /// <param name="parentId">可选的组织结构父节点id</param>
        void CreateOrganization(string orgName, int? parentId);

        /// <summary>
        /// 删除组织结构
        /// </summary>
        /// <param name="orgId">要删除的组织结构id</param>
        /// <returns></returns>
        void DeleteOrganization(int orgId);

        /// <summary>
        /// 更新组织结构
        /// </summary>
        /// <param name="orgId">待更新的组织结构id</param>
        /// <param name="orgName">待更新的组织结构名字</param>
        void UpdateOrganization(int orgId, string orgName);

        /// <summary>
        /// 迁移组织结构
        /// </summary>
        /// <param name="orgId">要迁移的组织结构id</param>
        /// <param name="parentId">可选的要迁移到的组织结构id</param>
        /// <returns></returns>
        void MigrateOrganization(int orgId, int? parentId);

        /// <summary>
        /// 从指定部门获取人员
        /// </summary>
        /// <param name="orgId">指定组织结构的id</param>
        /// <returns></returns>
        IQueryable<User> GetUsersFromOrganization(int orgId);

        /// <summary>
        /// 添加人员到组织结构
        /// </summary>
        /// <param name="orgId">指定组织结构的id</param>
        /// <param name="usersId">所有指定人员的id</param>
        void AddUsersToOrganization(int orgId, int[] usersId);

        /// <summary>
        /// 从组织结构移除人员
        /// </summary>
        /// <param name="orgId">指定组织结构的id</param>
        /// <param name="userId">指定人员的id</param>
        void RemoveUsersToOrganization(int orgId, int userId);

        IQueryable<Organization> FindAll();
    }
}
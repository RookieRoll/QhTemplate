using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QhTemplate.AdminWeb.ViewModels.Organizations;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.ApplicationService.Organizations;
using QhTemplate.ApplicationService.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationAppService _organizationApp;

        private readonly IUserAppService _userApp;

        public OrganizationController(IOrganizationAppService organizationApp, IUserAppService userApp)
        {
            _organizationApp = organizationApp;
            _userApp = userApp;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetOrganization()
        {
            var allOrganizations = (from o in _organizationApp.FindAll()
                where !o.IsDeleted
                select new NodeViewModel()
                {
                    id = o.Id,
                    text = o.Name,
                    parentId = o.ParentId,
                    path = o.Path,
                }).ToList();
            var organizations = new List<NodeViewModel>();
            foreach (var org in allOrganizations)
            {
                org.state = new State(true, false, false);
                var userCount = _userApp.Finds().Include(m => m.UserOrganization)
                    .Count(n => n.UserOrganization.Any(d => d.OrganizationId == org.id));
                if (userCount > 0)
                    org.text += $" (<strong>{userCount}</strong>)";
                else
                    org.text += $" ({userCount})";
                if (org.parentId == null)
                {
                    organizations.Add(org);
                    continue;
                }

                var parentOrganization = allOrganizations.SingleOrDefault(m => m.id == org.parentId);
                parentOrganization.children.Add(org);
            }

            return Json(organizations);
        }

        #region 创建子组织结构

        [HttpGet]
        public IActionResult CreateOrganization(int? parentId)
        {
            var organization = new Organization();
            if (parentId != null)
                organization = _organizationApp.GetOrganizationById((int) parentId);

            var model = new OrganizationCreateChildViewModel()
            {
                ParentId = organization?.Id ?? 0,
                ParentName = organization.Name,
            };
            return PartialView("_Create", model);
        }

        [HttpPost]
        public IActionResult CreateOrganization(string orgName, int? parentId)
        {
            _organizationApp.CreateOrganization(orgName, parentId);
            return Json("添加成功！");
        }

        #endregion

        #region 删除组织结构

        [HttpGet]
        public IActionResult DeleteOrganization(int orgId, OrganizationDeleteViewModel model)
        {
            var organization = _organizationApp.GetOrganizationById(orgId);
            var userdata = _userApp.Finds()
                .Include(m => m.UserOrganization)
                .Where(n => n.UserOrganization
                    .Any(d => d.OrganizationId == orgId));
            var user = userdata.FirstOrDefault();
            var userCount = userdata.Count();
            model = new OrganizationDeleteViewModel()
            {
                Id = organization.Id,
                Name = organization.Name,
                UserName = user?.Name,
                UserCount = userCount,
            };
            return PartialView("_Delete", model);
        }


        [HttpPost]
        public IActionResult DeleteOrganization(int orgId)
        {
            _organizationApp.DeleteOrganization(orgId);
            return Json("删除成功！");
        }

        #endregion

        #region 重命名组织结构

        [HttpGet]
        public IActionResult RenameOrganization(int orgId)
        {
            var organization = _organizationApp.GetOrganizationById(orgId);
            var model = new OrganizationRenameViewModel()
            {
                Id = organization.Id,
                Name = organization.Name,
            };
            return PartialView("_Rename", model);
        }


        [HttpPost]
        public IActionResult RenameOrganization(int orgId, string orgName)
        {
            _organizationApp.UpdateOrganization(orgId, orgName);
            return Json("修改成功！");
        }

        #endregion

        #region 迁移组织结构

        [HttpGet]
        public IActionResult MigrateOrganization(int orgId, int? parentId, OrganizationMigrateViewModel model)
        {
            var organization = _organizationApp.GetOrganizationById(orgId);
            var parentOrganization = new Organization();
            if (parentId != null)
                parentOrganization = _organizationApp.GetOrganizationById((int) parentId);
            model = new OrganizationMigrateViewModel()
            {
                Id = organization.Id,
                ParentId = parentId,
                Name = organization.Name,
                ParentName = parentOrganization.Name,
            };
            return PartialView("_Migrate", model);
        }


        [HttpPost]
        public IActionResult MigrateOrganization(int orgId, int? parentId)
        {
            _organizationApp.MigrateOrganization(orgId, parentId);
            return Json("迁移成功！");
        }

        #endregion

        #region 添加人员到组织结构

        [HttpGet]
        public IActionResult AddUsersToOrganization(int orgId)
        {
            var organization = _organizationApp.GetOrganizationById(orgId);
            var model = new AddUsersToOrganizationViewModel()
            {
                Id = organization.Id,
                Name = organization.Name,
            };
            return PartialView("_AddUsersToOrganization", model);
        }


        [HttpPost]
        public IActionResult AddUsersToOrganization(int orgId, int[] usersId)
        {
            _organizationApp.AddUsersToOrganization(orgId, usersId);
            return Json("添加成功！");
        }

        #endregion

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <param name="orgId">组织结构id</param>
        /// <param name="request">datatables数据请求</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers(int orgId, IDataTablesRequest request)
        {
            var users = _userApp.Finds();
            var userCount = users.Count();
            if (userCount == 0)
            {
                var data = users.Select(m => new OrganizationUserViewModel());
                return new DataTablesJsonResult(DataTablesResponse.Create(request, 0, 0, data));
            }

            var sortColumn = request.Columns.FirstOrDefault(c => c.Sort != null);
            if (sortColumn != null)
            {
                string sort = sortColumn.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
                string orderStr = $"name {sort}";
                users = users.OrderBy(orderStr);
            }

            users = string.IsNullOrWhiteSpace(request.Search.Value)
                ? users
                : users.Where(item => item.Name.Contains(request.Search.Value));

            var searchCount = users.Count();
            var pagedData = users.Skip(request.Start).Take(request.Length).Select(m => new OrganizationUserViewModel
            {
                Id = m.Id,
                Name = $"{m.Name}({m.EmailAddress})",
            }).ToList();

            var selectedUsers = _organizationApp.GetUsersFromOrganization(orgId).ToList();
            foreach (var item in pagedData)
            {
                if (selectedUsers.Exists(u => u.Id == item.Id))
                    item.Checked = true;
            }

            var response = DataTablesResponse.Create(request, userCount, searchCount, pagedData);
            return new DataTablesJsonResult(response, true);
        }

        /// <summary>
        /// 获取某个组织结构的所有成员
        /// </summary>
        /// <param name="orgId">要查询的组织结构id</param>
        /// <param name="request">datatables数据请求</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsersFromOrganization(int? orgId, IDataTablesRequest request)
        {
            var users = new List<User>();
            var userCount = 0;
            var searchCount = 0;
            if (orgId != null)
            {
                var orgUser = _organizationApp.GetUsersFromOrganization((int) orgId);

                orgUser = String.IsNullOrWhiteSpace(request.Search.Value)
                    ? orgUser
                    : orgUser.Where(item => item.Name.Contains(request.Search.Value));
                searchCount = orgUser.Count();

                var sortColumn = request.Columns.FirstOrDefault(c => c.Sort != null);
                if (sortColumn != null)
                {
                    var sort = sortColumn.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
                    var orderStr = $"{sortColumn.Field} {sort}";
                    orgUser = orgUser.OrderBy(orderStr);
                }

                users = orgUser.ToList();
            }

            userCount = users.Count();
            var pagedData = users.Skip(request.Start).Take(request.Length).Select(m => new OrganizationUserViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Username = m.UserName,
                EmailAddress = m.EmailAddress,
                CreationTime = string.Format("{0:d}", m.CreationTime)
            }).ToList();
            var response = DataTablesResponse.Create(request, userCount, searchCount, pagedData);
            return new DataTablesJsonResult(response, true);
        }

        [HttpGet]
        public IActionResult RemoveUserFromOrganization(int orgId, int userId, OrganizationRemoveUserViewModel model)
        {
            var user = _userApp.Finds().SingleOrDefault(u => u.Id == userId && !u.IsDeleted);
            if (user == null)
                throw new UserFriendlyException("要移除的成员不存在!", HttpStatusCode.BadRequest);

            var organization = _organizationApp.GetOrganizationById(orgId);
            model = new OrganizationRemoveUserViewModel()
            {
                OrgId = orgId,
                UserId = userId,
                OrgName = organization.Name,
                UserName = user.Name,
            };
            return PartialView("_RemoveUserFromOrganization", model);
        }


        [HttpPost]
        public IActionResult RemoveUserFromOrganization(int orgId, int userId)
        {
            _organizationApp.RemoveUsersToOrganization(orgId, userId);
            return Json("移除成功！");
            ;
        }
    }
}
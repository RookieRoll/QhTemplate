using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.Extentions;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.AdminWeb.ViewModels.Roles;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.ApplicationService.Authentications;

namespace QhTemplate.AdminWeb.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRolesAppService _roleApp;

        private readonly PermissionManager _permissionManager;

        private readonly MenuProvider _menuProvider;

        public RoleController(IRolesAppService roleApp, PermissionManager permissionManager, MenuProvider menuProvider)
        {
            _roleApp = roleApp;
            _permissionManager = permissionManager;
            _menuProvider = menuProvider;
        }


        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRole(string roleName, bool isDefault, string[] permissionNames)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return Json("角色名不能为空");
            _roleApp.Create(roleName, isDefault, permissionNames);
            return Json("角色创建成功");
        }

        public IActionResult UpdateRole(int? roleId)
        {
            var role = _roleApp.Find(roleId);
            var model = new SetRoleViewModel
            {
                IsDefault = role.IsDefault,
                Name = role.Name,
                RoleId = role.Id
            };
            return PartialView("_Update", model);
        }

        [HttpPost]
        public IActionResult UpdateRole(int? roleId, string roleName, bool isDefault, string[] permissions)
        {
            _roleApp.Update(roleId, roleName, isDefault, permissions);
            var user = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var id = int.Parse(user);
            _menuProvider.ReloadMenu(id);
            return Json("修改成功");
        }

        public IActionResult RemoveRole(int? id)
        {
            var role = _roleApp.Find(id);
            RoleDeleteViewModel model = new RoleDeleteViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return PartialView("_Delete", model);
        }

        public IActionResult ConfiredRemove(int? id)
        {
            _roleApp.Remove(id);
            _menuProvider.ReloadMenu((int)id);
            return Json("删除成功");
        }
        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _roleApp.GetAll();

            var filteredData = String.IsNullOrWhiteSpace(request.Search.Value)
                ? data
                : data.Where(item => item.Name.Contains(request.Search.Value));

            var sortColumn = request.Columns.FirstOrDefault(c => c.Sort != null);
            if (sortColumn != null)
            {
                string ascending = sortColumn.Sort.Direction == SortDirection.Ascending ? "asc" : "desc";
                string orderStr = $"{sortColumn.Field} {ascending}";
                filteredData = filteredData.OrderBy(orderStr);
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length).Select(m => new RoleViewModel
            {
                Id = m.Id,
                Name = m.Name,
                IsDefault = m.IsDefault,
                IsStatic = m.IsStatic,
                CreationTime = string.Format("{0:d}", m.CreationTime)
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            return new DataTablesJsonResult(response, true);
        }

        public IActionResult GetPermissions(int? id = null)
        {
            var permissoinNodes = _permissionManager.GetRolePermissions(id).MapToPermissionTree();
            return Json(permissoinNodes);
        }
    }
}

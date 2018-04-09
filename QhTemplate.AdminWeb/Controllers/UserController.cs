using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.Extentions;
using QhTemplate.AdminWeb.Navigation;
using QhTemplate.AdminWeb.ViewModels.Users;
using QhTemplate.ApplicationService.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;

        private readonly MenuProvider _menuProvider;

        public UserController(IUserAppService userAppService, MenuProvider menuProvider)
        {
            _userAppService = userAppService;
            _menuProvider = menuProvider;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(UserEditViewModel model)
        {
            _userAppService.Create(model.Username,model.Name,model.EmailAddress,UserType.User);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var user = _userAppService.Find(id);
            var model = new UserEditViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.UserName,
                EmailAddress = user.EmailAddress
            };
            return PartialView("_Update", model);
        }

       [HttpPost]
        public IActionResult Update(UserEditViewModel model)
        {
            User user = new User
            {
                Id = model.Id,
                UserName = model.Username,
                Name = model.Name,
                EmailAddress = model.EmailAddress
            };
            _userAppService.Update(user);
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            User user = _userAppService.Find(id);
            UserDeleteViewModel result = new UserDeleteViewModel
            {
                Id = user.Id,
                Name = user.Name
            };
            return PartialView("_Delete", result);
        }

        [HttpPost]
        public IActionResult Delete(UserDeleteViewModel model)
        {
            _userAppService.Remove(model.Id);
            return RedirectToAction("Index");
        }

        #region 权限设置
        public IActionResult Authorize(int id)
        {
            return PartialView("_Authorize", id);
        }

        public IActionResult Permission(int id)
        {
            var result = _userAppService.GetAuthorize(id).MapToPermissionTree();
            return Json(result);
        }

        [HttpPost]
        public IActionResult Permission(int id, string[] permissions)
        {
            _userAppService.SetAuthorize(id, permissions);
            var user = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var ids = int.Parse(user);
            _menuProvider.ReloadMenu(ids);
            return Json("修改成功");
        }
        #endregion

        #region 权限设置部分
        public IActionResult Role(int id)
        {
            return PartialView("_Role", id);
        }

        public IActionResult RoleEdit(int id)
        {
            var roles = _userAppService.GetRolesByUserId(id);
            var result = _userAppService.GetRoles().MapToMultipleSelection(roles);
            return Json(result);
        }

        [HttpPost]
        public ActionResult RoleEdit(int id, int[] roles)
        {
            _userAppService.SetRoleByUserId(id,roles);
            var user = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var ids = int.Parse(user);
            _menuProvider.ReloadMenu(ids); 
            return Json("修改成功");
        }
        #endregion
        public IActionResult GetData(IDataTablesRequest request)
        {
            var data = _userAppService.Finds();


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
            
            var dataPage = filteredData.Skip(request.Start).Take(request.Length).Select(m => new UserViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Username = m.UserName,
                EmailAddress = m.EmailAddress,
                CreationTime = string.Format("{0:d}", m.CreationTime)
            });

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
            
            return new DataTablesJsonResult(response, true);
        }
    }
}
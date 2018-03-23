using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.AdminWeb.ViewModels.Areas;
using QhTemplate.AdminWeb.ViewModels.Organizations;
using QhTemplate.ApplicationService.Areas;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaAppService _areaApp;

        public AreaController(IAreaAppService areaApp)
        {
            _areaApp = areaApp;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAreas()
        {
            var areas = (from o in _areaApp.FindAll()
                select new NodeViewModel()
                {
                    id = o.Id,
                    text = o.Name,
                    parentId = o.ParentId,
                    path = o.Path,
                }).ToList();
            var organizations = new List<NodeViewModel>();
            foreach (var org in areas)
            {
                org.state = new State(true, false, false);
//                var userCount = _userApp.Finds().Include(m => m.UserOrganization)
//                    .Count(n => n.UserOrganization.Any(d => d.OrganizationId == org.id));
//                if (userCount > 0)
//                    org.text += $" (<strong>{userCount}</strong>)";
//                else
//                    org.text += $" ({userCount})";
                if (org.parentId == null)
                {
                    organizations.Add(org);
                    continue;
                }

                var parentOrganization = areas.SingleOrDefault(m => m.id == org.parentId);
                parentOrganization.children.Add(org);
            }

            return Json(organizations);
        }

        public IActionResult CreateArea(int parentId)
        {
            var organization = new Area();
            if (parentId != 0)
                organization = _areaApp.GetAreaById(parentId);

            var model = new AreasViewModel()
            {
                Id = organization?.Id ?? 0,
                Name = organization.Name,
                Code = organization.Code
            };
            return PartialView("_Create", model);
        }

        [HttpPost]
        public IActionResult CreateArea(string orgName, string code, int parentId)
        {
            _areaApp.CreateAreas(orgName, code, parentId);
            return Json("添加成功！");
        }

        public IActionResult UpdateArea(int id)
        {
            {
                var area = _areaApp.GetAreaById(id);
                return PartialView("_Update", AreasViewModel.ConvertAreasViewModel(area));
            }
        }

        public IActionResult UpdateArea(int id, string name, string code)
        {
            _areaApp.UpdateAreas(id, name, code);
            return Json("修改成功");
        }

        public IActionResult DeleteArea(int areaId)
        {
            var area = _areaApp.GetAreaById(areaId);
            return PartialView("_Delete", AreasViewModel.ConvertAreasViewModel(area));
        }

        [HttpPost]
        public IActionResult RemoveArea(int areaId)
        {
            _areaApp.DeleteAreas(areaId);
            return Json("删除成功");
        }

        public IActionResult GetAreaByParentId(int parentId)
        {
            var result = _areaApp.Finds(m => m.ParentId == parentId)
                .Select(m => AreasViewModel.ConvertAreasViewModel(m));
            return Json(result);
        }
    }
}
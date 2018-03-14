using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
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
            if (parentId != null)
                organization = _areaApp.GetAreaById((int) parentId);

            var model = new OrganizationCreateChildViewModel()
            {
                ParentId = organization?.Id ?? 0,
                ParentName = organization.Name,
            };
            return PartialView("_Create", model);
        }
        
        [HttpPost]
        public IActionResult CreateOrganization(string orgName, string code,int parentId)
        {
            _areaApp.CreateAreas(orgName,code,parentId);
            return Json("添加成功！");
        }
    }
}
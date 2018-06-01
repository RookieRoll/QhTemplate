using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QhTemplate.FontWeb.Models.ResumeInfo;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.ViewComponents
{
    public class ResumesViewComponent:ViewComponent
    {
        private readonly EmsDBContext _dbContext;

        public ResumesViewComponent(EmsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            int id = int.Parse(userId);
            var resumes = _dbContext.Resumes.Where(m => m.UserId == id).Select(m =>ResumeViewModel.ComvertToViewModel(m));
            return View("ResumeList", resumes);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IronPython.Runtime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class FileDownloadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly EmsDBContext _context;

        public FileDownloadController(IHostingEnvironment hostingEnvironment, EmsDBContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

      
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var id = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))
                ?.Value);
            var userId = int.Parse(id);

            #region 记录简历历史
            var filename = file.FileName.Replace(".pdf", "");
            var originResume = _context.Resumes.FirstOrDefault(m => m.UserId == userId && m.IsDefault);
            var resume = Resumes.Create(filename, userId);
            if (originResume == null)
            {
                resume.IsDefault = true;
            }
            _context.Resumes.Add(resume);
            _context.SaveChanges();
            #endregion

            var path = _hostingEnvironment.WebRootPath + "/upload/resumes/" + resume.RealName + ".pdf";
            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json("上传成功");
        }

        /// <summary>
        /// 前台用户下载简历链接
        /// </summary>
        /// <param name="id">简历Id</param>
        /// <returns></returns>
        public IActionResult DownloadFile(int id)
        {
            var resume = _context.Resumes.FirstOrDefault(m => m.Id == id) ?? 
                throw new UserFriendlyException("该简历不存在");

            var filename = resume.RealName+".pdf";
            var addrUrl = _hostingEnvironment.WebRootPath + "/upload/resumes/" +filename;
            var stream = System.IO.File.OpenRead(addrUrl);
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[".pdf"];
            var name = resume.Name+".pdf";
            return File(stream, memi, name);
        }
        /// <summary>
        /// 公司下载简历
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult CompanyDownloadFile(int id)
        {
            var relation = _context.FileRelation.FirstOrDefault(m => m.Id == id)??
                throw new UserFriendlyException("该简历不存在");
            var resume=_context.Resumes.FirstOrDefault(m=>m.Id.Equals(int.Parse(relation.RealName)))?? 
                throw new UserFriendlyException("该简历不存在");

            relation.Status= (int)ResumeStatus.Readed;

            _context.SaveChanges();

            var filename = resume.RealName + ".pdf";
            var addrUrl = _hostingEnvironment.WebRootPath + "/upload/resumes/" + filename;
            var stream = System.IO.File.OpenRead(addrUrl);
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[".pdf"];
            var name = relation.DisplayName+".pdf";
            return File(stream, memi, name);
        }
 
     
    }
}
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
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var id = await Task.FromResult(HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value);
            string fileExt = GetFileExt(file.FileName);
            var path = _hostingEnvironment.WebRootPath + "/upload/resumes/" + id + '.' + fileExt;
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
        /// 下载文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult DownLoad(string file)
        {
            var temp = file.Split("@");
            var company = int.Parse(temp[0]);
            var userId = int.Parse(temp[1]);
            var filename = temp[2];
            var real = _context.FileRelation.FirstOrDefault(m => m.CompanyId == company && m.UserId == userId && m.DisplayName.Equals(filename))?.RealName ??
                throw new UserFriendlyException("该文件不存在");

            var addrUrl = _hostingEnvironment.WebRootPath+ "/upload/resumes/" + real + ".pdf";

            var stream = System.IO.File.OpenRead(addrUrl);
           
            string fileExt = GetFileExt(file); //获取文件扩展名

            //获取文件的ContentType

            var provider = new FileExtensionContentTypeProvider();

            var memi = provider.Mappings[".pdf"];
            var name = filename + ".pdf";
            return File(stream, memi, name);
        }

        private string GetFileExt(string file)
        {
            return file.Split('.').LastOrDefault();
        }
    }
}
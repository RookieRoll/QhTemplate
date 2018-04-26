using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace QhTemplate.FontWeb.Controllers
{
    public class FileDownloadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileDownloadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FileSave(int companyId)

        {
            var date = Request;

            var files = Request.Form.Files;

            long size = files.Sum(f => f.Length);

            string webRootPath = _hostingEnvironment.WebRootPath;

            string contentRootPath = _hostingEnvironment.ContentRootPath;

            foreach (var formFile in files)

            {
                if (formFile.Length > 0)

                {
                    string fileExt = GetFileExt(formFile.FileName); //文件扩展名，不含“.”

                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名

                    var filePath = webRootPath + "/upload/" + newFileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))

                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new {count = files.Count, size});
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult DowLoad(string file)
        {
            var addrUrl = file;

            var stream = System.IO.File.OpenRead(addrUrl);

            string fileExt = GetFileExt(file); //获取文件扩展名

            //获取文件的ContentType

            var provider = new FileExtensionContentTypeProvider();

            var memi = provider.Mappings[fileExt];

            return File(stream, memi, Path.GetFileName(addrUrl));
        }

        private string GetFileExt(string file)
        {
            return file.Split('.').LastOrDefault();
        }
    }
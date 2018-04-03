using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IronPython.Hosting;
using IronPython.Runtime;
using QhTemplate.ApplicationService.Areas;
using QhTemplate.ApplicationService.Schools;

namespace QhTemplate.ApplicationService.Craws
{
    public class CrawApplicationService
    {
        private readonly string FilePath = "";
        private readonly IAreaAppService _areaAppService;
        private readonly ISchoolService _arSchoolService;
        private readonly string PixUrl = "";

        public CrawApplicationService(IAreaAppService areaAppService, ISchoolService arSchoolService)
        {
            _areaAppService = areaAppService;
            _arSchoolService = arSchoolService;
        }

        public void Run()
        {
            var pyEngine = Python.CreateEngine();
            dynamic py = pyEngine.ExecuteFile(FilePath);

            var areaList = _areaAppService.FindAll();
            var school = _arSchoolService.Finds();
            var resultList = new ConcurrentQueue<object>();
            Parallel.ForEach(areaList, m =>
            {
                Parallel.ForEach(school, n =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var url = string.Join('/', PixUrl, m.Code, n.Code, i);
                        List result =py.get_recruitment_talks(url);
                        resultList.Append(result);
                    }
                });
            });
            
            
        }
    }
}
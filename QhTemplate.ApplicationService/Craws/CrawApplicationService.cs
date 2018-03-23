using System.IO;
using IronPython.Hosting;
using QhTemplate.ApplicationService.Areas;
using QhTemplate.ApplicationService.Schools;

namespace QhTemplate.ApplicationService.Craws
{
    public class CrawApplicationService
    {
        private readonly string FilePath = "";
        private readonly IAreaAppService _areaAppService;
        private readonly ISchoolService _arSchoolService;

        public CrawApplicationService(IAreaAppService areaAppService, ISchoolService arSchoolService)
        {
            _areaAppService = areaAppService;
            _arSchoolService = arSchoolService;
        }

        public void Run()
        {
            var pyEngine = Python.CreateEngine();
            dynamic py = pyEngine.ExecuteFile(FilePath);
            var result = py.get_recruitment_talks("");
        }
    }
}
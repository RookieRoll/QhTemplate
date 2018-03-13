using System.IO;
using IronPython.Hosting;

namespace QhTemplate.ApplicationService.Craws
{
    public class CrawApplicationService
    {
        private readonly string FilePath = "";

        public void Run()
        {
            var pyEngine = Python.CreateEngine();
            dynamic py = pyEngine.ExecuteFile(FilePath);
            var result = py.get_recruitment_talks("");
        }
    }
}
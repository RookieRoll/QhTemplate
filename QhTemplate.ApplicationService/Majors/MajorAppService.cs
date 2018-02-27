using QhTemplate.ApplicationCore.Majors;

namespace QhTemplate.ApplicationService.Majors
{
    //TODO 未完成
    public class MajorAppService:IMajorAppService
    {
        private readonly MajorManager _majorManager;

        public MajorAppService(MajorManager majorManager)
        {
            _majorManager = majorManager;
        }
        
    }
}
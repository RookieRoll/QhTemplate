using System;
using System.Linq;
using System.Security.Principal;
using QhTemplate.ApplicationCore.BriefingContents;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.BriefingContents
{
    public class BriefingContentService:IBriefingContentService
    {
        private readonly BriefingContentManager _briefingContentManager;

        public BriefingContentService(BriefingContentManager briefingContentManager)
        {
            _briefingContentManager = briefingContentManager;
        }

        public void Create(string title, string content, string held,
            DateTime startTime, int schoolId)
        {
            _briefingContentManager.Create(title,content,held,startTime,schoolId);
        }

        public void Update(BriefingContent briefingContent)
        {
            _briefingContentManager.Update(briefingContent);
        }

        public void Delete(int id)
        {
            _briefingContentManager.Delete(id);
        }

        public BriefingContent Find(int id)
        {
            return _briefingContentManager.Find(id);
        }


        public IQueryable<BriefingContent> Finds()
        {
            return _briefingContentManager.Finds().AsQueryable();
        }

        public IQueryable<BriefingContent> Finds(Func<BriefingContent, bool> func)
        {
            return _briefingContentManager.Finds(func).AsQueryable();
        }

        public BriefingContent FirstOrDefault(Func<BriefingContent, bool> func)
        {
            return _briefingContentManager.FirstOrDefault(func);
        }

        public BriefingContent First(Func<BriefingContent, bool> func)
        {
            return _briefingContentManager.First(func);
        }
    }
}
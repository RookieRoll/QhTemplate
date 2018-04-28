using System;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.BriefingContents
{
    public interface IBriefingContentService
    {
        void Create(string title, string content, string held,
            DateTime startTime, int schoolId);

        void Update(BriefingContent briefingContent);
        void Delete(int id);
        BriefingContent Find(int id);
        IQueryable<BriefingContent> Finds();
        IQueryable<BriefingContent> Finds(Func<BriefingContent, bool> func);
        BriefingContent FirstOrDefault(Func<BriefingContent, bool> func);
        BriefingContent First(Func<BriefingContent, bool> func);
    }
}
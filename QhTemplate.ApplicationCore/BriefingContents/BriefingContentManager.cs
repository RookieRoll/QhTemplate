using System;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.BriefingContents
{
    public class BriefingContentManager:BaseManager<BriefingContent>
    {
        public BriefingContentManager(EmsDBContext db) : base(db)
        {
        }

        public BriefingContent Find(int id)
        {
            var briefingContent = FirstOrDefault(m => m.Id == id);
            return briefingContent ?? throw new UserFriendlyException("该宣讲会不存在");
        }

        public void Create(string title, string content, string held,
            DateTime startTime, int schoolId,string companyName)
        {
            var briefing = BriefingContent.Create(title,content,held,startTime,schoolId,companyName);
            _db.BriefingContent.Add(briefing);
            Save();
        }

        public void Update(BriefingContent briefingContent)
        {
            var briefing = Find(briefingContent.Id);
            briefing.Update(briefingContent.Title,briefingContent.Content,briefingContent.Held,briefingContent.StartTime);
            Save();
        }

        public void Delete(int id)
        {
            var briefing = Find(id);
            _db.BriefingContent.Remove(briefing);
            Save();
        }
    }
}

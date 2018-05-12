using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Resumes
{
    public class ResumeManager:BaseManager<FileRelation>
    {
        public ResumeManager(EmsDBContext db) : base(db)
        {
        }

   
    }
}
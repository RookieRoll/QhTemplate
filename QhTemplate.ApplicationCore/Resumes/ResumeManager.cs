using System.IO;
using System.Linq;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Resumes
{
    public class ResumeManager : BaseManager<FileRelation>
    {
        public ResumeManager(EmsDBContext db) : base(db)
        {
        }

        public FileRelation Find(int id)
        {
            return _db.FileRelation.FirstOrDefault(m => m.Id == id) ?? throw new UserFriendlyException("该投递记录不存在");
        }

        public void DeleteComfirm(int id)
        {
            var resume = Find(id);
            resume.IsDeleted = true;
            _db.SaveChanges();
        }
    }
}
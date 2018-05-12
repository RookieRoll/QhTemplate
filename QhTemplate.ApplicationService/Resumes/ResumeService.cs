using System;
using System.Linq;
using QhTemplate.ApplicationCore.Resumes;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Resumes
{
    public class ResumeService:IResumeService
    {

        private readonly ResumeManager _resume;

        public ResumeService(ResumeManager resume)
        {
            _resume = resume;
        }

        public IQueryable<FileRelation> Finds()
        {
            return _resume.Finds().AsQueryable();
        }

        public IQueryable<FileRelation> Finds(Func<FileRelation, bool> func)
        {
            return _resume.Finds(func).AsQueryable();
        }

        public FileRelation FirstOrDefault(Func<FileRelation, bool> func)
        {
            return _resume.FirstOrDefault(func);
        }

        public FileRelation First(Func<FileRelation, bool> func)
        {
            return _resume.First(func);
        }
    }
}
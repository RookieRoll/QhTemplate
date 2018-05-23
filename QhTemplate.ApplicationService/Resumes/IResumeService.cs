using System;
using System.IO;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Resumes
{
    public interface IResumeService
    {
        IQueryable<FileRelation> Finds();
        IQueryable<FileRelation> Finds(Func<FileRelation, bool> func);
        FileRelation FirstOrDefault(Func<FileRelation, bool> func);
        FileRelation First(Func<FileRelation, bool> func);

        FileRelation Find(int id);
        void DeleteComfirm(int id);
    }
}
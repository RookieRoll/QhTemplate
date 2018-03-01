using System;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Majors
{
    public interface IMajorAppService
    {
        IQueryable<Major> Finds();
        IQueryable<Major> Finds(Func<Major, bool> func);
        Major FirstOrDefault(Func<Major, bool> func);
        Major First(Func<Major, bool> func);
        Major Find(int id);
        void Create(string name, string code);
        void Remove(int id);
        void Update(Major area);
    }
}
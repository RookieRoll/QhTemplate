using System;
using System.Linq;
using QhTemplate.ApplicationCore.Majors;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

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

        public IQueryable<Major> Finds()
        {
            return _majorManager.Finds().AsQueryable();
        }

        public IQueryable<Major> Finds(Func<Major, bool> func)
        {
            return _majorManager.Finds(func).AsQueryable();
        }

        public Major FirstOrDefault(Func<Major, bool> func)
        {
            return _majorManager.FirstOrDefault(func);
        }

        public Major First(Func<Major, bool> func)
        {
            return _majorManager.First(func);
        }

        public Major Find(int id)
        {
            return _majorManager.Find(id);
        }

        public void Create(string name, string code)
        {
            var major = Major.CreateMajor(name, code);
            _majorManager.Create(major);
            
        }

        public void Remove(int id)
        {
            _majorManager.Delete(id);
        }

        public void Update(Major major)
        {
           _majorManager.Update(major);
        }
    }
}
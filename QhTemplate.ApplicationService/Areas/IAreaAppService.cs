using System;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Areas
{
    public interface IAreaAppService
    {
        IQueryable<Area> Finds();
        IQueryable<Area> Finds(Func<Area,bool> func);
        Area FirstOrDefault(Func<Area, bool> func);
        Area First(Func<Area, bool> func);
        Area Find(int id);
        void Create(string name, string code,int parentId);
        void Remove(int id);
        void Update(Area area);
    }
}
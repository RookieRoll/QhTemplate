using System;
using System.Linq;
using QhTemplate.ApplicationCore.Areas;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Areas
{
    public class AreaAppService : IAreaAppService
    {
        private readonly AreaManager _areaManager;

        public AreaAppService(AreaManager areaManager)
        {
            _areaManager = areaManager;
        }

        public IQueryable<Area> Finds()
        {
            return _areaManager.Finds().AsQueryable();
        }

        public IQueryable<Area> Finds(Func<Area, bool> func)
        {
            return _areaManager.Finds(func).AsQueryable();
        }

        public Area FirstOrDefault(Func<Area, bool> func)
        {
            return _areaManager.FirstOrDefault(func);
        }

        public Area First(Func<Area, bool> func)
        {
            return _areaManager.First(func);
        }

        public Area Find(int id)
        {
            return _areaManager.Find(id);
        }

        public void Create( string name, string code,int parentId)
        {
            var area = Area.CreateArea(name, code,parentId);
            _areaManager.Create(area);
        }

        public void Remove(int id)
        {
            _areaManager.Delete(id);
        }

        public void Update(Area user)
        {
           _areaManager.Update(user);
        }
    }
}
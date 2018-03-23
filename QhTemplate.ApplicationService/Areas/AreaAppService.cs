using System;
using System.Collections.Generic;
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


        public Area GetAreaById(int orgId)
        {
            return _areaManager.Find(orgId);
        }

        public void CreateAreas(string orgName,string code, int parentId)
        {
            var area = Area.CreateArea(orgName, code, parentId);
            _areaManager.Create(area);
        }

        public void DeleteAreas(int orgId)
        {
            _areaManager.Delete(orgId);
        }

        public void UpdateAreas(int orgId, string orgName,string code)
        {
            var area=new Area()
            {
                Id = orgId,
                Name = orgName,
                Code = code
            };
            _areaManager.Update(area);
        }


        public IQueryable<Area> FindAll()
        {
            return _areaManager.Finds().AsQueryable();
        }

        public IQueryable<Area> Finds(Func<Area, bool> func)
        {
            return _areaManager.Finds(func).AsQueryable();
        }
    }
}
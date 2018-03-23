using System;
using System.Collections.Generic;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Areas
{
    public interface IAreaAppService
    {
        Area GetAreaById(int orgId);
        void CreateAreas(string orgName,string code, int parentId);

        void DeleteAreas(int orgId);

        void UpdateAreas(int orgId, string orgName,string code);
        
        IQueryable<Area> FindAll();

        IQueryable<Area> Finds(Func<Area, bool> func);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Recruitments
{
    public interface IRecruitmentServcie
    {
        IQueryable<Recruitment> Finds();
        IQueryable<Recruitment> Finds(Func<Recruitment, bool> func);
        Recruitment FirstOrDefault(Func<Recruitment, bool> func);
        Recruitment First(Func<Recruitment, bool> func);
        Recruitment Find(int id);
        void Create(string title, string content, DateTime endTime, int companyid, List<int> ids,List<int> areaids);
        void Remove(int id);
        void Update(Recruitment area, List<int> list,List<int> areaids);
        IQueryable<AreaRecruit> GetAreaRecruits(int reId);
    }
}
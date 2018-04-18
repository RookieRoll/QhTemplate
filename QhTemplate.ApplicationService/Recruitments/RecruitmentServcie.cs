using System;
using System.Collections.Generic;
using System.Linq;
using QhTemplate.ApplicationCore.RecruitMents;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Recruitments
{
    public class RecruitmentServcie:IRecruitmentServcie
    {    private readonly EmsDBContext _db;
        private readonly RecruitmentManager _recruitment;

        public RecruitmentServcie(EmsDBContext db, RecruitmentManager recruitment)
        {
            _db = db;
            _recruitment = recruitment;
        }

        public IQueryable<Recruitment> Finds()
        {
            return _recruitment.Finds().AsQueryable();
        }

        public IQueryable<Recruitment> Finds(Func<Recruitment, bool> func)
        {
            return _recruitment.Finds(func).AsQueryable();
        }

        public Recruitment FirstOrDefault(Func<Recruitment, bool> func)
        {
            return _recruitment.FirstOrDefault(func);
        }

        public Recruitment First(Func<Recruitment, bool> func)
        {
            return _recruitment.First(func);
        }

        public Recruitment Find(int id)
        {
            return _recruitment.Find(id);
        }

        public void Create(string title, string content, DateTime endTime,int companyid,List<int> ids)
        {
            var recruitment = Recruitment.Create(title,content,endTime,companyid);
            var recruitmentBuilder=new RecruitmentBuilder(_recruitment,_db);
            recruitmentBuilder.CreateRecruitment(recruitment,ids);
        }

        public void Remove(int id)
        {
            _recruitment.Delete(id);
        }

        public void Update(Recruitment area,List<int> list)
        {
            var recruitmentBuilder=new RecruitmentBuilder(_recruitment,_db);
            recruitmentBuilder.UpDateRecruitMent(area,list);
        }
    }
}
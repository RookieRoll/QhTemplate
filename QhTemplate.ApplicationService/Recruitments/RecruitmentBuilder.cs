using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Community.CsharpSqlite;
using QhTemplate.ApplicationCore.RecruitMents;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Recruitments
{
    public class RecruitmentBuilder
    {
        private readonly EmsDBContext _db;
        private readonly RecruitmentManager _recruitment;

        public RecruitmentBuilder(RecruitmentManager recruitment, EmsDBContext db)
        {
            _recruitment = recruitment;
            _db = db;
        }

        public void CreateRecruitment(Recruitment recruitment,List<int> majorid)
        {
            using (var trans=_db.Database.BeginTransaction())
            {
                var id = CreateRecruitmentEntity(recruitment);
                BuildRelationOfMajor(id,majorid);
                trans.Commit();
            }
            
        }

        private int CreateRecruitmentEntity(Recruitment recruitment)
        {
            _recruitment.Create(recruitment);
            return recruitment.Id;
        }

        private void BuildRelationOfMajor(int Id,List<int> majorid)
        {
            List<MajorRecruitMent> list=new List<MajorRecruitMent>();
            
            majorid.ForEach(m =>
            {
                list.Add(new MajorRecruitMent()
                {
                    MajorId = m,
                    RecruitMentId = Id
                });
            });
            
            _db.MajorRecruitMent.AddRange(list);
            _db.SaveChanges();
        }

        public void UpDateRecruitMent(Recruitment recruitment, List<int> ids)
        {
            using (var strans=_db.Database.BeginTransaction())
            {
                _db.MajorRecruitMent.RemoveRange(_db.MajorRecruitMent.Where(m=>m.RecruitMentId==recruitment.Id));
                _recruitment.Update(recruitment);
                BuildRelationOfMajor(recruitment.Id,ids);
                strans.Commit();
            }
        }
    }
}
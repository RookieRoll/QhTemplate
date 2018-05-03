using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Linq;

namespace QhTemplate.ApplicationCore.RecruitMents
{
    public class RecruitmentManager : BaseManager<Recruitment>
    {
        public RecruitmentManager(EmsDBContext db) : base(db)
        {
        }

        public Recruitment Find(int id)
        {
            var recruitment = FirstOrDefault(m => m.Id == id);
            return recruitment ?? throw new UserFriendlyException("该招聘不存在");
        }

        public int Create(Recruitment recruitment)
        {
            _db.Recruitment.Add(recruitment);
            Save();
            return recruitment.Id;
        }

        public void Update(Recruitment recruitment)
        {
            var originRecruitment = Find(recruitment.Id);
            originRecruitment.Update(recruitment.Title, recruitment.Content, recruitment.EndTime);
            Save();
        }

        public void Delete(int id)
        {
            using (var scope = _db.Database.BeginTransaction())
            {
                try
                {
                    var origin = Find(id);
                    _db.Recruitment.Remove(origin);
                    var relations = _db.MajorRecruitMent.Where(m => m.RecruitMentId == origin.Id);
                    _db.MajorRecruitMent.RemoveRange(relations);
                    _db.AreaRecruit.RemoveRange(_db.AreaRecruit.Where(m => m.RecruitMentId == origin.Id));
                    Save();
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                }

            }

        }
    }
}
using System.Linq;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Majors
{
    public class MajorManager:BaseManager<Major>
    {
        public MajorManager(EmsDBContext db) : base(db)
        {
        }
        
        public void Create(Area obj)
        {
            if (IsAreaExit( obj.Name, obj.Code))
            {
                throw new UserFriendlyException("该学科已存在");
            }

            _db.Area.Add(obj);
            Save();
        }

        public Major Find(int id)
        {
            var major = FirstOrDefault(m => m.Id == id);
            return major ?? throw new UserFriendlyException("该学科不存在");
        }

        public void Update(Area obj)
        {
            var major = Find(obj.Id);
            if (IsAreaExit( obj.Name, obj.Code,obj.Id))
            {
                throw new UserFriendlyException("该学科已存在");
            }
            major.Update(obj.Name,obj.Code);
            Save();
        }

        public void Delete(int id)
        {
            var major = Find(id);
            _db.Major.Remove(major);
            Save();
        }

        private bool IsAreaExit( string name, string code,int? id=null)
        {
            return Finds(m => m.Id != id && m.Name.Equals(name) && m.Code.Equals(code)).Any();
        }
    }
}
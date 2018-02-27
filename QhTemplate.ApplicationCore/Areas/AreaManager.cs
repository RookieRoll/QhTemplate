using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Areas
{
    public class AreaManager : BaseManager<Area>
    {
        public AreaManager(EmsDBContext db) : base(db)
        {
        }

        public void Create(Area obj)
        {
            if (IsAreaExit(obj.ParentId, obj.Name, obj.Code))
            {
                throw new UserFriendlyException("该地名/code已经存在,请更换一个地名");
            }

            _db.Area.Add(obj);
            Save();
        }

        public Area Find(int id)
        {
            var area = FirstOrDefault(m => m.Id == id);
            return area ?? throw new UserFriendlyException("该地方不存在");
        }

        public void Update(Area obj)
        {
            var area = Find(obj.Id);
            if (IsAreaExit(area.ParentId, obj.Name, obj.Code,area.Id))
            {
                throw new UserFriendlyException("该地名/code已经存在,请更换一个地名");
            }
            area.Update(obj.Name, obj.Code);
            Save();
        }

        public void Delete(int id)
        {
            var area = Find(id);
            _db.Area.Remove(area);
            Save();
        }

        private bool IsAreaExit(int parentid, string name, string code, int? id=null)
        {
            return id == null
                ? Finds(m => m.ParentId == parentid && m.Name.Equals(name) && m.Code.Equals(code)).Any()
                : Finds(m => m.ParentId == parentid && m.Name.Equals(name) && m.Code.Equals(code) && m.Id != id).Any();
        }
    }
}
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Schools
{
    public class SchoolManagers:BaseManager<SchoolArea>
    {
        public SchoolManagers(EmsDBContext db) : base(db)
        {
        }

        public SchoolArea Find(int id)
        {
            var school = FirstOrDefault(m => m.Id == id);
            return school ?? throw new UserFriendlyException("该学校不存在");
        }

        public void Create(SchoolArea school)
        {
            _db.SchoolArea.Add(school);
            Save();
        }

        public void Update(SchoolArea school)
        {
            school.Update(school.Name,school.Code,school.Address);
            Save();
        }

        public void Migration(SchoolArea school)
        {
            school.Migration(school.AreaId,school.Path);
            Save();
        }

        public void Delete(int id)
        {
            var school = Find(id);
            _db.SchoolArea.Remove(school);
            Save();
        }
    }
}
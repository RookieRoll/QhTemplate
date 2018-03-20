using System;
using System.Linq;
using QhTemplate.ApplicationCore.Schools;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Schools
{
    public class SchoolService:ISchoolService
    {
        private readonly SchoolManagers _schoolManagers;

        public SchoolService(SchoolManagers schoolManagers)
        {
            _schoolManagers = schoolManagers;
        }

        public IQueryable<SchoolArea> Finds()
        {
            return _schoolManagers.Finds().AsQueryable();
        }

        public IQueryable<SchoolArea> Finds(Func<SchoolArea, bool> func)
        {
            return _schoolManagers.Finds(func).AsQueryable();
        }

        public SchoolArea FirstOrDefault(Func<SchoolArea, bool> func)
        {
            return _schoolManagers.FirstOrDefault(func);
        }

        public SchoolArea First(Func<SchoolArea, bool> func)
        {
            return _schoolManagers.First(func);
        }

        public SchoolArea Find(int id)
        {
            return _schoolManagers.Find(id);
        }

        public void Create(string name, string code, string path, string address,int areaid)
        {
            var school = SchoolArea.CreateNewArea(name, address, code, areaid, path);
            _schoolManagers.Create(school);
        }

        public void Remove(int id)
        {
            _schoolManagers.Delete(id);
        }

        public void Update(SchoolArea area)
        {
            _schoolManagers.Update(area);
        }

        public void Migration(int id, string path, int areaid)
        {
            var school = Find(id);
            school.Migration(areaid,path);
           _schoolManagers.Migration(school);
        }
    }
}
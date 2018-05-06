using System;
using System.Collections.Generic;
using System.Linq;
using IronPython.Runtime.Operations;
using QhTemplate.ApplicationCore.Areas;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.ApplicationCore.Schools;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Schools
{
    public class SchoolService : ISchoolService
    {
        private readonly SchoolManagers _schoolManagers;
        private readonly AreaManager _areaManager;
        private readonly EmsDBContext _db;
        
        public SchoolService(SchoolManagers schoolManagers, AreaManager areaManager, EmsDBContext db)
        {
            _schoolManagers = schoolManagers;
            _areaManager = areaManager;
            _db = db;
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

        public void Create(string name, string code, string address, int areaid)
        {
            var path = _areaManager.Find(areaid).Path;
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
            school.Migration(areaid, path);
            _schoolManagers.Migration(school);
        }

        public void AddTeachers(int schoolAreaId, int user)
        {
            _db.SchoolUser.Add(new SchoolUser
            {
                SchoolId = schoolAreaId,
                UserId = user
            });
            _db.SaveChanges();
        }
    }
}
using System;
using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Schools
{
    public interface ISchoolService
    {
        IQueryable<SchoolArea> Finds();
        IQueryable<SchoolArea> Finds(Func<SchoolArea, bool> func);
        SchoolArea FirstOrDefault(Func<SchoolArea, bool> func);
        SchoolArea First(Func<SchoolArea, bool> func);
        SchoolArea Find(int id);
        void Create(string name, string code,string path,string address,int areaid);
        void Remove(int id);
        void Update(SchoolArea area);
        void Migration(int id, string path, int areaid);
    }
}
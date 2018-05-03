using System;
using System.Linq;
using Microsoft.Scripting.Metadata;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Companys
{
    public interface ICompanyService
    {
        int Creat(string name, string address, string username, string telphone,string email);
        void Update(Company company);
        void Delete(int id);
        Company Find(int id);
        IQueryable<Company> Finds();
        IQueryable<Company> Finds(Func<Company, bool> func);
        Company FirstOrDefault(Func<Company, bool> func);
        Company First(Func<Company, bool> func);
        void SetCompanyUser(int companyId, int userId);
        
    }
}
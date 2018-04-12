using System;
using System.Linq;
using QhTemplate.ApplicationCore.Companys;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Companys
{
    public class CompanyService:ICompanyService
    {
        private readonly CompanyManager _companyManager;
        private readonly EmsDBContext _db; 
        public CompanyService(CompanyManager companyManager, EmsDBContext db)
        {
            _companyManager = companyManager;
            _db = db;
        }

        public int Creat(string name, string address, string username, string telphone)
        {
            var company = Company.Create(name, address, username, telphone);
            return _companyManager.Create(company);
        }

        public void Update(Company company)
        {
            _companyManager.Update(company);
        }

        public void Delete(int id)
        {
            _companyManager.Delete(id);
        }

        public Company Find(int id)
        {
            return _companyManager.Find(id);
        }

        public IQueryable<Company> Finds()
        {
            return _companyManager.Finds().AsQueryable();
        }

        public IQueryable<Company> Finds(Func<Company, bool> func)
        {
            return _companyManager.Finds(func).AsQueryable();
        }

        public Company FirstOrDefault(Func<Company, bool> func)
        {
            return _companyManager.FirstOrDefault(func);
        }

        public Company First(Func<Company, bool> func)
        {
            return _companyManager.First(func);
        }

        public void SetCompanyUser(int companyId, int userId)
        {
            _db.CompanyUser.Add(new CompanyUser
            {
                CompanyId = companyId,
                UserId = userId
            });
            _db.SaveChanges();
        }
    }
}
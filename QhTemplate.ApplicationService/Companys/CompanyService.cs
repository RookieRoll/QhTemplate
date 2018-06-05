using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using QhTemplate.ApplicationCore;
using QhTemplate.ApplicationCore.Companys;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Companys
{
    public class CompanyService : ICompanyService
    {
        private readonly CompanyManager _companyManager;
        private readonly EmsDBContext _db;
        private readonly IHostingEnvironment _hosting;

        public CompanyService(CompanyManager companyManager, EmsDBContext db, IHostingEnvironment hosting)
        {
            _companyManager = companyManager;
            _db = db;
            _hosting = hosting;
        }

        public int Creat(string name, string address, string username, string telphone, string email)
        {
            if (!IsCompanyExit(name, null))
            {
                throw new UserFriendlyException("该公司在系统中已经存在");
            }

            var company = Company.Create(name, address, username, telphone, email);
            var result = _companyManager.Create(company);
            //CreateFolder(result);
            return result;
        }

        private bool IsCompanyExit(string name, int? id)
        {
            if (id == null)
            {
                return _db.Company.FirstOrDefault(m => m.Name.Equals(name)) == null;
            }

            return _db.Company.FirstOrDefault(m => m.Name.Equals(name) && m.Id != id) == null;
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
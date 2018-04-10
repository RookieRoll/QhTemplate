using Microsoft.EntityFrameworkCore;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Companys
{
    public class CompanyManager : BaseManager<Company>
    {
        public CompanyManager(EmsDBContext db) : base(db)
        {
        }

        public void Create(Company company)
        {
            _db.Company.Add(company);
            Save();
        }

        public void Update(Company company)
        {
            var originCompany = Find(company.Id);
            originCompany.Update(company.Name,company.Address,company.Tellphone);
            Save();
        }

        public void Delete(int id)
        {
            var company = Find(id);
            _db.Company.Remove(company);
            Save();
        }
        
        public Company Find(int id)
        {
            var company = FirstOrDefault(m => m.Id == id);
            return company ?? throw new UserFriendlyException("该公司不存在");
        }
    }
}
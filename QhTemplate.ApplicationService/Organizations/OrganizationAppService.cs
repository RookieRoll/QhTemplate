using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.ApplicationCore.Organizations;
using QhTemplate.ApplicationCore.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Organizations
{
    public class OrganizationAppService : IOrganizationAppService
    {
        private readonly OrganizationManager _organizationManager;
        private readonly UserManager _userManager;
        private readonly EmsDBContext _db;

        public OrganizationAppService(OrganizationManager organizationManager, UserManager userManager, EmsDBContext db)
        {
            _organizationManager = organizationManager;
            _userManager = userManager;
            _db = db;
        }

        public Organization GetOrganizationById(int orgId)
        {
            var organization = _organizationManager.Find(orgId);
            return organization;
        }

        public IEnumerable<Organization> GetOrganizationsByUserId(int userId)
        {
            var user = _userManager.Find(userId);
            return _organizationManager.Finds(m => m.UserOrganization.Any(d => d.UserId == userId));
        }

        public void CreateOrganization(string orgName, int? parentId)
        {
            var organization = Organization.Create(orgName, parentId);
            _organizationManager.Create(organization);
        }

        public void DeleteOrganization(int orgId)
        {
            _organizationManager.Delete(orgId);
        }

        public void UpdateOrganization(int orgId, string orgName)
        {
            var organization = new Organization()
            {
                Id = orgId,
                Name = orgName
            };
            _organizationManager.Update(organization);
        }

        public void MigrateOrganization(int orgId, int? parentId)
        {
            _organizationManager.Migrate(orgId, parentId);
        }

        public IQueryable<User> GetUsersFromOrganization(int orgId)
        {
            var organization = _organizationManager.Find(orgId);
            var user = _userManager.Finds(m => m.UserOrganization.Any(n => n.OrganizationId == orgId)).AsQueryable();
            return user;
        }

        public void AddUsersToOrganization(int orgId, int[] usersId)
        {
            var organization = _organizationManager.Find(orgId);
            if (!usersId.Any())
            {
                var users = _userManager.Finds(u => usersId.Contains(u.Id)).ToList();
                users.ForEach(user =>
                {
                    _db.UserOrganization.Add(new UserOrganization
                    {
                        User = user,
                        UserId = user.Id,
                        Organization = organization,
                        OrganizationId = organization.Id
                    });
                });
            }

            _db.SaveChanges();
        }

        public void RemoveUsersToOrganization(int orgId, int userId)
        {
            var organization = _organizationManager.Find(orgId);
            var user = _userManager.Find(userId);
            if (user == null)
                throw new UserFriendlyException("您要操作的人员出现异常");
            else
            {
                var userOrganization = _db.UserOrganization.SingleOrDefault(m => m.UserId == userId);
                if (userOrganization != null)
                    _db.UserOrganization.Remove(userOrganization);
            }

            _db.SaveChanges();
        }

        public IQueryable<Organization> FindAll()
        {
            return _organizationManager.Finds().AsQueryable();
        }
    }
}
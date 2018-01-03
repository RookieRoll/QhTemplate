using System.Linq;
using QhTemplate.ApplicationCore.Authentications.Roles;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Authentications
{
    public class RoleAppService : IRolesAppService
    {
        private readonly RoleManager _roleManager;

        public RoleAppService(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public void Create(string roleName, bool isDefault, string[] permissionNames)
        {
            var role = Role.Create(roleName, isDefault);
            _roleManager.Create(role, permissionNames);
        }

        public void Update(int? roleId, string roleName, bool isDefault, string[] permissionNames)
        {
            _roleManager.Update(roleId, roleName, isDefault, permissionNames);
        }

        public IQueryable<Role> GetAll()
        {
            return _roleManager.Finds().AsQueryable();
        }

        public Role Find(int? id)
        {
            return _roleManager.Find(id);
        }

        public void Remove(int? id)
        {
            _roleManager.Delete(id);
        }
    }
}
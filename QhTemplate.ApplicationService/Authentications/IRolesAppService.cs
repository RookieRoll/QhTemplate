using System.Linq;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationService.Authentications
{
    public interface IRolesAppService
    {
        void Create(string roleName, bool isDefault, string[] permissionNames);
        void Update(int? roleId, string roleName, bool isDefault, string[] permissionNames);
        IQueryable<Role> GetAll();
        Role Find(int? id);
        void Remove(int? id);
    }
}
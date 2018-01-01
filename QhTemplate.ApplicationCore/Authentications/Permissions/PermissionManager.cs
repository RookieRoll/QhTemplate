using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Authentications.Permissions
{
    public class PermissionManager:BaseManager<Permission>
    {
        public PermissionManager(EmsDBContext db) : base(db)
        {
        }
    }
}
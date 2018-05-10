using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.ApplicationCore.Users
{
    public sealed class UserManager : BaseManager<User>
    {
        public UserManager(EmsDBContext db) : base(db)
        {
        }

        public User Find(int? id)
        {
            var user = FirstOrDefault(m => m.Id == id);
            return user ?? throw new UserFriendlyException("该用户不存在");
        }

        public int Create(User model)
        {
            _db.User.Add(model);
            Save();
            return model.Id;
        }

        public void Update(User model)
        {
            var user = Find(model.Id);
            user.Update(model.UserName, model.EmailAddress);
            Save();
        }

        public void Deleted(int? id)
        {
            var user = Find(id);
            user.Delete();
            Save();
        }

        private bool IsUserNameExit(string userName, int? id = null)
        {
            var user = FirstOrDefault(m => m.UserName.Equals(userName) && m.Id != id);
            return user != null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Models.Accounts
{
    public class AccountsViewModel
    {
        public string SchoolName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AccountSignUp
    {
        public string SchoolName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

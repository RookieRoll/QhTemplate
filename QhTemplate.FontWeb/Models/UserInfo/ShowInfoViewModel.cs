using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QhTemplate.FontWeb.Models.UserInfo
{
    public class ShowInfoViewModel
    {
        public bool HasResume { get; set; }
        public UserInfo UserInfos { get; set; }
        public ResumeInfo ResumeInfos { get; set; }
    }
}

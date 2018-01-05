using System.ComponentModel.DataAnnotations;

namespace QhTemplate.AdminWeb.ViewModels.Account
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        [Required(ErrorMessage = "校验码不能为空")]
        public string ValidateCode { get; set; }
    }
}
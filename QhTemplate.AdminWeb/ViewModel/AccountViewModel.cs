using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.KeyVault.Models;

namespace QhTemplate.AdminWeb.ViewModel
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
using System.ComponentModel.DataAnnotations;

namespace QhTemplate.AdminWeb.ViewModels.Users
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "用户姓名不能为空")]
        [MaxLength(32, ErrorMessage = "用户姓名长度不能大于32位")]
        public string Name { get; set; }

        [Display(Name = "用户名")]
        [Required(ErrorMessage = "用户姓名不能为空")]
        [MaxLength(32, ErrorMessage = "用户姓名长度不能大于32位")]
        [MinLength(2, ErrorMessage = "用户姓名长度不能小于2位")]
        public string Username { get; set; }

        [Display(Name = "邮箱地址")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string EmailAddress { get; set; }
    }
}
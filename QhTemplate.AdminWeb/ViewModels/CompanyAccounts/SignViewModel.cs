namespace QhTemplate.AdminWeb.ViewModels.CompanyAccounts
{
    public class SignViewModel
    {
        public int CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignUpViewModel
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyTelphone { get; set; }
    }
}
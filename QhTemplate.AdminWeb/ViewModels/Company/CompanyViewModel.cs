namespace QhTemplate.AdminWeb.ViewModels.Company
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalPerson { get; set; }
        public string TellPhone { get; set; }
        public string CreateTime { get; set; }
        public string Email { get; set; }
        public static CompanyViewModel ConvertCompanyViewModel(MysqlEntityFrameWorkCore.Models.Company model)
        {
            return new CompanyViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                LegalPerson = model.LegalPerson,
                TellPhone = model.Tellphone,
                CreateTime = model.CreateTime.ToString("yyyy-MM-dd"),
                Email=model.Email
            };
        }
    }
}
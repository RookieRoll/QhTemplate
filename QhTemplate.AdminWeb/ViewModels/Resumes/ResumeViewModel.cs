namespace QhTemplate.AdminWeb.ViewModels.Resumes
{
    public class ResumeViewModel
    {
        public int Id { get; set;}
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string SendTime { get; set; }
        public string FileName { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}
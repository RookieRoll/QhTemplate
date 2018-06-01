using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Models.ResumeInfo
{
    public class ResumeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string Time { get; set; }

        public static ResumeViewModel ComvertToViewModel(Resumes resumes)
        {
            return new ResumeViewModel
            {
                Id = resumes.Id,
                Name = resumes.Name,
                IsDefault = resumes.IsDefault,
                Time = resumes.CreationTime.ToString("yyyy-MM-dd HH:mm")
            };
        }
    }
}
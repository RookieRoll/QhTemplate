namespace QhTemplate.AdminWeb.ViewModels.Recruitments
{
    public class MajorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        
        public static MajorViewModel ConvertToViewModel(MysqlEntityFrameWorkCore.Models.Major major)
        {
            return  new MajorViewModel
            {
                Id = major.Id,
                Name = major.Name
            };
        }
    }
}
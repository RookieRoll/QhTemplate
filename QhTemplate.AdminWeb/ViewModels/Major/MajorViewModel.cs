namespace QhTemplate.AdminWeb.ViewModels.Major
{
    public class MajorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static MajorViewModel ConvertToViewModel(MysqlEntityFrameWorkCore.Models.Major major)
        {
            return  new MajorViewModel
            {
                Id = major.Id,
                Name = major.Name,
                Code = major.Code
            };
        }
    }
}
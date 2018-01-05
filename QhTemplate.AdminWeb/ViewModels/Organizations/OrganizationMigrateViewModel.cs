namespace QhTemplate.AdminWeb.ViewModels.Organizations
{
    public class OrganizationMigrateViewModel
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public int? ParentId { set; get; }

        public string ParentName { set; get; }
    }
}
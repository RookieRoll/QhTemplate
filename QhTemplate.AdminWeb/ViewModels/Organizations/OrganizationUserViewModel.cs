namespace QhTemplate.AdminWeb.ViewModels.Organizations
{
    public class OrganizationUserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string EmailAddress { get; set; }

        public string CreationTime { get; set; }

        public bool Checked { set; get; }

        public OrganizationUserViewModel()
        {
            Checked = false;
        }
    }
}
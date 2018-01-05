namespace QhTemplate.AdminWeb.ViewModels.Roles
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStatic { get; set; }
        public string CreationTime { get; set; }
        public bool IsDefault { get; set; }  
    }
}
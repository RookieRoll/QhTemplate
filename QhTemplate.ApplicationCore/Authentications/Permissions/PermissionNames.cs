namespace QhTemplate.ApplicationCore.Authentications.Permissions
{
    public class PermissionNames
    {
        public const string Total = "Total";
        public const string BaseMenu = Total + ".BaseMenu";

        #region 用户信息

        public const string User = BaseMenu + ".User";
        public const string User_Create = User + ".Create";
        public const string User_Edit = User + ".Edit";
        public const string User_Delete = User + ".Delete";
        public const string User_Permission = User + ".Permission";
        public const string User_Role = User + ".Role";
        
        #endregion

        #region 角色权限

        public const string Role = BaseMenu + ".Role";
        public const string Role_Create = Role + ".Create";
        public const string Role_Update = Role + ".Update";
        public const string Role_Delete = Role + ".Delete";
        #endregion

        #region 组织结构

        public const string Organization = BaseMenu + ".Organization";
        public const string Organization_Create = Organization + ".Create";
        public const string Organization_Delete = Organization + ".Delete";
        public const string Organization_Rename = Organization + ".Rename";
        public const string Organization_Migrate = Organization + ".Migrate";
        public const string Organization_AddUser = Organization + ".AddUser";
        public const string Organization_RemoveUser = Organization + ".RemoveUser";

        #endregion

        #region 地区学校管理
        public const string SchoolArea = BaseMenu + ".SchoolArea";
        public const string SchoolArea_AreaCreate = SchoolArea + ".AreateCreate";
        public const string SchoolArea_AreaDelete = SchoolArea + ".AreaDelete";
        public const string SchoolArea_SchoolCreate = SchoolArea + ".SchoolCreate";
        public const string SchoolArea_SchoolDelete = SchoolArea + ".SchoolDelete";
        #endregion
    }
}
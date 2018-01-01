﻿namespace QhTemplate.ApplicationCore.Authentications.Permissions
{
    public class PermissionNames
    {
        public const string Total = "Total";

        #region 用户信息

        public const string User = Total + ".User";

        #endregion

        #region 角色权限

        public const string Role = Total + ".Role";

        #endregion

        #region 组织结构

        public const string Organization = Total + ".Organization";
        public const string Organization_Create = Organization + ".Create";
        public const string Organization_Delete = Organization + ".Delete";
        public const string Organization_Rename = Organization + ".Rename";
        public const string Organization_Migrate = Organization + ".Migrate";
        public const string Organization_AddUser = Organization + ".AddUser";
        public const string Organization_RemoveUser = Organization + ".RemoveUser";

        #endregion
    }
}
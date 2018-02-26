/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/1/31 21:01:05                           */
/*==============================================================*/


drop table if exists AuditLog;

drop table if exists Organization;

drop table if exists Permission;

drop table if exists Role;

drop table if exists User;

drop table if exists UserOrganization;

drop table if exists UserRole;

/*==============================================================*/
/* Table: AuditLog                                              */
/*==============================================================*/
create table AuditLog
(
   Id                   int not null auto_increment,
   UserId               int,
   ServiceName          national varchar(256),
   MethodName           national varchar(256),
   Parameters           national varchar(1024),
   ExecutionTime        datetime not null,
   ExecutionDuration    int not null,
   ClientIpAddress      national varchar(64),
   ClientName           national varchar(128),
   BrowserInfo          national varchar(256),
   Exception            national varchar(2000),
   primary key (Id)
);

/*==============================================================*/
/* Table: Organization                                          */
/*==============================================================*/
create table Organization
(
   Id                   int not null auto_increment,
   ParentId             int,
   Name                 national varchar(64),
   Path                 national varchar(256),
   IsDeleted            bool not null,
   CreationTime         datetime not null,
   LastModificationTime datetime,
   DeletionTime         datetime,
   primary key (Id)
);

/*==============================================================*/
/* Table: Permission                                            */
/*==============================================================*/
create table Permission
(
   Id                   int not null auto_increment,
   RoleId               int,
   UserId               int,
   Code                 national varchar(256) not null,
   CreationTime         datetime not null,
   primary key (Id)
);

/*==============================================================*/
/* Table: Role                                                  */
/*==============================================================*/
create table Role
(
   Id                   int not null auto_increment,
   Name                 national varchar(32),
   IsStatic             bool not null,
   IsDefault            bool not null,
   IsDeleted            bool not null,
   CreationTime         datetime not null,
   LastModificationTime datetime,
   DeletionTime         datetime,
   primary key (Id)
);

/*==============================================================*/
/* Table: User                                                  */
/*==============================================================*/
create table User
(
   Id                   int not null auto_increment,
   Name                 national varchar(32),
   UserName             national varchar(32),
   Password             national varchar(32),
   EmailAddress         national varchar(256),
   IsDeleted            bool not null,
   CreationTime         datetime not null,
   LastModificationTime datetime,
   DeletionTime         datetime,
   primary key (Id)
);

/*==============================================================*/
/* Table: UserOrganization                                      */
/*==============================================================*/
create table UserOrganization
(
   UserId               int not null,
   OrganizationId       int not null,
   primary key (UserId, OrganizationId)
);

/*==============================================================*/
/* Table: UserRole                                              */
/*==============================================================*/
create table UserRole
(
   UserId               int not null,
   RoleId               int not null,
   primary key (UserId, RoleId)
);

alter table UserOrganization add constraint FK_Reference_3 foreign key (UserId)
      references User (Id);

alter table UserOrganization add constraint FK_Reference_4 foreign key (OrganizationId)
      references Organization (Id);

alter table UserRole add constraint FK_Reference_1 foreign key (UserId)
      references User (Id);

alter table UserRole add constraint FK_Reference_2 foreign key (RoleId)
      references Role (Id);

INSERT INTO `Organization`(`Id`, `ParentId`, `Name`, `Path`, `IsDeleted`, `CreationTime`, `LastModificationTime`, `DeletionTime`) VALUES (1, NULL, '本公司', '1,', 0, '2018-01-18 09:58:21', '2018-01-18 09:58:21', NULL);
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (101, 1, NULL, 'Total.BaseMenu.User', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (102, 1, NULL, 'Total.BaseMenu.Organization', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (103, 1, NULL, 'Total.BaseMenu.Organization.RemoveUser', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (104, 1, NULL, 'Total.BaseMenu.Organization.AddUser', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (105, 1, NULL, 'Total.BaseMenu.Organization.Migrate', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (106, 1, NULL, 'Total', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (107, 1, NULL, 'Total.BaseMenu.Organization.Delete', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (108, 1, NULL, 'Total.BaseMenu.Organization.Create', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (109, 1, NULL, 'Total.BaseMenu.Role', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (110, 1, NULL, 'Total.BaseMenu', '2018-01-17 22:16:11');
INSERT INTO `Permission`(`Id`, `RoleId`, `UserId`, `Code`, `CreationTime`) VALUES (111, 1, NULL, 'Total.BaseMenu.Organization.Rename', '2018-01-17 22:16:11');
INSERT INTO `Role`(`Id`, `Name`, `IsStatic`, `IsDefault`, `IsDeleted`, `CreationTime`, `LastModificationTime`, `DeletionTime`) VALUES (1, 'admin', 1, 1, 0, '2018-01-15 10:21:35', '2018-01-17 22:16:11', NULL);
INSERT INTO `Role`(`Id`, `Name`, `IsStatic`, `IsDefault`, `IsDeleted`, `CreationTime`, `LastModificationTime`, `DeletionTime`) VALUES (2, '学生测试', 0, 0, 0, '2018-01-17 21:50:20', '2018-01-18 09:57:48', '2018-01-17 21:50:44');
INSERT INTO `User`(`Id`, `Name`, `UserName`, `Password`, `EmailAddress`, `IsDeleted`, `CreationTime`, `LastModificationTime`, `DeletionTime`) VALUES (1, 'admin', 'admin', '123456', '28076465688@qq.com', 0, '2018-01-01 15:18:04', '2018-01-01 15:18:08', NULL);
INSERT INTO `UserRole`(`UserId`, `RoleId`) VALUES (1, 1);

/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/2/26 22:36:21                           */
/*==============================================================*/


drop table if exists Area;

drop table if exists AuditLog;

drop table if exists BriefingDetail;

drop table if exists Company;

drop table if exists Major;

drop table if exists Organization;

drop table if exists Permission;

drop table if exists Role;

drop table if exists SchoolArea;

drop table if exists User;

drop table if exists UserOrganization;

drop table if exists UserRole;

drop table if exists 学生信息;

drop table if exists 招聘岗位;

/*==============================================================*/
/* Table: Area                                                  */
/*==============================================================*/
create table Area
(
   Id                   int not null auto_increment,
   Name                 national varchar(255),
   Code                 varchar(255),
   ParentId             int,
   primary key (Id)
);

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
/* Table: BriefingDetail                                        */
/*==============================================================*/
create table BriefingDetail
(
   CompanyId            int,
   SchoolId             int,
   Address              national varchar(255),
   StartTime            datetime,
   PublishTime          datetime,
   CompanyName          national varchar(255),
   Body                 national char(1),
   OptionLink           national varchar(255),
   CityId               int,
   id                   int not null auto_increment,
   primary key (id)
);

/*==============================================================*/
/* Table: Company                                               */
/*==============================================================*/
create table Company
(
   UserId               int not null,
   CompanyName          varchar(255),
   CompanyAddress       varchar(255),
   CompanyOwner         varchar(255),
   TelPhone             varchar(255),
   Id                   int not null,
   primary key (Id)
);

/*==============================================================*/
/* Table: Major                                                 */
/*==============================================================*/
create table Major
(
   Id                   int not null auto_increment,
   Name                 national varchar(255),
   Code                 varchar(255),
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
/* Table: SchoolArea                                            */
/*==============================================================*/
create table SchoolArea
(
   AreaId               int not null,
   Name                 varchar(255),
   Address              varchar(255),
   Code                 varchar(255),
   Id                   int not null auto_increment,
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

/*==============================================================*/
/* Table: 学生信息                                                  */
/*==============================================================*/
create table 学生信息
(
   UserId               int,
   SchoolId             int not null,
   primary key ()
);

/*==============================================================*/
/* Table: 招聘岗位                                                  */
/*==============================================================*/
create table 招聘岗位
(
   CompanyId            int,
   majorId              int
);

alter table BriefingDetail add constraint FK_Reference_10 foreign key (CompanyId)
      references Company (Id);

alter table BriefingDetail add constraint FK_Reference_11 foreign key (SchoolId)
      references SchoolArea (Id);

alter table Company add constraint FK_Reference_5 foreign key (UserId)
      references User (Id);

alter table SchoolArea add constraint FK_Reference_7 foreign key (AreaId)
      references Area (Id);

alter table UserOrganization add constraint FK_Reference_3 foreign key (UserId)
      references User (Id);

alter table UserOrganization add constraint FK_Reference_4 foreign key (OrganizationId)
      references Organization (Id);

alter table UserRole add constraint FK_Reference_1 foreign key (UserId)
      references User (Id);

alter table UserRole add constraint FK_Reference_2 foreign key (RoleId)
      references Role (Id);

alter table 学生信息 add constraint FK_Reference_6 foreign key (UserId)
      references User (Id);

alter table 学生信息 add constraint FK_Reference_8 foreign key (SchoolId)
      references SchoolArea (Id);

alter table 招聘岗位 add constraint FK_Reference_12 foreign key (majorId)
      references Major (Id);

alter table 招聘岗位 add constraint FK_Reference_9 foreign key (CompanyId)
      references Company (Id);


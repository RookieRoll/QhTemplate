/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2018/2/26 22:36:21                           */
/*==============================================================*/


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
alter table SchoolArea add constraint FK_Reference_7 foreign key (AreaId)
      references Area (Id);
      
/*TODO*/

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


create table NewArticle
(
   Id                   int not null auto_increment,
   Title                national char(1),
   Content              national char(1),
   publishTime          datetime,
   IsDelete             bool,
   primary key (Id)
);




alter table BriefingDetail add constraint FK_Reference_10 foreign key (CompanyId)
      references Company (Id);

alter table BriefingDetail add constraint FK_Reference_11 foreign key (SchoolId)
      references SchoolArea (Id);

alter table Company add constraint FK_Reference_5 foreign key (UserId)
      references User (Id);




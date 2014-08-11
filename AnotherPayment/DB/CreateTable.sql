/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2014/8/5 3:08:06                             */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('AttachInfo')
            and   type = 'U')
   drop table AttachInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CouponCode')
            and   type = 'U')
   drop table CouponCode
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TelePhone')
            and   type = 'U')
   drop table TelePhone
go

if exists (select 1
            from  sysobjects
           where  id = object_id('UserInfo')
            and   type = 'U')
   drop table UserInfo
go

/*==============================================================*/
/* Table: AttachInfo                                            */
/*==============================================================*/
create table AttachInfo (
   id                   numeric              identity,
   AttachName           nvarchar(100)        null,
   AttachUrl            nvarchar(200)        null,
   UploadUser           nvarchar(20)         null,
   State                tinyint              null,
   constraint PK_ATTACHINFO primary key (id)
)
go

/*==============================================================*/
/* Table: CouponCode                                            */
/*==============================================================*/
create table CouponCode (
   id                   numeric              identity,
   CouponCode           nvarchar(20)         null,
   State                tinyint              null,
   constraint PK_COUPONCODE primary key (id)
)
go

/*==============================================================*/
/* Table: TelePhone                                             */
/*==============================================================*/
create table TelePhone (
   id                   numeric              identity,
   Phone                nvarchar(11)         null,
   state                tinyint              null,
   createtime			date				 null,
   constraint PK_TELEPHONE primary key (id)
)
go

/*==============================================================*/
/* Table: UserInfo                                              */
/*==============================================================*/
create table UserInfo (
   ID                   numeric              identity,
   UserName             nvarchar(20)         null,
   Password             nvarchar(20)         null,
   Coupon               nvarchar(20)         null,
   state                tinyint				 null,
   constraint PK_USERINFO primary key (ID)
)
go

INSERT INTO UserInfo VALUES ('sa','123',NULL,0);
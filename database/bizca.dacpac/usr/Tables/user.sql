create table [usr].[user]
(
	[userId]		     int identity(1,1) not null,
	[appUserId]		     varchar(10) not null,
	[email]			     varchar(50) not null,
	[phoneNumber]	     varchar(15) not null,
	[userCode]		     uniqueidentifier not null,
	[partnerId]		     smallint not null,
	[civilityId]	     smallint not null,
	[economicActivityId] smallint  null,
	[firstName]			 nvarchar(50) not null,
	[lastName]			 nvarchar(50) not null,
	[birthDate]			 date not null,
	[birthCountryId]	 smallint not null,
	[birthCity]			 varchar(50) not null,
	[creationDate]		 datetime2 not null,
    [lastUpdate]		 datetime2 not null
)
go

alter table [usr].[user] add constraint [pk_user] primary key clustered ( [userId] asc)
go

alter table [usr].[user] add constraint [df_user_creationDate] default getutcdate() for [creationDate]
go

alter table [usr].[user] add constraint [df_user_lastUpdate] default getutcdate() for [lastUpdate]
go

alter table [usr].[user] add constraint [fk_user_economicActivityId] foreign key ([economicActivityId]) references [ref].[economicActivity]([economicActivityId]) 
go

alter table [usr].[user] add constraint [fk_user_countryId] foreign key ([birthCountryId]) references [ref].[country]([countryId]) 
go

alter table [usr].[user] add constraint [fk_user_civilityId] foreign key ([civilityId]) references [ref].[civility]([civilityId]) 
go

create unique index [ix_partnerId_phoneNumber] on [usr].[user] ([partnerId], [phoneNumber])
go

create index [ix_economicActivity_economicActivityId] on [usr].[user] ([economicActivityId])
go

create unique index [ix_partnerId_appUserId] on [usr].[user] ([partnerId], [appUserId])
go

create unique index [ix_partnerId_email] on [usr].[user] ([partnerId], [email])
go

create index [ix_country_countryId] on [usr].[user] ([birthCountryId])
go

create index [ix_civility_civilityId] on [usr].[user] ([civilityId])
go

create unique index [ix_userCode] on [usr].[user] ([userCode])
go
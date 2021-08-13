create table [usr].[user]
(
	[userId]		        int identity(1,1) not null,
	[externalUserId]	    varchar(20) not null,
	[userCode]		        uniqueidentifier not null,
	[partnerId]		        smallint not null,
	[civilityId]	        smallint not null,
	[active]				bit not null,
	[economicActivityId]	smallint  null,
	[firstName]				nvarchar(100) not null,
	[lastName]			    nvarchar(100) not null,
	[birthDate]			    date null,
	[birthCountryId]	    smallint null,
	[birthCity]			    varchar(50) null,
	[creationDate]		    datetime2 not null,
    [lastUpdate]		    datetime2 not null,
	[rowversion]			[rowversion] not null
)
go

alter table [usr].[user] add constraint [pk_user] primary key clustered ( [userId] asc)
go

alter table [usr].[user] add constraint [df_user_creationDate] default getdate() for [creationDate]
go

alter table [usr].[user] add constraint [df_user_lastUpdate] default getdate() for [lastUpdate]
go

alter table [usr].[user] add constraint [df_user_active] default 0 for [active]
go

alter table [usr].[user] add constraint [fk_user_economicActivityId] foreign key ([economicActivityId]) references [ref].[economicActivity]([economicActivityId]) 
go

alter table [usr].[user] add constraint [fk_user_birthCountryId] foreign key ([birthCountryId]) references [ref].[country]([countryId]) 
go

alter table [usr].[user] add constraint [fk_user_civilityId] foreign key ([civilityId]) references [ref].[civility]([civilityId]) 
go

alter table [usr].[user] add constraint [fk_user_partnerId] foreign key ([partnerId]) references [ref].[partner] ([partnerId])
go

create unique index [ix_user_partnerId_externalUserId] on [usr].[user] ([partnerId], [externalUserId])
go

create index [ix_user_economicActivityId] on [usr].[user] ([economicActivityId])
go

create index [ix_user_birthCountryId] on [usr].[user] ([birthCountryId])
go

create unique index [ix_user_userCode] on [usr].[user] ([userCode])
go

create index [ix_user_civilityId] on [usr].[user] ([civilityId])
go

create index [ix_user_partnerId] on [usr].[user] ([partnerId])
go
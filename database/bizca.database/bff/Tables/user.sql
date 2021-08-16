create table [bff].[user]
(
	[userId]		            int identity(1,1) not null,
	[externalUserId]	        varchar(20) not null,
	[civilityId]	            smallint not null,
	[roleId]		            smallint not null,
	[firstName]				    nvarchar(100) not null,
	[lastName]			        nvarchar(100) not null,
	[phoneNumber]			    nvarchar(20) not null,
	[whatsapp]			        nvarchar(20) null,
	[email]						nvarchar(50) not null,
	[channelConfirmationStatus] smallint not null,
	[channelActivationStatus]   smallint not null,
	[creationDate]		        datetime2 not null,
    [lastUpdate]		        datetime2 not null,
	[rowversion]			    [rowversion] not null
)
go

alter table [bff].[user] add constraint [pk_user] primary key clustered ( [userId] asc)
go

alter table [bff].[user] add constraint [df_user_creationDate] default getdate() for [creationDate]
go

alter table [bff].[user] add constraint [df_user_lastUpdate] default getdate() for [lastUpdate]
go

alter table [bff].[user] add constraint [fk_user_civilityId] foreign key ([civilityId]) references [ref].[civility]([civilityId]) 
go

alter table [bff].[user] add constraint [fk_user_roleId] foreign key ([roleId]) references [bff].[role]([roleId]) 
go

create unique index [ix_user_externalUserId] on [bff].[user] ([externalUserId])
go

create unique index [ix_user_phoneNumber] on [bff].[user] ([phoneNumber])
go

create unique index [ix_user_email] on [bff].[user] ([email])
go

create index [ix_user_civilityId] on [bff].[user] ([civilityId])
go

create index [ix_user_roleId] on [bff].[user] ([roleId])
go


create table [usr].[password]
(
	[passwordId]     int identity(1,1) not null,
	[userId]	     int not null,
	[active]		 bit not null,
	[securityStamp]	 varchar(250) not null,
	[passwordHash]	 varchar(250) not null,
	[creationDate]	 datetime2 not null
)
go

alter table [usr].[password] add constraint [pk_password] primary key clustered ( [passwordId] )
go

alter table [usr].[password] add constraint [df_password_creationDate] default getdate() for [creationDate]
go

alter table [usr].[password] add constraint [df_password_active] default 0 for [active]
go

alter table [usr].[password] add constraint [fk_password_userId] foreign key ([userId]) references [usr].[user]([userId]) 
go

create unique index [ix_password_userId_active] on [usr].[password]( [userId], [active]) where [active] = 1
go

create index [ix_password_userId] on [usr].[password]( [userId])
go


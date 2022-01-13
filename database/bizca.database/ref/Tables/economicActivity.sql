create table [ref].[economicActivity]
(
	[economicActivityId]	smallint	not null,
	[economicActivityCode]	varchar(30)	not null,
	[description]			varchar(50) not null,
	[creationDate]			datetime2   not null,
    [lastUpdate]			datetime2   not null
)
go

alter table [ref].[economicActivity] add constraint [pk_economicActivity] primary key clustered ( [economicActivityId] asc)
go

alter table [ref].[economicActivity] add constraint [df_economicActivity_creationDate] default getdate() for [creationDate]
go

alter table [ref].[economicActivity] add constraint [df_economicActivity_lastUpdate] default getdate() for [lastUpdate]
go

create unique index [ix_economicActivity_economicActivityCode] on [ref].[economicActivity] ([economicActivityCode] asc)
go
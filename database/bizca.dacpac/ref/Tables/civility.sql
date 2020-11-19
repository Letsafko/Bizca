create table [ref].[civility]
(
	[civilityId]	smallint	not null,
	[civilityCode]	varchar(5)	not null,
	[creationDate]	datetime2   not null,
    [lastUpdate]	datetime2   not null
)
go

alter table [ref].[civility] add constraint [pk_civility] primary key clustered ( [civilityId] asc)
go

alter table [ref].[civility] add constraint [df_civility_creationDate] default getutcdate() for [creationDate]
go

alter table [ref].[civility] add constraint [df_civility_lastUpdate] default getutcdate() for [lastUpdate]
go

create unique index [ix_civilityCode] on [ref].[civility] ([civilityCode] asc)
go
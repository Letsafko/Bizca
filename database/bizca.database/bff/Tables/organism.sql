create table [bff].[organism]
(
	[organismId]	int not null,
	[codeInsee]		varchar(10) not null,
	[organismName]  varchar(100) not null,
	[organismHref]  varchar(100) not null,
	[creationDate]	datetime2 not null,
    [lastUpdate]	datetime2 not null
)
go

alter table [bff].[organism] add constraint [pk_organism] primary key clustered ( [organismId] asc)
go

alter table [bff].[organism] add constraint [df_organism_creationDate] default getdate() for [creationDate]
go

alter table [bff].[organism] add constraint [df_organism_lastUpdate] default getdate() for [lastUpdate]
go

create unique index [ix_organism_codeInsee] on [bff].[organism] ([codeInsee])
go


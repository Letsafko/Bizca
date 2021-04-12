create table [bff].[procedureType]
(
	[procedureTypeId]  smallint not null,
	[description]	   varchar(150) not null,
	[creationDate]	   datetime2 not null,
    [lastUpdate]	   datetime2 not null
)
go

alter table [bff].[procedureType] add constraint [pk_procedureType] primary key clustered ( [procedureTypeId] asc)
go 

alter table [bff].[procedureType] add constraint [df_procedureType_creationDate] default getutcdate() for [creationDate]
go

alter table [bff].[procedureType] add constraint [df_procedureType_lastUpdate] default getutcdate() for [lastUpdate]
go

create table [bff].[procedure]
(
	[procedureId]     int not null,
	[procedureTypeId] smallint not null,
	[organismId]	  int not null,
	[creationDate]	  datetime2 not null,
    [lastUpdate]	  datetime2 not null
)
go

alter table [bff].[procedure] add constraint [pk_procedure] primary key clustered ( [procedureId] asc)
go

alter table [bff].[procedure] add constraint [fk_procedure_procedureTypeId] foreign key([procedureTypeId]) references [bff].[procedureType]( [procedureTypeId] )
go

alter table [bff].[procedure] add constraint [fk_procedure_organismId] foreign key([organismId]) references [bff].[organism]( [organismId] )
go

alter table [bff].[procedure] add constraint [df_procedure_creationDate] default getutcdate() for [creationDate]
go

alter table [bff].[procedure] add constraint [df_procedure_lastUpdate] default getutcdate() for [lastUpdate]
go

create unique index [ix_procedure_procedureTypeId_organismId] on [bff].[procedure] ( [procedureTypeId], [organismId] )
go

create index [ix_procedure_procedureTypeId] on [bff].[procedure] ( [procedureTypeId] )
go

create index [ix_procedure_organismId] on [bff].[procedure] ( [organismId] )
go
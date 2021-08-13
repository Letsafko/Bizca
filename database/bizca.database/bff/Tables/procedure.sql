create table [bff].[procedure]
(
	[procedureTypeId] int not null,
	[organismId]	  int not null,
	[active]		  bit not null,
	[procedureHref]   varchar(200) not null,
	[creationDate]	  datetime2 not null,
    [lastUpdate]	  datetime2 not null
)
go

alter table [bff].[procedure] add constraint [pk_procedure] primary key clustered ( [procedureTypeId], [organismId])
go

alter table [bff].[procedure] add constraint [fk_procedure_procedureTypeId] foreign key([procedureTypeId]) references [bff].[procedureType]( [procedureTypeId] )
go

alter table [bff].[procedure] add constraint [fk_procedure_organismId] foreign key([organismId]) references [bff].[organism]( [organismId] )
go

alter table [bff].[procedure] add constraint [df_procedure_creationDate] default getdate() for [creationDate]
go

alter table [bff].[procedure] add constraint [df_procedure_lastUpdate] default getdate() for [lastUpdate]
go

create index [ix_procedure_procedureTypeId] on [bff].[procedure] ( [procedureTypeId] )
go

create index [ix_procedure_organismId] on [bff].[procedure] ( [organismId] )
go
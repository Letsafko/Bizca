create table [bff].[contactList]
(
    [listId]			smallint not null,
    [procedureTypeId]	int not null,
    [organismId]		int not null,
    [name]				smallint not null,
    [creationDate]		datetime2 not null
)
go

alter table [bff].[contactList] add constraint [pk_contactList] primary key clustered ( [listId] )
go

alter table [bff].[contactList] add constraint [df_contactList_creationDate] default getdate() for [creationDate]
go

alter table [bff].[contactList] add constraint [fk_contactList_procedureId_organismId]
    foreign key([procedureTypeId], [organismId])
    references [bff].[procedure]([procedureTypeId], [organismId])
go


create unique index [ix_contactList_procedureId_organismId] on [bff].[contactList] ([procedureTypeId], [organismId])
go
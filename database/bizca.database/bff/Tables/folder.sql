create table [bff].[folder]
(
    [
    folderId]
    smallint
    not
    null, [
    defaultListId]
    smallint
    not
    null, [
    partnerId]
    smallint
    not
    null, [
    name]
    varchar
(
    30
) not null,
    [creationDate] datetime2 not null
    )
    go

alter table [bff].[folder] add constraint [pk_folder] primary key clustered ( [folderId] )
    go

alter table [bff].[folder] add constraint [fk_folder_partnerId] foreign key ([partnerId]) references [ref].[partner]([partnerId])
    go

alter table [bff].[folder] add constraint [chk_folder_defaultListId] check ([defaultListId] > 0)
    go


alter table [bff].[folder] add constraint [df_folder_creationDate] default getdate() for [creationDate]
    go

create unique index [ix_folder_partnerId] on [bff].[folder] ([partnerId])
    go
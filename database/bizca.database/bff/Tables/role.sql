create table [bff].[role]
(
    [
    roleId]
    smallint
    not
    null, [
    description]
    varchar
(
    30
) not null,
    [creationDate] datetime2 not null,
    [lastUpdate] datetime2 not null
    )
    go

alter table [bff].[role] add constraint [pk_role] primary key clustered ( [roleId] asc)
    go

alter table [bff].[role] add constraint [df_role_creationDate] default getdate() for [creationDate]
    go

alter table [bff].[role] add constraint [df_role_lastUpdate] default getdate() for [lastUpdate]
    go

create table [ref].[language]
(
    [
    languageId]
    smallint
    not
    null, [
    languageCode]
    char
(
    2
) not null,
    [description] nvarchar
(
    20
) not null,
    [creationDate] datetime2 not null,
    [lastUpdate] datetime2 not null
    )
    go

alter table [ref].[language] add constraint [pk_language] primary key clustered ( [languageId] )
    go

alter table [ref].[language] add constraint [df_language_creationDate] default getdate() for [creationDate]
    go

alter table [ref].[language] add constraint [df_language_lastUpdate] default getdate() for [lastUpdate]
    go

create unique index [ix_language_languageCode] on [ref].[language]([languageCode])
    go
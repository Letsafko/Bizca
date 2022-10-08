create table [ref].[emailTemplateType]
(
    [
    emailTemplateTypeId]
    smallint
    not
    null, [
    descriptionFr]
    nvarchar
(
    100
) not null,
    [descriptionEn] nvarchar
(
    100
) not null,
    [creationDate] datetime2 not null,
    [lastUpdate] datetime2 not null
    )
    go

alter table [ref].[emailTemplateType] add constraint [pk_emailTemplateType]
    primary key clustered ( [emailTemplateTypeId] )
    go

alter table [ref].[emailTemplateType] add constraint [df_emailTemplateType_creationDate]
    default getdate() for [creationDate]
    go

alter table [ref].[emailTemplateType] add constraint [df_emailTemplateType_lastUpdate]
    default getdate() for [lastUpdate]
    go

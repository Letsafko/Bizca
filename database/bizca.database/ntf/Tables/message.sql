create table [ntf].[message]
(
    [
    messageId]
    int
    identity
(
    1,
    1
) not null,
    [userName] varchar
(
    20
) not null,
    [emailTemplateId] smallint not null,
    [phoneNumber] smallint not null,
    [email] smallint not null,
    [channelId] smallint not null,
    [content] varchar
(
    500
) not null,
    [creationDate] datetime2 not null
    )
    go

alter table [ntf].[message] add constraint pk_message primary key ([messageId])
    go

alter table [ntf].[message] add constraint fk_message_channelId foreign key ([channelId])
    references [ref].[channel]([channelId])
    go

alter table [ntf].[message] add constraint fk_message_emailTemplateId foreign key ([emailTemplateId])
    references [ref].[emailTemplate](emailTemplateId)
    go

alter table [ntf].[message] add constraint ck_message_phoneNumber_email
    check (([phoneNumber] is not null) or ([email] is not null))
    go

alter table [ntf].[message] add constraint [df_message_creationDate]
    default getdate() for [creationDate]
    go

create index [ix_message_channelId]
    on [ntf].[message] ([channelId] asc)
    go

create index [ix_message_emailTemplateId]
    on [ntf].[message] ([emailTemplateId] asc)
    go




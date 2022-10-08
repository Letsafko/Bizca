create table [bff].[subscription]
(
    [
    subscriptionId]
    int
    identity
(
    1,
    1
) not null,
    [subscriptionCode] uniqueidentifier not null,
    [subscriptionStatusId] smallint not null,
    [userId] int not null,
    [procedureTypeId] int not null,
    [organismId] int not null,
    [bundleId] smallint null,
    [amount] money null,
    [firstName] nvarchar
(
    100
) not null,
    [lastName] nvarchar
(
    100
) not null,
    [phoneNumber] nvarchar
(
    50
) not null,
    [whatsapp] nvarchar
(
    50
) null,
    [email] nvarchar
(
    50
) not null,
    [isFreeze] bit null,
    [whatsappCounter] int null,
    [totalWhatsapp] int null,
    [emailCounter] int null,
    [totalEmail] int null,
    [smsCounter] int null,
    [totalSms] int null,
    [activatedChannelMask] smallint not null,
    [confirmedChannelMask] smallint not null,
    [beginDate] datetime2 null,
    [endDate] datetime2 null,
    [creationDate] datetime2 not null,
    [lastUpdate] datetime2 not null
    )
    go

alter table [bff].[subscription] add constraint [pk_subscription] primary key clustered ( [subscriptionId] asc)
    go

alter table [bff].[subscription] add constraint [fk_subscription_procedureTypeId_organismId] foreign key ([procedureTypeId], [organismId]) references [bff].[procedure]( [procedureTypeId], [organismId])
    go

alter table [bff].[subscription] add constraint [fk_subscription_subscriptionStatusId] foreign key ([subscriptionStatusId]) references [bff].[subscriptionStatus] ([subscriptionStatusId])
    go

alter table [bff].[subscription] add constraint [fk_subscription_bundleId] foreign key ([bundleId]) references [bff].[bundle]( [bundleId] )
    go

alter table [bff].[subscription] add constraint [fk_subscription_userId] foreign key ([userId]) references [bff].[user] ( [userId] )
    go

alter table [bff].[subscription] add constraint [df_subscription_creationDate] default getdate() for [creationDate]
    go

alter table [bff].[subscription] add constraint [df_subscription_lastUpdate] default getdate() for [lastUpdate]
    go

alter table [bff].[subscription] add constraint [chk_subscription_whatsappCounter] check ( [whatsappCounter] is null or [whatsappCounter] >= 0)
    go

alter table [bff].[subscription] add constraint [chk_subscription_totalWhatsapp] check ( [totalWhatsapp] is null or [totalWhatsapp] > 0)
    go

alter table [bff].[subscription] add constraint [chk_subscription_emailCounter] check ( [emailCounter] is null or [emailCounter] >= 0)
    go

alter table [bff].[subscription] add constraint [chk_subscription_totalEmail] check ( [totalEmail] is null or [totalEmail] > 0)
    go

alter table [bff].[subscription] add constraint [chk_subscription_smsCounter] check ( [smsCounter] is null or [smsCounter] >= 0)
    go

alter table [bff].[subscription] add constraint [chk_subscription_totalSms] check ( [totalSms] is null or [totalSms] > 0)
    go

alter table [bff].[subscription] add constraint [chk_subscription_amount] check ( [amount] is null or [amount] > 0)
    go

create index [ix_subscription_procedureTypeId_organismId] on [bff].[subscription] ( [procedureTypeId], [organismId] )
    go

create index [ix_subscription_subscriptionStatusId] on [bff].[subscription] ([subscriptionStatusId])
    go

create unique index [ix_subscription_userId_organismId_procedureTypeId_subscriptionStatusId] on [bff].[subscription](
    [subscriptionStatusId]
    ,[procedureTypeId]
    ,[organismId]
    ,[userId]
    ) where [subscriptionStatusId] = 2
    go

create unique index [ix_subscription_subscriptionCode] on [bff].[subscription] ([subscriptionCode])
    go

create index [ix_subscription_bundleId] on [bff].[subscription] ([bundleId])
    go

create index [ix_subscription_userId] on [bff].[subscription]([userId])
    go
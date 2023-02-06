create table [bff].[bundle]
(
    [bundleId]          smallint not null,
    [bundleCode]	    varchar(20) not null,
    [priority]		    smallint not null,
    [amount]			money not null,
    [description]	    varchar(100) not null,
    [totalWhatsapp]     int null,
    [totalEmail]        int not null,
    [totalSms]          int not null,
    [intervalInWeeks]   smallint not null,
    [creationDate]	    datetime2 not null,
    [lastUpdate]	    datetime2 not null
    )
    go

alter table [bff].[bundle] add constraint [pk_bundle] primary key clustered ( [bundleId] )
    go

alter table [bff].[bundle] add constraint [df_bundle_creationDate] default getdate() for [creationDate]
    go

alter table [bff].[bundle] add constraint [df_bundle_lastUpdate] default getdate() for [lastUpdate]
    go

alter table [bff].[bundle] add constraint [chk_bundle_totalWhatsapp] check ( [totalWhatsapp] is null or [totalWhatsapp] > 0)
    go

alter table [bff].[bundle] add constraint [chk_bundle_intervalInWeeks] check ( [intervalInWeeks] > 0)
    go

alter table [bff].[bundle] add constraint [chk_bundle_totalEmail] check ( [totalEmail] > 0)
    go

alter table [bff].[bundle] add constraint [chk_bundle_totalSms] check ( [totalSms] > 0)
    go

alter table [bff].[bundle] add constraint [chk_bundle_priority] check ( [priority] > 0)
    go

alter table [bff].[bundle] add constraint [chk_bundle_amount] check ( [amount] > 0)
    go

create unique index [ix_bundle_bundleCode] on [bff].[bundle] ([bundleCode])
    go

create unique index [ix_bundle_priority] on [bff].[bundle] ([priority])
    go
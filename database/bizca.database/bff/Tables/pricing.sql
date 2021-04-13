create table [bff].[pricing]
(
	[pricingId]         smallint not null,
	[pricingCode]	    varchar(20) not null,
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

alter table [bff].[pricing] add constraint [pk_pricing] primary key clustered ( [pricingId] )
go

alter table [bff].[pricing] add constraint [df_pricing_creationDate] default getutcdate() for [creationDate]
go

alter table [bff].[pricing] add constraint [df_pricing_lastUpdate] default getutcdate() for [lastUpdate]
go

alter table [bff].[pricing] add constraint [chk_pricing_totalWhatsapp] check ( [totalWhatsapp] is null or [totalWhatsapp] > 0)
go

alter table [bff].[pricing] add constraint [chk_pricing_intervalInWeeks] check ( [intervalInWeeks] > 0)
go

alter table [bff].[pricing] add constraint [chk_pricing_totalEmail] check ( [totalEmail] > 0)
go

alter table [bff].[pricing] add constraint [chk_pricing_totalSms] check ( [totalSms] > 0)
go

alter table [bff].[pricing] add constraint [chk_pricing_priority] check ( [priority] > 0)
go

alter table [bff].[pricing] add constraint [chk_pricing_amount] check ( [amount] > 0)
go

create unique index [ix_pricing_pricingCode] on [bff].[pricing] ([pricingCode])
go

create unique index [ix_pricing_priority] on [bff].[pricing] ([priority])
go
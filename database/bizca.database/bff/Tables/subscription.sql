create table [bff].[subscription]
(
	[subscriptionId]		int identity(1,1) not null,
	[subscriptionStatusId]  smallint not null,
	[externalUserId]	    varchar(20) not null,
	[procedureId]			int not null,
	[pricingId]				smallint not null,
	[amount]				money not null,
	[firstName]		        nvarchar(100) not null,
	[lastName]		        nvarchar(100) not null,
	[phoneNumber]	        nvarchar(50) not null,
	[whatsapp]		        nvarchar(50) null,
	[email]			        nvarchar(50) not null,
	[whatsappCounter]       int null,
	[totalWhatsapp]         int null,
	[emailCounter]          int not null,
	[totalEmail]            int not null,
	[smsCounter]            int not null,
	[totalSms]              int not null,
	[activatedChannelMask]  smallint not null,
	[confirmedChannelMask]  smallint not null,
    [beginDate]	            datetime2 null,
    [endDate]               datetime2 null,
	[creationDate]	        datetime2 not null,
	[lastUpdate]	        datetime2 not null
)
go

alter table [bff].[subscription] add constraint [pk_subscription] primary key clustered ( [subscriptionId] asc)
go

alter table [bff].[subscription] add constraint [fk_subscription_subscriptionStatusId] foreign key ([subscriptionStatusId]) references [bff].[subscriptionStatus] ([subscriptionStatusId])
go

alter table [bff].[subscription] add constraint [fk_subscription_procedureId] foreign key ([procedureId]) references [bff].[procedure] ( [procedureId] )
go

alter table [bff].[subscription] add constraint [fk_subscription_pricingId] foreign key ([pricingId]) references [bff].[pricing]( [pricingId] )
go

alter table [bff].[subscription] add constraint [df_subscription_creationDate] default getutcdate() for [creationDate]
go

alter table [bff].[subscription] add constraint [df_subscription_lastUpdate] default getutcdate() for [lastUpdate]
go

alter table [bff].[subscription] add constraint [chk_subscription_whatsappCounter] check ( [whatsappCounter] is null or [whatsappCounter] >= 0)
go

alter table [bff].[subscription] add constraint [chk_subscription_totalWhatsapp] check ( [totalWhatsapp] is null or [totalWhatsapp] > 0)
go

alter table [bff].[subscription] add constraint [chk_subscription_emailCounter] check ( [emailCounter] >= 0)
go

alter table [bff].[subscription] add constraint [chk_subscription_totalEmail] check ( [totalEmail] > 0)
go

alter table [bff].[subscription] add constraint [chk_subscription_smsCounter] check ( [smsCounter] >= 0)
go

alter table [bff].[subscription] add constraint [chk_subscription_totalSms] check ( [totalSms] > 0)
go

alter table [bff].[subscription] add constraint [chk_subscription_amount] check ( [amount] > 0)
go

create index [ix_subscription_subscriptionStatusId] on [bff].[subscription] ([subscriptionStatusId])
go

create index [ix_subscription_procedureId] on [bff].[subscription] ([procedureId])
go

create index [ix_subscription_pricingId] on [bff].[subscription] ([pricingId])
go
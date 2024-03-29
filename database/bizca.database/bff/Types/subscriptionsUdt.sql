﻿create type [bff].[subscriptionsUdt] as table
(
	[subscriptionId]		int not null,
	[subscriptionCode]      uniqueidentifier not null,
	[subscriptionStatusId]  smallint not null,
	[procedureTypeId]		int not null,
	[organismId]			int not null,
	[bundleId]				smallint null,
	[isFreeze]				bit null,
	[amount]				money null,
	[firstName]		        nvarchar(100) not null,
	[lastName]		        nvarchar(100) not null,
	[phoneNumber]	        nvarchar(50) not null,
	[whatsapp]		        nvarchar(50) null,
	[email]			        nvarchar(50) not null,
	[whatsappCounter]       int null,
	[totalWhatsapp]         int null,
	[emailCounter]          int null,
	[totalEmail]            int null,
	[smsCounter]            int null,
	[totalSms]              int null,
	[activatedChannelMask]  smallint not null,
	[confirmedChannelMask]  smallint not null,
    [beginDate]	            datetime2 null,
    [endDate]               datetime2 null,
	primary key ([subscriptionId]),
	index ix_udt_subscriptions_subscriptionCode ([subscriptionCode])
)

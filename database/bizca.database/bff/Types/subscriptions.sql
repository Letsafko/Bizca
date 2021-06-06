create type [bff].[subscriptions] as table
(
	[subscriptionId]		int identity(1,1) not null,
	[subscriptionStatusId]  smallint not null,
	[userId]				int not null,
	[procedureId]			int not null,
	[bundleId]				smallint not null,
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
    [endDate]               datetime2 null
)

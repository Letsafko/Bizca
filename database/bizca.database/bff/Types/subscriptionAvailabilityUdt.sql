create type [bff].[subscriptionAvailabilityUdt] as table
(
	[subscriptionId] int not null,
	[emailCounter]	 smallint not null,
	[smsCounter]	 smallint not null,
	primary key ([subscriptionId]),
	index ix_udt_subscriptionAvailability_subscriptionId ([subscriptionId])
)
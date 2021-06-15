create table [bff].[subscriptionStatus]
(
	[subscriptionStatusId]   smallint not null,
	[subscriptionStatusCode] varchar(20) not null,
	[subscriptionStatusName] varchar(20) not null,
	[creationDate]			 datetime2 not null,
    [lastUpdate]			 datetime2 not null
)
go

alter table [bff].[subscriptionStatus] add constraint [pk_subscriptionStatus] primary key clustered ( [subscriptionStatusId] asc)
go

alter table [bff].[subscriptionStatus] add constraint [df_subscriptionStatus_creationDate] default getutcdate() for [creationDate]
go

alter table [bff].[subscriptionStatus] add constraint [df_subscriptionStatus_lastUpdate] default getutcdate() for [lastUpdate]
go

create unique index [ix_subscriptionStatus_subscriptionStatusCode] on [bff].[subscriptionStatus] ([subscriptionStatusCode])
go

create table [ref].[channel]
(
	[channelId]     smallint    not null,
	[channelCode]   varchar(30) not null,
	[creationDate]	datetime2   not null,
    [lastUpdate]	datetime2   not null
)
go

alter table [ref].[channel] add constraint [pk_channel] primary key clustered ( [channelId] asc)
go

alter table [ref].[channel] add constraint [df_channel_creationDate] default getutcdate() for [creationDate]
go

alter table [ref].[channel] add constraint [df_channel_lastUpdate] default getutcdate() for [lastUpdate]
go

create unique index [ix_channelCode] on [ref].[channel] ([channelCode] asc)
go
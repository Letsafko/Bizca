create table [usr].[userChannel]
(
	[userId]		 int not null,
	[channelId]		 smallint not null,
	[value]     	 varchar(50) not null,
	[active]		 bit not null,
	[confirmed]		 bit not null,
	[creationDate]	 datetime2 not null,
    [lastUpdate]	 datetime2 not null
)
go

alter table [usr].[userChannel] add constraint [pk_userChannel] primary key clustered ( [userId], [channelId] )
go

alter table [usr].[userChannel] add constraint [df_userChannel_creationDate] default getutcdate() for [creationDate]
go

alter table [usr].[userChannel] add constraint [df_userChannel_lastUpdate] default getutcdate() for [lastUpdate]
go

alter table [usr].[userChannel] add constraint [fk_userChannel_channelId] foreign key ([channelId]) references [ref].[channel]([channelId]) 
go

alter table [usr].[userChannel] add constraint [fk_userChannel_userId] foreign key ([userId]) references [usr].[user]([userId]) 
go

create index [ix_userChannel_channelId] on [usr].[userChannel]( [channelId])
go

create index [ix_userChannel_userId] on [usr].[userChannel]([userId])
go

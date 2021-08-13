create table [usr].[userChannelConfirmation]
(
	[userId]			int not null,
	[channelId]			smallint not null,
	[confirmationCode]	varchar(50) not null,
	[creationDate]		datetime2 not null,
	[expirationDate]	datetime2 null
)
go

alter table [usr].[userChannelConfirmation] add constraint [pk_userChannelConfirmation] primary key clustered ( [userId], [channelId], [creationDate] asc)
go

alter table [usr].[userChannelConfirmation] add constraint [df_userChannelConfirmation_creationDate] default getdate() for [creationDate]
go

alter table [usr].[userChannelConfirmation] add constraint [fk_userChannelConfirmation_channelId] foreign key ([channelId]) references [ref].[channel]([channelId]) 
go

alter table [usr].[userChannelConfirmation] add constraint [fk_userChannelConfirmation_userId] foreign key ([userId]) references [usr].[user]([userId]) 
go

create index [ix_userChannelConfirmation_userId_channelId] on [usr].[userChannelConfirmation] ([userId], [channelId])
go

create index [ix_userChannelConfirmation_channelId] on [usr].[userChannelConfirmation] ([channelId])
go

create index [ix_userChannelConfirmation_userId] on [usr].[userChannelConfirmation] ([userId])
go
create table [usr].[userChannelConfirmation]
(
	[userChannelId]	   int not null,
	[confirmationCode] varchar(50) not null,
	[creationDate]	   datetime2 not null,
	[expirationDate]   datetime2 null
)
go

alter table [usr].[userChannelConfirmation] add constraint [pk_userChannelConfirmation] primary key clustered ( [userChannelId], [creationDate] asc)
go

alter table [usr].[userChannelConfirmation] add constraint [df_userChannelConfirmation_creationDate] default getutcdate() for [creationDate]
go

create index [ix_userChannelConfirmation_userChannelId] on [usr].[userChannelConfirmation] ([userChannelId])
go

alter table [usr].[userChannelConfirmation] add constraint [fk_userChannelConfirmation_userChannelId] foreign key ([userChannelId]) references [usr].[userChannel]([userChannelId]) 
go
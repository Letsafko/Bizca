create table [usr].[userChannel]
(
    [userId]		 int not null,
    [channelMask]		 smallint not null,
    [partnerId]      smallint not null default 1,
    [value]     	 varchar(50) not null,
    [active]		 bit not null,
    [confirmed]		 bit not null,
    [creationDate]	 datetime2 not null,
    [lastUpdate]	 datetime2 not null
    )
    go

alter table [usr].[userChannel] add constraint [pk_userChannel] primary key clustered ( [userId], [channelMask] )
    go

alter table [usr].[userChannel] add constraint [df_userChannel_creationDate] default getdate() for [creationDate]
    go

alter table [usr].[userChannel] add constraint [df_userChannel_lastUpdate] default getdate() for [lastUpdate]
    go

alter table [usr].[userChannel] add constraint [fk_userChannel_partnerId] foreign key ([partnerId]) references [ref].[partner]([partnerId])
    go

alter table [usr].[userChannel] add constraint [fk_userChannel_userId] foreign key ([userId]) references [usr].[user]([userId])
    go

create unique index [ix_userChannel_value_partnerId] on [usr].[userChannel]( [value], [partnerId])
    go

create index [ix_userChannel_userId] on [usr].[userChannel]([userId])
    go

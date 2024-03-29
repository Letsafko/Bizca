﻿create table [ref].[partner]
(
	[partnerId]   smallint not null,
	[partnerCode] varchar(10) not null,
	[description] varchar(50) not null,
	[creationDate] [datetime2] not null,
    [lastUpdate] [datetime2] not null
)
go

alter table [ref].[partner] add constraint [pk_partner] primary key clustered ([partnerId] asc)
go

alter table [ref].[partner] add constraint [df_partner_creationDate] default getdate() for [creationDate]
go

alter table [ref].[partner] add constraint [df_partner_lastUpdate] default getdate() for [lastUpdate]
go

create unique index [ix_partner_partnerCode] on [ref].[partner] ([partnerCode])
go
﻿create table [ref].[country]
(
	[countryId]      smallint      not null,
    [countryCode]    varchar(2)    not null,
    [description]    varchar(50)   not null,
    [creationDate]   datetime2     not null,
    [lastUpdate]     datetime2     not null
)
go

alter table [ref].[country] add constraint [pk_country] primary key clustered ( [countryId] asc )
go

alter table [ref].[country] add constraint [df_country_creationDate] default getutcdate() for [creationDate]
go

alter table [ref].[country] add constraint [df_country_lastUpdate] default getutcdate() for [lastUpdate]
go 

create unique index [ix_countryCode] on [ref].[country] ([countryCode])
go
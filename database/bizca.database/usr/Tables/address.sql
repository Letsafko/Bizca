create table [usr].[address]
(
    [
    addressId]
    int
    identity
(
    1,
    1
) not null,
    [userId] int not null,
    [active] bit not null,
    [addressName] varchar
(
    100
) null,
    [city] varchar
(
    100
) null,
    [zipcode] varchar
(
    10
) null,
    [street] varchar
(
    255
) null,
    [countryId] smallint not null,
    [creationDate] datetime2 not null,
    [lastUpdate] datetime2 not null
    )
    go

alter table [usr].[address] add constraint [pk_address] primary key clustered ( [addressId] )
    go

alter table [usr].[address] add constraint [df_address_creationDate] default getdate() for [creationDate]
    go

alter table [usr].[address] add constraint [df_address_lastUpdate] default getdate() for [lastUpdate]
    go

alter table [usr].[address] add constraint [df_address_active] default 0 for [active]
    go

alter table [usr].[address] add constraint [fk_address_countryId] foreign key ([countryId]) references [ref].[country]([countryId])
    go

alter table [usr].[address] add constraint [fk_address_userId] foreign key ([userId]) references [usr].[user]([userId])
    go

create unique index [ix_address_userId_active] on [usr].[address]( [userId], [active]) where [active] = 1
    go

create index [ix_address_countryId] on [usr].[address]( [countryId])
    go

create index [ix_address_userId] on [usr].[address]( [userId])
    go

create type [usr].[channelCodes] as table
    (
    [userId] int not null,
    [channelId] smallint not null,
    [confirmationCode] varchar (50) not null,
    [expirationDate] datetime2 null
    )
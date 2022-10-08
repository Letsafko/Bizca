create type [usr].[passwords] as table
    (
    [userId] int not null,
    [active] bit not null,
    [securityStamp] varchar (250) not null,
    [passswordHash] varchar (250) not null
    )
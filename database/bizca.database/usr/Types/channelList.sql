create type [usr].[channelList] as table
    (
        userId int not null,
        channelId int not null,
        active bit not null,
        confirmed bit not null,
        [value] varchar (50) not null,
        primary key clustered ([userId], [channelId] asc)
    )
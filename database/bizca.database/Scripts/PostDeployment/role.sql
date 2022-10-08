merge into [bff].[role] as target
    using
    (
    values (1,'GUEST')
    ,(2,'ADMIN')
    ) as source ([roleId], [description])
    on target.[roleId] = source.[roleId]
    when matched then
update
    set [description] = source.[description],
    lastUpdate = getdate()
    when not matched by target then
insert
(
[
roleId
]
,
[
description
]
)
values
    (
    source.[roleId]
        , source.[description]
    );
go
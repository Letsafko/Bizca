merge into [ref].[partner] as target
    using
    (
    values (1, 'bizca', 'bizca'),
    (2, 'lxo', 'linxo')
    ) as source (partnerId, partnerCode, description) on target.partnerId = source.partnerId
    when matched then
update
    set partnerCode = source.partnerCode,
    description = source.description,
    lastUpdate = getdate()
    when not matched by target then
insert
(
partnerId
,
partnerCode
,
description
)
values
    (
    source.partnerId,
    source.partnerCode,
    source.description
    );
go
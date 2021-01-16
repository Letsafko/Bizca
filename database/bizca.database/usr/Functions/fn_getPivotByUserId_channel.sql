create function [usr].[fn_getPivotByUserId_channel]
(
	@userId int
) 
returns table
as
return
    select 
        userId,
	    max(case when channelId = 1 then value end) as phone,
        max(case when channelId = 1 then convert(int, active) end) as phoneActive,
        max(case when channelId = 1 then convert(int, confirmed) end) as phoneConfirmed,
        max(case when channelId = 2 then value end) as email,
        max(case when channelId = 2 then convert(int, active) end) as emailActive,
        max(case when channelId = 2 then convert(int, confirmed) end) as emailConfirmed,
        max(case when channelId = 4 then value end) as whatsapp,
        max(case when channelId = 4 then convert(int, active) end) as whatsappActive,
        max(case when channelId = 4 then convert(int, confirmed) end) as whatsappConfirmed,
        max(case when channelId = 8 then value end) as messenger,
        max(case when channelId = 8 then convert(int, active) end) as messengerActive,
        max(case when channelId = 8 then convert(int, confirmed) end) as messengerConfirmed
    from [usr].[userChannel] 
    where userId = @userId
    group by userId


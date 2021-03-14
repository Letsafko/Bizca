create function [usr].[fn_getPivotByUserId_channel]
(
	@userId int
) 
returns table
as
return
    select 
        userId,
	    max(case when channelMask & 1 != 0 then value end) as phone,
        max(case when channelMask & 1 != 0 then convert(int, active) end) as phoneActive,
        max(case when channelMask & 1 != 0 then convert(int, confirmed) end) as phoneConfirmed,
                             
        max(case when channelMask & 2 != 0 then value end) as email,
        max(case when channelMask & 2 != 0 then convert(int, active) end) as emailActive,
        max(case when channelMask & 2 != 0 then convert(int, confirmed) end) as emailConfirmed,
                             
        max(case when channelMask & 4 != 0 then value end) as whatsapp,
        max(case when channelMask & 4 != 0 then convert(int, active) end) as whatsappActive,
        max(case when channelMask & 4 != 0 then convert(int, confirmed) end) as whatsappConfirmed,
                             
        max(case when channelMask & 8 != 0 then value end) as messenger,
        max(case when channelMask & 8 != 0 then convert(int, active) end) as messengerActive,
        max(case when channelMask & 8 != 0 then convert(int, confirmed) end) as messengerConfirmed
    from [usr].[userChannel] 
    where userId = @userId
    group by userId


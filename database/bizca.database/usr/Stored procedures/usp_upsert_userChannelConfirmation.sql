create procedure [usr].[usp_upsert_userChannelConfirmation]
	@channelCodes [usr].[channelCodes] readonly
as
    insert into [usr].[userChannelConfirmation]
    (
        userId,		
		channelId,	
        confirmationCode,
        expirationDate
    )
select s.userId,
       s.channelId,
       s.confirmationCode,
       s.expirationDate
from @channelCodes s
where not exists
    (
        select 1 from [usr].[userChannelConfirmation] 
        where userId = s.userId 
          and channelId = s.channelId 
          and confirmationCode = s.confirmationCode
    )
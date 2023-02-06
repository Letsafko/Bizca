create procedure [usr].[usp_upsert_userChannel]
	  @channels [usr].[channelList] readonly
as
begin

	delete uc
	from [usr].[userChannel] uc
	join @channels c 
	on 
	(
		c.userId = uc.userId and 
		c.channelId & uc.channelMask != 0
	)
	
	insert into [usr].[userChannel]
	(
		  [userId]		
		, [channelMask]		
		, [value]     	
		, [active]		
		, [confirmed]		
		, [creationDate]	
		, [lastUpdate]	
	)
select
    userId
     , channelId
     , [value]
     , active
     , confirmed
     , getdate()
     , getdate()
from @channels

end
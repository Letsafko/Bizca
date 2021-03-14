create procedure [usr].[usp_upsert_userChannel]
	  @channels [usr].[channelList] readonly
as
begin

	delete from [usr].[userChannel]
	where exists
	(
		select 1 from @channels c  where userId = c.userId
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
		, getutcdate()
		, getutcdate()
	from @channels

end
create procedure [usr].[usp_upsert_userChannel]
	  @channels [usr].[channelList] readonly
as
begin

	update [usr].[userChannel]
	   set [value]      = c.[value]
		  ,[active]	    = c.[active]
		  ,[confirmed]	= c.[confirmed]
		  ,[lastUpdate] = getdate()
	from [usr].[userChannel] u
	join @channels c on u.userId = c.userId and 
	                    u.channelId = c.channelId
	
	insert into [usr].[userChannel]
	(
		  [userId]		
		, [channelId]		
		, [value]     	
		, [active]		
		, [confirmed]		
		, [creationDate]	
		, [lastUpdate]	
	)
	select
		  source.userId
		, source.channelId
		, source.[value]
		, source.active
		, source.confirmed
		, getutcdate()
		, getutcdate()
	from @channels source
	where not exists
	(
		select 1 from [usr].[userChannel] where userId = source.userId and channelId = source.channelId
	)

end
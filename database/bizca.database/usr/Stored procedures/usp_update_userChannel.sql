create procedure [usr].[usp_update_userChannel]
	  @channels [usr].[channelList] readonly
as
begin
	
	merge into [usr].[userChannel] as target
	using @channels as source on target.userId = source.userId and
	                             target.channelId = source.channelId
	when matched then
	update
		set  target.[value]      = source.[value]
		   , target.[active]	 = source.[active]
		   , target.[confirmed]	 = source.[confirmed]
		   , target.[lastUpdate] = getdate()
	when not matched by target then
		insert
		(
		     [userId]		
		   , [channelId]		
		   , [value]     	
		   , [active]		
		   , [confirmed]		
		   , [creationDate]	
		   , [lastUpdate]	
		)
		values
		(
		     source.userId
		   , source.channelId
		   , source.value
		   , source.active
		   , source.confirmed
		   , getutcdate()
		   , getutcdate()
		);

end
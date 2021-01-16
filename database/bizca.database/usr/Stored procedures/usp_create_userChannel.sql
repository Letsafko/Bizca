create procedure [usr].[usp_create_userChannel]
	  @channels [usr].[channelList] readonly
as
begin
	
	set xact_abort on

	begin try
		
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
		     c.userId
		   , c.channelId
		   , c.value
		   , c.active
		   , c.confirmed
		   , getutcdate()
		   , getutcdate()
		from @channels c

	end try
	begin catch
		
		throw

	end catch

end
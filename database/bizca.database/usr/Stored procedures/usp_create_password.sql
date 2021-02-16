create procedure [usr].[usp_create_password]
	@passwords [usr].[passwords] readonly
as
begin
	
	update s
		set s.[active] = 0
	from [usr].[password] s
	join @passwords t on t.[userId] = s.[userId]

	insert into [usr].[password]
	(
		  [userId] 	    
		, [active]		
		, [securityStamp]
		, [passwordHash]
	)
	select 
		  p.[userId] 	    
		, p.[active]		
		, p.[securityStamp]
		, p.[passswordHash]
	from @passwords p
	where [active] = 1

end

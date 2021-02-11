create procedure [usr].[usp_update_user]
	  @externalUserId	  varchar(10)  
	, @partnerId		  smallint	   
	, @civilityId	      smallint 	   = null
	, @firstName		  nvarchar(50) = null
	, @lastName			  nvarchar(50) = null
	, @birthDate   		  date 		   = null
	, @birthCountryId	  smallint 	   = null
	, @birthCity		  varchar(50)  = null
	, @economicActivityId smallint     = null
as
begin
	
	set xact_abort on

	update u
		  set u.[civilityId]	       = @civilityId    
			, u.[firstName]		       = @firstName	
			, u.[lastName]		       = @lastName	
			, u.[birthCity]		       = @birthCity
			, u.[birthDate]		       = @birthDate
			, u.[birthCountryId]	   = @birthCountryId
			, u.[economicActivityId]   = @economicActivityId
			, u.[lastUpdate]		   = getutcdate()
	output inserted.userId
	from [usr].[user] u
	where externalUserId = @externalUserId and 
	      partnerId = @partnerId 

end
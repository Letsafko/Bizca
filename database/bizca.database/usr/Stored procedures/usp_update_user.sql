create procedure [usr].[usp_update_user]
	  @externalUserId	  varchar(20)  
	, @partnerId		  smallint	   
	, @civilityId	      smallint 	   = null
	, @firstName		  nvarchar(50) = null
	, @lastName			  nvarchar(50) = null
	, @birthDate   		  date 		   = null
	, @birthCountryId	  smallint 	   = null
	, @birthCity		  varchar(50)  = null
	, @economicActivityId smallint     = null
	, @rowversion		  rowversion
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
			, u.[lastUpdate]		   = getdate()
	output inserted.userId
	from [usr].[user] u
	where externalUserId = @externalUserId and 
	      partnerId = @partnerId and
		  [rowversion] = @rowversion

end
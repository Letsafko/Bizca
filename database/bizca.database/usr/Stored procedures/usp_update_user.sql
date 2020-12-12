create procedure [usr].[usp_update_user]
	  @externalUserId	  varchar(10)  
	, @partnerId		  smallint	   
	, @email			  varchar(50)  = null
	, @phoneNumber	      varchar(15)  = null
	, @civilityId	      smallint 	   = null
	, @firstName		  nvarchar(50) = null
	, @lastName			  nvarchar(50) = null
	, @birthDate   		  date 		   = null
	, @birthCountryId	  smallint 	   = null
	, @birthCity		  varchar(50)  = null
	, @channels			  int		   = null
	, @economicActivityId smallint     = null
as
begin
	
	set xact_abort on

	update u
		  set u.[email]			       = @email
			, u.[phoneNumber]	       = @phoneNumber  
			, u.[civilityId]	       = @civilityId    
			, u.[firstName]		       = @firstName	
			, u.[lastName]		       = @lastName	
			, u.[birthCity]		       = @birthCity
			, u.[birthDate]		       = @birthDate
			, u.[birthCountryId]	   = @birthCountryId
			, u.[channels]			   = @channels
			, u.[economicActivityId]   = @economicActivityId
			, u.[lastUpdate]		   = getutcdate()
	from [usr].[user] u
	where externalUserId = @externalUserId and
		  partnerId = @partnerId

end
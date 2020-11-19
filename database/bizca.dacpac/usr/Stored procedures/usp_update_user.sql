create procedure [usr].[usp_update_user]
	  @appUserId		  varchar(10) 
	, @partnerId		  smallint
	, @email			  varchar(50) 
	, @phoneNumber	      varchar(15)
	, @civilityId	      smallint 
	, @firstName		  nvarchar(50) 
	, @lastName			  nvarchar(50)
	, @birthDate   		  date 
	, @birthCountryId	  smallint 
	, @birthCity		  varchar(50)
	, @economicActivityId smallint = null
as
begin
	
	set xact_abort on

	update u
		  set u.[email]			= @email
			, u.[phoneNumber]	= @phoneNumber  
			, u.[civilityId]	= @civilityId    
			, u.[firstName]		= @firstName	
			, u.[lastName]		= @lastName	
			, u.birthCity		= @birthCity
			, u.birthDate		= @birthDate
			, u.birthCountryId	= @birthCountryId
			, u.[lastUpdate]	= getutcdate()
			, u.[economicActivityId] = @economicActivityId
	from [usr].[user] u
	where appUserId = @appUserId and
		  partnerId = @partnerId

end
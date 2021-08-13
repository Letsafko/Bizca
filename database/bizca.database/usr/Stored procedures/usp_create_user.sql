create procedure [usr].[usp_create_user]
	  @externalUserId	  varchar(20) 
	, @partnerId		  smallint
	, @civilityId	      smallint 
	, @firstName		  nvarchar(50) 
	, @lastName			  nvarchar(50)
	, @birthDate   		  date 
	, @birthCountryId	  smallint 
	, @birthCity		  varchar(50)
	, @userCode		      uniqueidentifier  
	, @economicActivityId smallint = null
as
begin
	
	insert into [usr].[user]
	(
		  [externalUserId]		    
		, [userCode]		    
		, [partnerId]		    
		, [civilityId]	    
		, [economicActivityId]
		, [firstName]			
		, [lastName]		
		, [birthDate]
		, [birthCountryId]
		, [birthCity]
		, [creationDate]		
		, [lastUpdate]		
	)
	output inserted.userId
	values
	(
		  @externalUserId		 
		, @userCode		     
		, @partnerId		 
		, @civilityId	     
		, @economicActivityId
		, @firstName		 
		, @lastName			 
		, @birthDate   		
		, @birthCountryId	
		, @birthCity		
		, getdate()
		, getdate()
	)

end
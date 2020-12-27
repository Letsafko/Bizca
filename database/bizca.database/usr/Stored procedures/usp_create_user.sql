create procedure [usr].[usp_create_user]
	  @externalUserId	  varchar(10) 
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
	
	set xact_abort on

	begin try
		
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
			, getutcdate()
			, getutcdate()
		)

		select scope_identity()

	end try
	begin catch
		
		throw

	end catch

end
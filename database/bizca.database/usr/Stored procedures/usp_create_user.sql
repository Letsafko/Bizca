create procedure [usr].[usp_create_user]
	  @externalUserId	  varchar(10) 
	, @email			  varchar(50) 
	, @phoneNumber	      varchar(15)
	, @partnerId		  smallint
	, @civilityId	      smallint 
	, @firstName		  nvarchar(50) 
	, @lastName			  nvarchar(50)
	, @birthDate   		  date 
	, @birthCountryId	  smallint 
	, @birthCity		  varchar(50)
	, @userCode		      uniqueidentifier  
	, @channels			  int
	, @economicActivityId smallint = null
as
begin
	
	set xact_abort on

	begin try
		
		insert into [usr].[user]
		(
			  [externalUserId]		    
			, [email]			    
			, [phoneNumber]	    
			, [userCode]		    
			, [partnerId]		    
			, [civilityId]	    
			, [economicActivityId]
			, [channels]
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
			, @email			 
			, @phoneNumber	     
			, @userCode		     
			, @partnerId		 
			, @civilityId	     
			, @economicActivityId
			, @channels
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
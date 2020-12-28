create procedure [usr].[usp_getByPartnerAndExternalUserId_user]
	@partnerId		smallint,
	@externalUserId varchar(10)
as
	select
	         u.userId
		   , u.externalUserId		    
		   , u.userCode		    
		   , u.partnerId		    
		   , u.firstName			
		   , u.lastName		
		   , c.civilityId
		   , u.birthDate
		   , u.birthCity
		   , c.civilityCode
		   , u.birthCountryId
		   , co.countryCode birthCountryCode
		   , uc.email	    
		   , uc.emailActive	    
		   , uc.emailConfirmed	    
		   , uc.phone	    
		   , uc.phoneActive	    
		   , uc.phoneConfirmed	    
		   , uc.whatsapp	    
		   , uc.whatsappActive	    
		   , uc.whatsappConfirmed	    
		   , uc.messenger
		   , uc.messengerActive	    
		   , uc.messengerConfirmed	    
		   , e.economicActivityId
		   , e.economicActivityCode
	from [usr].[user] u
	join [ref].[civility] c on c.civilityId = u.civilityId
	join [ref].[country] co on co.countryId = u.birthCountryId
	outer apply fn_getPivotByUserId_channel(u.userId) uc
	left join [ref].[economicActivity] e on e.economicActivityId = u.economicActivityId
	where externalUserId = @externalUserId and
		  partnerId		 = @partnerId
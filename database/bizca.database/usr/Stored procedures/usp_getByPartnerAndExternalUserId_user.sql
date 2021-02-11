create procedure [usr].[usp_getByPartnerAndExternalUserId_user]
	@partnerId		smallint,
	@externalUserId varchar(20)
as
	declare @userId int;

	select @userId = userId from [usr].[user] 
	where externalUserId = @externalUserId and
	      partnerId = @partnerId

	select
	         u.userId
		   , u.externalUserId		    
		   , u.userCode		    
		   , u.partnerId		    
		   , u.firstName			
		   , u.lastName		
		   , u.civilityId
		   , u.birthDate
		   , u.birthCity
		   , u.birthCountryId
		   , u.economicActivityId
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
		   , u.[rowversion]
	from [usr].[user] u
	outer apply fn_getPivotByUserId_channel(u.userId) uc
	where u.userId = @userId

	select
		uc.channelId,
		expirationDate,
		confirmationCode
	from [usr].[userChannel] uc 
	join [usr].[userChannelConfirmation] a on uc.userId = a.userId and uc.channelId = a.channelId
	where uc.userId = @userId
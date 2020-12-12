create procedure [usr].[usp_getByPartnerAndExternalUserId_user]
	@partnerId		smallint,
	@externalUserId varchar(10)
as
	select
	     u.[externalUserId]		    
	   , u.[email]			    
	   , u.[phoneNumber]	    
	   , u.[userCode]		    
	   , u.[partnerId]		    
	   , u.[firstName]			
	   , u.[lastName]			
	   , c.civilityId
	   , u.birthDate
	   , u.birthCity
	   , c.[civilityCode]	
	   , u.birthCountryId
	   , co.countryCode birthCountryCode
	   , e.[economicActivityId]
	   , e.[economicActivityCode]
	from [usr].[user] u
	join [ref].[civility] c on c.civilityId = u.civilityId
	join [ref].[country] co on co.countryId = u.birthCountryId
	left join [ref].[economicActivity] e on e.economicActivityId = u.economicActivityId
	where externalUserId = @externalUserId and
		  partnerId		 = @partnerId
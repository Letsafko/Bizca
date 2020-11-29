﻿create procedure [usr].[usp_getByPartnerAndAppUserId_user]
	@partnerId smallint,
	@appUserId varchar(10)
as
	select
	     u.[appUserId]		    
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
	where appUserId = @appUserId and
		  partnerId = @partnerId
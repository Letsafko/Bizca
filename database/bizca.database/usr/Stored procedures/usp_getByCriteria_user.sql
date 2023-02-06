create procedure [usr].[usp_getByCriteria_user]
	@partnerId		smallint 
  , @externalUserId	varchar(10)	  = null
  , @email			varchar(50)	  = null
  , @phone			varchar(15)	  = null
  , @whatsapp		varchar(15)	  = null
  , @firstName		nvarchar(50)  = null
  , @lastName		nvarchar(50)  = null
  , @birthDate		date		  = null
  , @direction	    varchar(10)	  = 'next'
  , @pageSize		int			  = 20 
  , @index			int			  = 0
as
begin
	
	declare @parmDefinition nvarchar(500) = 
	  ' @partnerId		smallint 
	  , @externalUserId	varchar(10)	
	  , @email			varchar(50)	
	  , @phone			varchar(15)	
	  , @whatsapp		varchar(15)
	  , @firstName		nvarchar(50)
	  , @lastName		nvarchar(50)
	  , @birthDate		date		
	  , @direction	    varchar(10)
	  , @pageSize		int			
	  , @index			int';
	   
	declare @query nvarchar(MAX) = 
		'select top(@pageSize)
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
		   , co.description birthCountryDescription
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
		   , a.addressName
		   , a.addressId
		   , a.[active]  [addresseActive]
		   , a.[city]    [addresseCity]
		   , a.[zipcode]	
		   , a.[street]	 [addresseStreet]
		   , co2.countryCode
		   , co2.countryId
		   , co2.[description]
		from [usr].[user] u
		join [ref].[civility] c on c.civilityId = u.civilityId
		left join [ref].[country] co on co.countryId = u.birthCountryId
		left join [usr].[address] a on a.[userId] = u.[userId] and a.[active] = 1
		left join [ref].[country] co2 on co2.countryId = a.countryId
		left join [ref].[economicActivity] e on e.economicActivityId = u.economicActivityId
		outer apply usr.fn_getPivotByUserId_channel(u.userId) uc
		where u.partnerId = @partnerId';

	if @phone is not null
		set @query = @query + ' and uc.phone = @phone';

	if @whatsapp is not null
		set @query = @query + ' and uc.whatsapp = @whatsapp';
	
	if @birthDate is not null
		set @query = @query + ' and u.birthDate = @birthDate';

	if @email is not null
		set @query = @query + ' and uc.email = @email';

	if @externalUserId is not null
		set @query = @query + ' and u.externalUserId = @externalUserId';

	if @firstName is not null
		set @query = @query + ' and contains((u.firstName, u.lastName), @firstName)';

	if @lastName is not null
		set @query = @query + ' and contains((u.firstName, u.lastName), @lastName)';

	if @direction = 'next'
		set @query = @query + ' and u.userId > @index';
else
		set @query = @query + ' and u.userId < @index';

	set @query = @query + ' order by u.userId asc'

	execute [dbo].[sp_executesql] @query
	, @parmDefinition
	, @partnerId
	, @externalUserId	
	, @email		
	, @phone	
	, @whatsapp
	, @firstName	
	, @lastName	
	, @birthDate	
	, @direction	
	, @pageSize	
	, @index;

end
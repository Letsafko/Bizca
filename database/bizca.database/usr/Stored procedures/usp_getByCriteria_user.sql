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
	
	if @direction is null or @direction not in ('prev', 'next')
	begin
		declare @msg varchar(100) = formatmessage('invalid direction value(%s), should be (prev or next)', @direction)
		raiserror(@msg, 16, 1);
		return;
	end
	
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
			 u.externalUserId		    
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
		   , uc.emailconfirmed	    
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
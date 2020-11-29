create procedure [usr].[usp_getByCriteria_user]
	@partnerCode	varchar(30)	  
  , @appUserId		varchar(10)	  = null
  , @email			varchar(50)	  = null
  , @phone			varchar(15)	  = null
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
	
	declare @ParmDefinition nvarchar(500) = '@partnerCode	varchar(30)	
	  , @appUserId		varchar(10)	
	  , @email			varchar(50)	
	  , @phone			varchar(15)	
	  , @firstName		nvarchar(50)
	  , @lastName		nvarchar(50)
	  , @birthDate		date		
	  , @direction	    varchar(10)
	  , @pageSize		int			
	  , @index			int';
	   
	declare @query nvarchar(MAX) = 
		'select top(@pageSize)
			 u.appUserId		    
		   , u.email	    
		   , u.phoneNumber	    
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
		   , e.economicActivityId
		   , e.economicActivityCode
		from [usr].[user] u
		join [ref].[partner] p on p.partnerId = u.partnerId
		join [ref].[civility] c on c.civilityId = u.civilityId
		join [ref].[country] co on co.countryId = u.birthCountryId
		left join [ref].[economicActivity] e on e.economicActivityId = u.economicActivityId
		where p.partnerCode = @partnerCode';

	if @phone is not null
		set @query = @query + ' and u.phoneNumber = @phone';
	
	if @birthDate is not null
		set @query = @query + ' and u.birthDate = @birthDate';

	if @email is not null
		set @query = @query + ' and contains((u.email), @email)';

	if @appUserId is not null
		set @query = @query + ' and contains((u.appUserId), @appUserId)';

	if @firstName is not null
		set @query = @query + ' and contains((u.firstName, u.lastName), @firstName)';

	if @lastName is not null
		set @query = @query + ' and contains((u.firstName, u.lastName), @lastName)';

	if @direction = 'next'
		set @query = @query + ' and u.userId > @index order by u.userId asc';
	else
		set @query = @query + ' and u.userId < @index order by u.userId desc';

	execute sp_executesql @query
	, @ParmDefinition
	, @partnerCode
	, @appUserId	
	, @email		
	, @phone		
	, @firstName	
	, @lastName	
	, @birthDate	
	, @direction	
	, @pageSize	
	, @index;

end
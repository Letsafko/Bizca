create procedure [usr].[usp_getByCriteria_user]
	@partnerCode	varchar(30)	  
  , @appUserId		varchar(10)	  = null
  , @email			varchar(50)	  = null
  , @phone			varchar(15)	  = null
  , @firstName		nvarchar(50)  = null
  , @lastName		nvarchar(50)  = null
  , @birthDate		date		  = null
  , @pageSize		int			  = 20 
as
begin
	
	declare @ParmDefinition nvarchar(500) = '@pageSize int
	   , @partnerCode	varchar(30)
	   , @appUserId		varchar(10)
	   , @email			varchar(50)	
	   , @phone			varchar(15)	
	   , @firstName		nvarchar(50)	
	   , @lastName		nvarchar(50)	
	   , @birthDate		date';
	   
	declare @query nvarchar(MAX) = 
		'select top(@pageSize)
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
		join [ref].[partner] p on p.partnerId = u.partnerId
		join [ref].[civility] c on c.civilityId = u.civilityId
		join [ref].[country] co on co.countryId = u.birthCountryId
		left join [ref].[economicActivity] e on e.economicActivityId = u.economicActivityId
		where p.partnerCode = @partnerCode';

	if @email is not null
		set @query = @query + ' and u.email = @email';

	if @phone is not null
		set @query = @query + ' and u.phoneNumber = @phone';
	
	if @birthDate is not null
		set @query = @query + ' and u.birthDate = @birthDate'

	if @appUserId is not null
	begin
		set @appUserId = @appUserId + '%'
		set @query = @query + ' and u.appUserId like @appUserId'
	end 

	if @firstName is not null
	begin
		set @firstName = @firstName + '%'
		set @query = @query + ' and u.firstName like @firstName'
	end

	if @lastName is not null
	begin
		set @lastName = @lastName + '%'
		set @query = @query + ' and u.lastName like @lastName'
	end

	execute sp_executesql @query
	, @ParmDefinition
	, @pageSize  
	, @partnerCode
	, @appUserId  
	, @email	 
	, @phone	 
	, @firstName
	, @lastName	
	, @birthDate 

end
	
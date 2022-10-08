create procedure [ref].[usp_GetById_Country]
	@countryId smallint
as
select countryId,
       countryCode,
    [description]
from [ref].[country]
where countryId = @countryId
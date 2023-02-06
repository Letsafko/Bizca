create procedure [ref].[usp_getById_Civility]
	@civilityId smallint
as
select civilityId,
       civilityCode
from [ref].[civility]
where civilityId = @civilityId
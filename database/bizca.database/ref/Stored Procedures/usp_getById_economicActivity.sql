create procedure [ref].[usp_getById_EconomicActivity]
	@economicActivityId smallint
as
select economicActivityId,
       economicActivityCode,
       description
from [ref].[economicActivity]
where economicActivityId = @economicActivityId
create procedure [ref].[usp_GetById_Partner]
	@partnerId smallint
as
select partnerId,
       partnerCode,
    [description]
from [ref].[partner]
where partnerId = @partnerId

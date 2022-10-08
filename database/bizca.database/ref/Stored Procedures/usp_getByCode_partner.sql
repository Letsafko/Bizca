create procedure [ref].[usp_getByCode_Partner]
	@partnerCode varchar(30)
as
select partnerId,
       partnerCode,
       description
from [ref].[partner]
where partnerCode = @partnerCode
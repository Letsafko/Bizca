create function [bff].[fn_getById_bundle]
(
	@bundleId int
)
returns table
as
return
    select top 1 
		  bundleId
		, bundleCode
		, description bundleLabel
		, [priority]
		, amount        [price]
		, intervalInWeeks
		, totalEmail    [bundleTotalEmail]
		, totalSms      [bundleTotalSms]
		, totalWhatsapp [bundleTotalWhatsapp]
	from [bff].[bundle]
	where bundleId = @bundleId
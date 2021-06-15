create procedure [bff].[usp_getAll_bundle]
as
begin
	
	 select
		  bundleId
		, bundleCode
		, description bundleLabel
		, [priority]
		, amount        [price]
		, intervalInWeeks
		, totalEmail    [bundleTotalEmail]
		, totalSms      [bundleTotalSms]
		, totalWhatsapp [bundleTotalWhatsapp]
		, [description]
	from [bff].[bundle]

end


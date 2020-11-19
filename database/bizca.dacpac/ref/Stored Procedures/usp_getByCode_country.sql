create procedure [ref].[usp_getByCode_country]
	@countryCode varchar(2)
as
	select 
		countryId,
		countryCode,
		description
	from [ref].[country]
	where countryCode = @countryCode

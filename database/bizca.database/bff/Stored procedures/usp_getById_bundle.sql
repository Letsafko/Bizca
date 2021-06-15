create procedure [bff].[usp_getById_bundle]
	@bundleId int
as
	select * from fn_getById_bundle(@bundleId)
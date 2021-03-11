create procedure [usr].[usp_isExists_channel]
	@partnerId		 smallint,
	@channelResource varchar(50)
as
	select 1 from [usr].[userChannel] 
	where  [value] = @channelResource and
	       partnerId = @partnerId
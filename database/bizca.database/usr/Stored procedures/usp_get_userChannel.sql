create procedure[usr].[usp_get_userChannel]
	@userId int,
	@channelId smallint
as
	select
		channelId,
		value,
		active,
		confirmed
	from [usr].[userChannel]
	where channelId = @channelId and
			userId = @userId

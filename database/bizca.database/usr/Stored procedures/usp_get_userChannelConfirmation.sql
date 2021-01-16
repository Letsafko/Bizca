create procedure [usr].[usp_get_userChannelConfirmation]
	@userId int,
	@channelId smallint,
	@confirmationCode varchar(50)
as
	select
		channelId,
		value as [channelValue],
		expirationDate,
		confirmationCode
	from [usr].[userChannelConfirmation] a
	join [usr].[userChannel] uc on uc.userChannelId = a.userChannelId
	where userId = @userId and
		  channelId = @channelId and
		  confirmationCode = @confirmationCode

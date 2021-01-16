create procedure [usr].[usp_create_userChannelConfirmation]
	@userId int,
    @channelId smallint,
    @expirationDate datetime,
    @codeConfirmation varchar(50)
as
    insert into [usr].[userChannelConfirmation]
    (
        userChannelId,
        confirmationCode,
        expirationDate
    )
    select 
        userChannelId,
        @codeConfirmation,
        @expirationDate
    from [usr].[userChannel]
    where userId = @userId and 
          channelId = @channelId
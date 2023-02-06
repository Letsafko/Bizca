create procedure [bff].[usp_create_user]
	  @externalUserId	  varchar(20) 
	, @civilityId	      smallint 
	, @roleId		      smallint 
	, @firstName		  nvarchar(100) 
	, @lastName			  nvarchar(100)
	, @phoneNumber        nvarchar(20)
	, @whatsapp           nvarchar(20)
	, @email              nvarchar(50)
	, @confirmationStatus smallint
	, @activationStatus   smallint
as
begin

insert into [bff].[user]
    (
          [externalUserId]
        , [civilityId]
        , [roleId]
        , [firstName]
        , [lastName]
        , [phoneNumber]
        , [whatsapp]
        , [email]
        , [channelConfirmationStatus]
        , [channelActivationStatus]
        , [creationDate]
        , [lastUpdate]
    )
    output inserted.userId
values
    (
          @externalUserId
        , @civilityId
        , @roleId
        , @firstName
        , @lastName
        , @phoneNumber
        , @whatsapp
        , @email
        , @confirmationStatus
        , @activationStatus
        , getdate()
        , getdate()
    )

end
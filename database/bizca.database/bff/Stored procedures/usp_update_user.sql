create procedure [bff].[usp_update_user]
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
	, @rowversion		  rowversion
as
begin
	
	set
xact_abort on

update u
set u.[civilityId]                = @civilityId
  , u.[firstName]                 = @firstName
  , u.[roleId]                    = @roleId
  , u.[lastName]                  = @lastName
  , u.[phoneNumber]               = @phoneNumber
  , u.[whatsapp]                  = @whatsapp
  , u.[email]                     = @email
  , u.[channelConfirmationStatus] = @confirmationStatus
  , u.channelActivationStatus     = @activationStatus
  , u.[lastUpdate]                = getdate() output inserted.userId
from [bff].[user] u
where externalUserId = @externalUserId
  and
    [rowversion] = @rowversion

end
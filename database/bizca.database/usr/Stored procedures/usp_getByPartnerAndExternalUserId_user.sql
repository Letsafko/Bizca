create procedure [usr].[usp_getByPartnerAndExternalUserId_user]
	@partnerId		smallint,
	@externalUserId varchar(20)
as
declare @userId int;

select @userId = userId from [usr].[user]
where externalUserId = @externalUserId and
    partnerId = @partnerId

select dto = 'user'
select
    u.userId
     , u.externalUserId
     , u.userCode
     , u.partnerId
     , u.firstName
     , u.lastName
     , u.civilityId
     , u.birthDate
     , u.birthCity
     , u.birthCountryId
     , u.economicActivityId
     , uc.email
     , uc.emailActive
     , uc.emailConfirmed
     , uc.phone
     , uc.phoneActive
     , uc.phoneConfirmed
     , uc.whatsapp
     , uc.whatsappActive
     , uc.whatsappConfirmed
     , uc.messenger
     , uc.messengerActive
     , uc.messengerConfirmed
     , u.[rowversion]
from [usr].[user] u
    outer apply fn_getPivotByUserId_channel(u.userId) uc
where u.userId = @userId

select dto = 'channelConfirmations'
select
    channelId,
    expirationDate,
    confirmationCode
from [usr].[userChannelConfirmation] uco
    join [usr].[userChannel] uc
on
    (
    uc.channelMask & uco.channelId != 0 and
    uc.userId = uco.userId
    )
where uco.userId = @userId

select dto = 'addresses'
select
    a.addressName
     , a.[addressId]
     , a.[active]
     , a.[city]
     , a.[zipcode]
     , a.[street]
     , c.countryCode
     , c.countryId
     , c.[description]
from [usr].[address] a
    join [ref].[country] c on c.countryId = a.countryId
where a.userId = @userId

select dto = 'passwords'
select
    [active]
        , [passwordHash]
        , [securityStamp]
from [usr].[password]
where userId = @userId
create procedure [bff].[usp_getByProcedure_subscribers]
	@procedureTypeId int,
	@organismId		 int
as
select s.subscriptionId,
       s.subscriptionCode,
       s.lastName,
       s.firstName,
       s.email,
       s.emailCounter,
       s.totalEmail,
       s.phoneNumber,
       s.smsCounter,
       s.totalSms
from bff.subscription s
where s.subscriptionStatusId = 2
  and s.organismId = @organismId
  and s.procedureTypeId = @procedureTypeId
  and (
                s.totalSms - s.smsCounter > 0 or
                s.totalEmail - s.emailCounter > 0
    )
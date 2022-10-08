create procedure [bff].[usp_update_subscriptionAvailability]
	@subscriptions [bff].[subscriptionAvailabilityUdt] readonly
as
update s
set s.emailCounter = udt.emailCounter,
    s.smsCounter   = udt.smsCounter from bff.subscription s
	join @subscriptions udt
on udt.subscriptionId = s.subscriptionId
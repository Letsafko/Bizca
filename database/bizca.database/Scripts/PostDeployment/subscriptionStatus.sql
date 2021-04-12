merge into [bff].[subscriptionStatus] as target
	using 
	(
		values   (1,'Deactivated','Deactivated')
				,(2,'Expired','Expired')
				,(3,'Active','Active')
	) as source(subscriptionStatusId, subscriptionStatusCode, subscriptionStatusName) 
	on 
	(
		target.subscriptionStatusId   = source.subscriptionStatusId
	)
when matched then 
	update
		set subscriptionStatusName = source.subscriptionStatusName,
			lastUpdate  = getutcdate()
when not matched by target then
	insert
	(
		  subscriptionStatusId
		, subscriptionStatusCode
		, subscriptionStatusName
	)
	values
	(
		  source.subscriptionStatusId
		, source.subscriptionStatusCode
		, source.subscriptionStatusName
	);
go
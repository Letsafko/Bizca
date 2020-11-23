merge into [ref].[economicActivity] as target
	using 
	(
		values   (1,'craftsman','craftsman')
				,(2,'technician','technician')
				,(3,'engineer','engineer')
				,(4,'student','student')
	) as source(economicActivityId, economicActivityCode, description) on target.economicActivityId = source.economicActivityId
when matched then 
	update
		set economicActivityCode = source.economicActivityCode,
			description  = source.description,
			lastUpdate  = getutcdate()
when not matched by target then
	insert
	(
		economicActivityId, 
		economicActivityCode, 
		description
	)
	values
	(
		source.economicActivityId, 
		source.economicActivityCode, 
		source.description
	);
go
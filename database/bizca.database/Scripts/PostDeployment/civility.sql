merge into [ref].[civility] as target
	using 
	(
		values   (1,'Mr')
				,(2,'Mrs')
				,(3,'Miss')
				,(4,'Other')
	) as source(civilityId, civilityCode) on target.civilityId = source.civilityId
when matched then 
	update
		set civilityCode = source.civilityCode,
			lastUpdate  = getutcdate()
when not matched by target then
	insert
	(
		civilityId, 
		civilityCode
	)
	values
	(
		source.civilityId, 
		source.civilityCode
	);
go
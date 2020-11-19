	declare	@civility table (
		[civilityId]	smallint	not null,
		[civilityCode]	varchar(5)	not null
	)

	insert into @civility 
	values   (1,'Mr')
			,(2,'Mrs')
			,(3,'Miss')
			,(4,'Other')

	merge into [ref].[civility] as target
		using @civility source on target.civilityId = source.civilityId
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
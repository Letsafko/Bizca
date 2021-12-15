merge into [ref].[language] as target
	using 
	(
		values   (1,'fr', 'french')
				,(2,'en', 'english')
	) as 
	source
	(
		[languageId],	
		[languageCode],
		[description]
	) on target.[languageId] = source.[languageId]
when matched then 
	update
		set [languageCode] = source.[languageCode],
			[description] = source.[description],
			lastUpdate  = getdate()
when not matched by target then
	insert
	(
		[languageId],	
		[languageCode],
		[description]	
	)
	values
	(
		source.[languageId],	
		source.[languageCode],
		source.[description]
	);
go
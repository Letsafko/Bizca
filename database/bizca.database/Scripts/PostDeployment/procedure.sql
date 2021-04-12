merge into [bff].[procedure] as target
	using 
	(
		values   (1, 1, 1)
				,(2, 2, 1)
				,(3, 1, 2)
				,(4, 2, 2)
	) as source(procedureId, procedureTypeId, organismId) on target.[procedureId] = source.[procedureId]
when matched then 
	update
		set procedureTypeId = source.procedureTypeId,
		    organismId = source.organismId,
			lastUpdate  = getutcdate()
when not matched by target then
	insert
	(
		    [procedureId]  
		  , [procedureTypeId]
		  , [organismId]	
	)
	values
	(
		    source.[procedureId]  
		  , source.[procedureTypeId]
		  , source.[organismId]	
	);
go
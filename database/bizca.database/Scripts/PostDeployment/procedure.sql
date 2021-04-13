merge into [bff].[procedure] as target
	using 
	(
		values   (1, 1, 1, 1, 'http://www.loiret.gouv.fr/create/1')
				,(2, 2, 1, 1, 'http://www.loiret.gouv.fr/create/2')
				,(3, 1, 2, 1, 'http://www.indre-et-loire.gouv.fr/create/1')
				,(4, 2, 2, 1, 'http://www.indre-et-loire.gouv.fr/create/2')
	) as source(procedureId, procedureTypeId, organismId, [active], [procedureHref]) on target.[procedureId] = source.[procedureId]
when matched then 
	update
		set procedureTypeId = source.procedureTypeId,
			procedureHref   = source.procedureHref,
		    organismId      = source.organismId,
			active          = source.active,
			lastUpdate      = getutcdate()
when not matched by target then
	insert
	(
		    [procedureId]  
		  , [procedureTypeId]
		  , [organismId]	
		  , [active]
		  , [procedureHref]
	)
	values
	(
		    source.[procedureId]  
		  , source.[procedureTypeId]
		  , source.[organismId]	
		  , source.[active]
		  , source.[procedureHref]
	);
go
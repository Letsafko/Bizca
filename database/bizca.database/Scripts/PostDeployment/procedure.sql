merge into [bff].[procedure] as target
	using 
	(
		values   (1, 1, 1, 'http://www.loiret.gouv.fr/create/1')
				,(2, 1, 1, 'http://www.loiret.gouv.fr/create/2')
				,(1, 2, 1, 'http://www.indre-et-loire.gouv.fr/create/1')
				,(2, 2, 1, 'http://www.indre-et-loire.gouv.fr/create/2')
	) as source(procedureTypeId, organismId, [active], [procedureHref]) 
	on 
	(
		target.[procedureTypeId] = source.[procedureTypeId] and
		target.[organismId]		 = source.[organismId]
	)
when matched then 
	update
		set procedureHref   = source.procedureHref,
			active          = source.active,
			lastUpdate      = getdate()
when not matched by target then
	insert
	(
		    [procedureTypeId]
		  , [organismId]	
		  , [active]
		  , [procedureHref]
	)
	values
	(
		    source.[procedureTypeId]
		  , source.[organismId]	
		  , source.[active]
		  , source.[procedureHref]
	);
go
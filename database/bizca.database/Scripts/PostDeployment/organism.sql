merge into [bff].[organism] as target
	using 
	(
		values   (1, '45234', 'Loiret'               , 'http://www.loiret.gouv.fr')
				,(2, '37261', 'Indre-et-Loire'       , 'http://www.indre-et-loire.gouv.fr')
				,(3, '56260', 'Morbihan'             , 'http://www.morbihan.gouv.fr')
				,(4, '59350', 'Nord'                 , 'http://www.nord.gouv.fr')
				,(5, '26362', 'Drôme'                , 'http://www.drome.gouv.fr')
				,(6, '06088', 'Alpes-Maritimes'      , 'http://www.alpes-maritimes.gouv.fr')
				,(7, '72181', 'Sarthe'               , 'http://www.sarthe.gouv.fr')
				,(8, '08105', 'Ardennes'             , 'http://www.ardennes.gouv.fr')
	) as source(organismId, codeInsee, organismName, organismHref) 
	on 
	(
		target.organismId = source.organismId
	)
when matched then 
	update
		set organismName = source.organismName,
		    organismHref = source.organismHref,
		    codeInsee    = source.codeInsee,
			lastUpdate   = getutcdate()
when not matched by target then
	insert
	(
		  [organismId]	
		, [codeInsee]		
		, [organismName]
		, [organismHref]
	)
	values
	(
		  source.[organismId]	
		, source.[codeInsee]		
		, source.[organismName]
		, source.[organismHref]
	);
go
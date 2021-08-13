merge into [bff].[procedureType] as target
	using 
	(
		values   (1,'Première demande de titre de sejour')
				,(2,'Renouvellement de titre de séjour salarié')
	) as source(procedureTypeId, [description]) 
	on 
	(
		target.[procedureTypeId] = source.[procedureTypeId]
	)
when matched then 
	update
		set [description] = source.[description],
			lastUpdate  = getdate()
when not matched by target then
	insert
	(
		    [procedureTypeId]
		  , [description]	 
	)	  
	values
	(
		    source.[procedureTypeId]
		  , source.[description]	 
	);
go
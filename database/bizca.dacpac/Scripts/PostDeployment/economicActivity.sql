	declare	@economicActivity table (
		[economicActivityId]	smallint	not null,
		[economicActivityCode]	varchar(30)	not null,
		[description]			varchar(50) not null
	)

	insert into @economicActivity 
	values   (1,'craftsman','craftsman')
			,(2,'technician','technician')
			,(3,'engineer','engineer')
			,(4,'student','student')

	merge into [ref].[economicActivity] as target
		using @economicActivity source on target.economicActivityId = source.economicActivityId
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
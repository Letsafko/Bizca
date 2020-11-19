	declare	@partner table (
		[partnerId]   smallint not null,
		[partnerCode] varchar(30) not null,
		[description] varchar(50) not null
	)

	insert into @partner 
	values (1, 'bizca', 'bizca'),
		   (2, 'lxo', 'linxo')

	merge into [ref].[partner] as target
		using @partner source on target.partnerId = source.partnerId
	when matched then 
		update
			set partnerCode = source.partnerCode,
			    description = source.description,
				lastUpdate	= getutcdate()
	when not matched by target then
		insert
		(
			partnerId, 
			partnerCode, 
			description
		)
		values
		(
			source.partnerId, 
			source.partnerCode, 
			source.description
		);
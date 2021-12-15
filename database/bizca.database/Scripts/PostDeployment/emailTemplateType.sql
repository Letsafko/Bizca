merge into [ref].[emailTemplateType] as target
	using 
	(
		values   (1, 'Activer votre compte', 'Activate your account'),
				 (2, 'Réinitialiser votre mot de passe', 'Reset your password')
	) as source
	(	
		[emailTemplateTypeId], 
		[descriptionFr], 
		[descriptionEn]
	) on target.[emailTemplateTypeId] = source.[emailTemplateTypeId]
when matched then 
	update
		set [descriptionFr] = source.[descriptionFr],
			[descriptionEn] = source.[descriptionEn],
			lastUpdate  = getdate()
when not matched by target then
	insert
	(
		[emailTemplateTypeId], 
		[descriptionFr], 
		[descriptionEn]
	)
	values
	(
		source.[emailTemplateTypeId], 
		source.[descriptionFr], 
		source.[descriptionEn]
	);
go
﻿declare @template table
(
	[emailTemplateId]		smallint not null,
	[emailTemplateTypeId]	smallint not null,
	[languageId]			smallint not null
)

declare @environnement varchar(30)  = '$(Environnement)'
if @environnement in ('Dev','Integration', 'AzureIntegration')
begin
	insert into @template values
	(3, 1, 1),
	(4, 2, 1),
	(6, 1, 2),
	(8, 3, 1)
end

merge into [ref].[emailTemplate] as target
	using @template as source  
	on target.[emailTemplateTypeId] = source.[emailTemplateTypeId] and 
		target.[languageId] = source.[languageId]
when matched then 
	update
		set [emailTemplateId] = source.[emailTemplateId],
			[lastUpdate]  = getdate()
when not matched by target then
	insert
	(
		[languageId],
		[emailTemplateId], 
		[emailTemplateTypeId]
	)
	values
	(
		source.[languageId],
		source.[emailTemplateId], 
		source.[emailTemplateTypeId]
	);
go
create table [ref].[emailTemplate]
(
	[emailTemplateId]		smallint not null,
	[emailTemplateTypeId]	smallint not null,
	[languageId]			smallint not null,
	[creationDate]			datetime2 not null,
    [lastUpdate]			datetime2 not null
)
go

alter table [ref].[emailTemplate] add constraint [pk_emailTemplate] 
	primary key clustered ( [emailTemplateId] )
	go

alter table [ref].[emailTemplate] add constraint [fk_emailTemplate_emailTemplateTypeId] 
	foreign key ([emailTemplateTypeId]) references [ref].[emailTemplateType] ([emailTemplateTypeId])
	go

alter table [ref].[emailTemplate] add constraint [fk_emailTemplate_languageId] 
	foreign key ([languageId]) 	references [ref].[language] ([languageId])
	go

alter table [ref].[emailTemplate] add constraint [df_emailTemplate_creationDate] 
	default getdate() for [creationDate]
	go

alter table [ref].[emailTemplate] add constraint [df_emailTemplate_lastUpdate] 
	default getdate() for [lastUpdate]
	go

create unique index [ix_emailTemplate_emailTemplateId_emailTemplateTypeId_languageId] 
	on [ref].[emailTemplate] ([emailTemplateId], [emailTemplateTypeId], [languageId]) 
	go

create index [ix_emailTemplate_emailTemplateTypeId] 
	on [ref].[emailTemplate] ([emailTemplateTypeId])
	go

create index [ix_emailTemplate_languageId] 
	on [ref].[emailTemplate] ([languageId])
	go
create procedure [ref].[usp_getByType_emailTemplate]
	@emailTemplateTypeId smallint,
	@languageCode char(2) = 'fr'
as
select et.emailTemplateId,
       et.emailTemplateTypeId,
       case
           when l.languageCode = 'fr' then ett.descriptionFr
           when l.languageCode = 'en' then ett.descriptionEn
           else ''
           end as [description]
from [ref].[emailTemplate] et
    join [ref].[emailTemplateType] ett
on et.emailTemplateTypeId = ett.emailTemplateTypeId
    join [ref].[language] l on l.languageId = et.languageId
where l.languageCode = @languageCode
  and
    et.emailTemplateTypeId = @emailTemplateTypeId
create function [bff].[fn_getById_procedure]
(
	@procedureId int
)
returns table
as
return
    select top 1
		  p.procedureId
		, p.procedureHref
		, p.active
		, pt.procedureTypeId
		, pt.description [procedureTypeLabel]
		, o.organismId
		, o.codeInsee
		, o.organismHref
		, o.organismName
	from [bff].[procedure] p
	join [bff].[procedureType] pt on pt.procedureTypeId = p.procedureTypeId
	join [bff].[organism] o on o.organismId = p.organismId
	where p.procedureId = @procedureId

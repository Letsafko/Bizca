create procedure [bff].[usp_getAll_procedure]
as
begin
	
	select 
		  p.procedureHref
		, p.procedureTypeId
		, pt.description [procedureTypeLabel]
		, o.organismId
		, o.codeInsee
		, o.organismHref
		, o.organismName
	from [bff].[procedure] p
	join [bff].[procedureType] pt on pt.procedureTypeId = p.procedureTypeId
	join [bff].[organism] o on o.organismId = p.organismId
	where p.active = 1

end

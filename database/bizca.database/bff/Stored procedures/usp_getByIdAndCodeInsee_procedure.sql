create procedure [bff].[usp_getByIdAndCodeInsee_procedure]
	@procedureTypeId int,
    @codeInsee varchar(10)
as
begin

    select top 1
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
    where p.procedureTypeId = @procedureTypeId and
          o.codeInsee = @codeInsee and
          p.active = 1

end

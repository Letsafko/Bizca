create procedure [bff].[usp_getByActiveSubscriptions_procedure]
as
begin

	;with activeProcedures 
	as
	(
		select 
			 s.procedureTypeId, 
			 s.organismId
		from bff.subscription s
		join bff.[procedure] p 
		on 
		(	
			p.procedureTypeId = s.procedureTypeId and
			p.organismId = s.organismId
		)
		join bff.organism o on o.organismId = p.organismId
		where subscriptionStatusId = 2
		group by s.procedureTypeId, 
				 s.organismId
		having count(s.procedureTypeId) > 0
	)

	select 
		p.procedureTypeId,
		p.procedureHref,
		pt.[description] [procedureTypeLabel],
		p.organismId,
		p.settings
	from [bff].[procedure] p
	join activeProcedures ap 
	on
	(
		ap.organismId = p.organismId and
		ap.procedureTypeId = p.procedureTypeId
	)
	join bff.procedureType pt on pt.procedureTypeId = p.procedureTypeId

end
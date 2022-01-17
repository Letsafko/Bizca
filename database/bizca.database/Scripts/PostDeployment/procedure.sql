merge into [bff].[procedure] as target
	using 
	(
		values   (1, 1,  1, 'http://www.loiret.gouv.fr/create/1','{}')
				,(2, 1,  1, 'http://www.loiret.gouv.fr/create/2','{}')
				,(1, 2,  1, 'http://www.indre-et-loire.gouv.fr/create/1','{}')
				,(2, 2,  1, 'http://www.indre-et-loire.gouv.fr/create/2','{}')
				,(3, 9,  1, 'https://www.rdv.var.gouv.fr/booking/create/864/3','{"iframe": "//*[@id=\\\"cnsw\\\"]/iframe", "script": "window.scrollBy(0, 400)", "clickById": "condition", "clickInputByXpath": "//*[@id=\"submit_Booking\"]/input[1]","checkError": "error", "radioBtnsClass": "//input[@name=\"planning\"]", "clickButtonByXpath": "//input[@name=\"nextButton\"]", "planningBooking": "//*[@id=\\\"planning_Booking\\\"]", "noFreePlaceAvailable": "Veuillez recommencer ultérieurement.", "elementToCheckForAppointment": "free" }')
				,(4, 10, 1, 'https://www.rdv.cote-dor.gouv.fr/booking/create/1071','{"iframe": "//*[@id=\\\"cnsw\\\"]/iframe", "script": "window.scrollBy(0, 400)", "clickById": "condition", "clickInputByXpath": "//*[@id=\"submit_Booking\"]/input[1]","checkError": "error", "radioBtnsClass": "//input[@name=\"planning\"]", "clickButtonByXpath": "//input[@name=\"nextButton\"]", "planningBooking": "//*[@id=\\\"planning_Booking\\\"]", "noFreePlaceAvailable": "Veuillez recommencer ultérieurement.", "elementToCheckForAppointment": "free" }')
	) as source(procedureTypeId, organismId, [active], [procedureHref], [settings]) 
	on 
	(
		target.[procedureTypeId] = source.[procedureTypeId] and
		target.[organismId]		 = source.[organismId]
	)
when matched then 
	update
		set procedureHref   = source.procedureHref,
			active          = source.active,
			settings	    = source.settings,
			lastUpdate      = getdate()
when not matched by target then
	insert
	(
		    [procedureTypeId]
		  , [organismId]	
		  , [active]
		  , [procedureHref]
		  , [settings]
	)
	values
	(
		    source.[procedureTypeId]
		  , source.[organismId]	
		  , source.[active]
		  , source.[procedureHref]
		  , source.[settings]
	);
go
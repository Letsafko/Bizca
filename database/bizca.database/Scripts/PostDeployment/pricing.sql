merge into [bff].[pricing] as target
	using 
	(
		values   (1, 'Basic'  , 'Basic'  , 1, 10, 10,  5,  5, 12)
				,(2, 'Confort', 'Confort', 2, 20, 20, 10, 10, 12)
				,(3, 'Prenium', 'Prenium', 3, 30, 30, 15, 15, 12)
	) as source(pricingId, pricingCode, [description], [priority], amount, totalEmail, totalWhatsapp, totalSms, intervalInWeeks) 
	on 
	(
		target.pricingId = source.pricingId
	)
when matched then 
	update
		set [pricingId]       = source.[pricingId]      
		  , [pricingCode]	  = source.[pricingCode]	 
		  , [amount]		  = source.[amount]		
		  , [description]	  = source.[description]	 
		  , [totalWhatsapp]   = source.[totalWhatsapp]  
		  , [totalEmail]      = source.[totalEmail]     
		  , [totalSms]        = source.[totalSms]       
		  , [intervalInWeeks] = source.[intervalInWeeks]
		  , lastUpdate        = getutcdate()
when not matched by target then
	insert
	(
		    [pricingId]      
		  , [pricingCode]
		  , [priority]
		  , [amount]		
		  , [description]	 
		  , [totalWhatsapp]  
		  , [totalEmail]     
		  , [totalSms]       
		  , [intervalInWeeks]
	)
	values
	(
		    source.[pricingId]      
		  , source.[pricingCode]	 
		  , source.[priority]
		  , source.[amount]		
		  , source.[description]	 
		  , source.[totalWhatsapp]  
		  , source.[totalEmail]     
		  , source.[totalSms]       
		  , source.[intervalInWeeks]
	);
go
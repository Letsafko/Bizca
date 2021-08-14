merge into [bff].[bundle] as target
	using 
	(
		values   (1, 'Basic'  , 'Basic'  , 1, 10, 10,  5,  5, 12)
				,(2, 'Confort', 'Confort', 2, 20, 20, 10, 10, 12)
				,(3, 'Premium', 'Premium', 3, 30, 30, 15, 15, 12)
	) as source(bundleId, bundleCode, [description], [priority], amount, totalEmail, totalWhatsapp, totalSms, intervalInWeeks) 
	on 
	(
		target.BundleId = source.BundleId
	)
when matched then 
	update
		set [bundleId]        = source.[bundleId]      
		  , [bundleCode]	  = source.[bundleCode]	 
		  , [amount]		  = source.[amount]		
		  , [description]	  = source.[description]	 
		  , [totalWhatsapp]   = source.[totalWhatsapp]  
		  , [totalEmail]      = source.[totalEmail]     
		  , [totalSms]        = source.[totalSms]       
		  , [intervalInWeeks] = source.[intervalInWeeks]
		  , lastUpdate        = getdate()
when not matched by target then
	insert
	(
		    [bundleId]      
		  , [bundleCode]
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
		    source.[bundleId]      
		  , source.[bundleCode]	 
		  , source.[priority]
		  , source.[amount]		
		  , source.[description]	 
		  , source.[totalWhatsapp]  
		  , source.[totalEmail]     
		  , source.[totalSms]       
		  , source.[intervalInWeeks]
	);
go
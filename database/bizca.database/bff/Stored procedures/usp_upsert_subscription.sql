create procedure [bff].[usp_upsert_subscription]
	@userId	int,
	@subscriptions [bff].[subscriptionsUdt] readonly
as
begin

	delete s
	from [bff].[subscription] s
	left join @subscriptions c on c.subscriptionId = s.subscriptionId
	where s.userId = @userId and
	      c.subscriptionId is null

	update s
	set   s.[subscriptionStatusId]  = udt.[subscriptionStatusId]  
		, s.[procedureTypeId]		= udt.[procedureTypeId]		
		, s.[organismId]			= udt.[organismId]			
		, s.[bundleId]				= udt.[bundleId]				
		, s.[amount]				= udt.[amount]				
		, s.[firstName]		        = udt.[firstName]		        
		, s.[lastName]		        = udt.[lastName]		        
		, s.[phoneNumber]	        = udt.[phoneNumber]	        
		, s.[whatsapp]		        = udt.[whatsapp]		        
		, s.[email]			        = udt.[email]			        
		, s.[whatsappCounter]       = udt.[whatsappCounter]       
		, s.[totalWhatsapp]         = udt.[totalWhatsapp]         
		, s.[emailCounter]          = udt.[emailCounter]          
		, s.[totalEmail]            = udt.[totalEmail]            
		, s.[smsCounter]            = udt.[smsCounter]            
		, s.[totalSms]              = udt.[totalSms]              
		, s.[activatedChannelMask]  = udt.[activatedChannelMask]  
		, s.[confirmedChannelMask]  = udt.[confirmedChannelMask]  
		, s.[beginDate]	            = udt.[beginDate]	            
		, s.[endDate]    			= udt.[endDate]    
		, s.[lastUpdate]			= getdate()
	from [bff].[subscription] s
	join @subscriptions udt on udt.subscriptionId = s.subscriptionId
	where s.userId = @userId and
		  s.subscriptionId > 0
		
	insert into [bff].[subscription]
	(	  
		  [subscriptionCode]      
		, [subscriptionStatusId]  
		, [userId]				
		, [procedureTypeId]		
		, [organismId]			
		, [bundleId]				
		, [amount]				
		, [firstName]		        
		, [lastName]		        
		, [phoneNumber]	        
		, [whatsapp]		        
		, [email]			        
		, [whatsappCounter]       
		, [totalWhatsapp]         
		, [emailCounter]          
		, [totalEmail]            
		, [smsCounter]            
		, [totalSms]              
		, [activatedChannelMask]  
		, [confirmedChannelMask]  
		, [beginDate]	            
		, [endDate]        
	)
	select  
			[subscriptionCode]      
		, [subscriptionStatusId]  
		, @userId				
		, [procedureTypeId]		
		, [organismId]			
		, [bundleId]				
		, [amount]				
		, [firstName]		        
		, [lastName]		        
		, [phoneNumber]	        
		, [whatsapp]		        
		, [email]			        
		, [whatsappCounter]       
		, [totalWhatsapp]         
		, [emailCounter]          
		, [totalEmail]            
		, [smsCounter]            
		, [totalSms]              
		, [activatedChannelMask]  
		, [confirmedChannelMask]  
		, [beginDate]	            
		, [endDate]        
	from @subscriptions
	where subscriptionId = 0

end

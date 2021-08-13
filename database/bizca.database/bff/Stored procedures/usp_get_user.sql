create procedure [bff].[usp_get_user]
	@externalUserId nvarchar(20)
as
begin
	
	declare @userId int;
	
	select @userId = userId from [bff].[user] 
	where externalUserId = @externalUserId

	select dto = 'user'
	select 
		  [userId]		        
		, [roleId]
		, [externalUserId]	        
		, [civilityId]	            
		, [firstName]				    
		, [lastName]			        
		, [phoneNumber]			    
		, [whatsapp]			        
		, [email]						
		, [channelConfirmationStatus] 
		, [channelActivationStatus] 
		, [rowversion]
	from [bff].[user]
	where [userId] = @userId

	select dto = 'subscriptions'
	select 
		  [subscriptionId]		
		, [subscriptionStatusId]
		, [subscriptionCode]
		, [amount]			
		, [isFreeze]
		, [firstName]		        
		, [lastName]		        
		, [whatsappCounter]       
		, [totalWhatsapp]         
		, [whatsapp]		        
		, [emailCounter]          
		, [totalEmail]            
		, [email]			        
		, [smsCounter]            
		, [totalSms]              
		, [phoneNumber]	        
		, [activatedChannelMask]  
		, [confirmedChannelMask]  
		, [beginDate]	            
		, [endDate]             
		, b.*
		, p.*
	from [bff].[subscription] s
	outer apply fn_getById_procedure(s.procedureTypeId, s.organismId) p
	outer apply fn_getById_bundle(s.bundleId) b
	where [userId] = @userId

end

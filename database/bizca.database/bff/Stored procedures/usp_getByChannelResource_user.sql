create procedure [bff].[usp_getByChannelResource_user]
	@email nvarchar(50) = null,
	@phone nvarchar(20) = null
as
begin
	
	declare @userId int;
	if @email is not null
	begin
		select @userId = userId from [bff].[user] 
		where email = @email
	end
	else if @phone is not null
	begin
		select @userId = userId from [bff].[user] 
		where phoneNumber = @phone
	end
	else
	begin
		raiserror ('at least one of email or phone should be not null', 16, 1);
		return;
	end

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
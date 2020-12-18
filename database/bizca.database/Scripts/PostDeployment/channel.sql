merge into [ref].[channel] as target
	using 
	(
		values   (0,'None')
				,(1,'Sms')
				,(2,'Email')
				,(3,'Whatsapp')
				,(4,'Messenger')
	) as source(channelId, channelCode) on target.channelId = source.channelId
when matched then 
	update
		set channelCode = source.channelCode,
			lastUpdate  = getutcdate()
when not matched by target then
	insert
	(
		channelId, 
		channelCode
	)
	values
	(
		source.channelId, 
		source.channelCode
	);
go
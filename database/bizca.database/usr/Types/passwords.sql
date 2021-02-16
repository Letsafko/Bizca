create type [usr].[passwords] as table
(
	[userId]	     int not null,
	[active]		 bit not null,
	[securityStamp]	 varchar(200) not null,
	[passswordHash]	 varchar(200) not null 
)
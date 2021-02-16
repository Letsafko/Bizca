create type [usr].[addresses] as table
(
	[addressId]      int not null,
	[active]		 bit not null,
	[addressName]    varchar(100) null,
	[city]           varchar(100) null,
	[zipcode]		 varchar(10) null,
	[street]		 varchar(255) null,
	[countryId]      smallint not null
)
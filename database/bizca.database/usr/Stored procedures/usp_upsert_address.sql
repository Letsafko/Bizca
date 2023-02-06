create procedure [usr].[usp_upsert_address]
	@addresses  [usr].[addresses] readonly
as
begin

update s
set s.[active]		= t.[active]
  , s.[addressName] = t.[addressName]
  , s.[city]		= t.[city]
  , s.[zipcode]		= t.[zipcode]
  , s.[street]		= t.[street]
  , s.[countryId]	= t.[countryId]
    from [usr].[address] s
	join @addresses t on t.[userId]    = s.[userId] and
    t.[addressId] = s.[addressId]

insert into [usr].[address]
(	  [userId]
    , [active]
    , [addressName]
    , [city]
    , [zipcode]
    , [street]
    , [countryId]
)
select
    [userId]
        , [active]
        , [addressName]
        , [city]
        , [zipcode]
        , [street]
        , [countryId]
from @addresses
where addressId = 0 and active = 1

end

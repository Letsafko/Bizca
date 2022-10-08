create procedure [usr].[usp_isExists_user]
	  @externalUserId	  varchar(10) 
	, @partnerId		  smallint
as
begin

select 1
from [usr].[user]
where externalUserId = @externalUserId
  and
    partnerId = @partnerId

end
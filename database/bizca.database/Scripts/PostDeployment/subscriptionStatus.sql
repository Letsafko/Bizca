merge into [bff].[subscriptionStatus] as target
    using
    (
        values (1,'Pending','Pending')
              ,(2,'PaymentSubmitted','PaymentSubmitted')
              ,(3,'Activated','Activated')
              ,(4,'Deactivated','Deactivated')
              ,(5,'Expired','Expired')
    ) as source (subscriptionStatusId, subscriptionStatusCode, subscriptionStatusName)
    on
    (
        target.subscriptionStatusId = source.subscriptionStatusId
    )
    when matched then
    update
        set subscriptionStatusName = source.subscriptionStatusName,
            subscriptionStatusCode = source.subscriptionStatusCode,
            lastUpdate = getdate()
    when not matched by target then
    insert
    (
        subscriptionStatusId,
        subscriptionStatusCode,
        subscriptionStatusName
    )
    values
    (
          source.subscriptionStatusId
        , source.subscriptionStatusCode
        , source.subscriptionStatusName
    );
go
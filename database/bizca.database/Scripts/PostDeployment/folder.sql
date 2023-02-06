declare @folder table
(
	[folderId]		smallint not null,
	[defaultListId] smallint not null,
	[partnerId]		smallint not null,
	[name]			varchar(30) not null
)

declare @environment varchar(30)  = '$(Environment)'
if @environment in ('Dev','Integration', 'AzureIntegration')
begin
    insert into @folder values (10, 14, 1, 'bizca-integration')
end
else if @environment in ('Qualification', 'AzureQualification')
begin
    insert into @folder values (11, 16, 1, 'bizca-qualification')
end
else if @environment in ('Production', 'AzureProduction')
begin
    insert into @folder values (12, 15, 1, 'bizca-production')
end

merge into [bff].[folder] as target
    using @folder as source on target.[folderId] = source.[folderId]
    when matched then
    update
        set [defaultListId] = source.[defaultListId],
            [partnerId] = source.[partnerId],
            [name] = source.[name]
    when not matched by target then
    insert
    (
        [folderId],
        [defaultListId],
        [partnerId],
        [name]
    )
    values
    (
        source.[folderId],
        source.[defaultListId],
        source.[partnerId],
        source.[name]
    );
go
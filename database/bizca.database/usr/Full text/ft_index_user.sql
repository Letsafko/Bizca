create
fulltext index on [usr].[user](
	[lastName] language [english],
	[firstName] language [english]
)
key index [pk_user] on ([ft_catalog_user], filegroup [PRIMARY])
with (change_tracking = auto, stoplist = system)

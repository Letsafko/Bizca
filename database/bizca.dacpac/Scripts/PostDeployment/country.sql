﻿	declare	@country table (
		[countryId]	    smallint not null,
		[countryCode]	varchar(2) not null,
		[description]	varchar(50) not null
	)

	insert into @country ([countryId], [description], [countryCode])
	values   (1,'Afghanistan','AF')
			,(2,'Aland Islands','AX')
			,(3,'Albania','AL')
			,(4,'Algeria','DZ')
			,(5,'American Samoa','AS')
			,(6,'Andorra','AD')
			,(7,'Angola','AO')
			,(8,'Anguilla','AI')
			,(9,'Antarctica','AQ')
			,(10,'Antigua and Barbuda','AG')
			,(11,'Argentina','AR')
			,(12,'Armenia','AM')
			,(13,'Aruba','AW')
			,(14,'Australia','AU')
			,(15,'Austria','AT')
			,(16,'Azerbaijan','AZ')
			,(17,'Bahamas','BS')
			,(18,'Bahrain','BH')
			,(19,'Bangladesh','BD')
			,(20,'Barbados','BB')
			,(21,'Belarus','BY')
			,(22,'Belgium','BE')
			,(23,'Belize','BZ')
			,(24,'Benin','BJ')
			,(25,'Bermuda','BM')
			,(26,'Bhutan','BT')
			,(27,'Bolivia','BO')
			,(28,'Bonaire, Sint Eustatius and Saba','BQ')
			,(29,'Bosnia and Herzegovina','BA')
			,(30,'Botswana','BW')
			,(31,'Bouvet Island','BV')
			,(32,'Brazil','BR')
			,(33,'British Indian Ocean Territory','IO')
			,(34,'Brunei','BN')
			,(35,'Bulgaria','BG')
			,(36,'Burkina Faso','BF')
			,(37,'Burundi','BI')
			,(38,'Cambodia','KH')
			,(39,'Cameroon','CM')
			,(40,'Canada','CA')
			,(41,'Cape Verde','CV')
			,(42,'Cayman Islands','KY')
			,(43,'Central African Republic','CF')
			,(44,'Chad','TD')
			,(45,'Chile','CL')
			,(46,'China','CN')
			,(47,'Christmas Island','CX')
			,(48,'Cocos (Keeling) Islands','CC')
			,(49,'Colombia','CO')
			,(50,'Comoros','KM')
			,(51,'Congo','CG')
			,(52,'Cook Islands','CK')
			,(53,'Costa Rica','CR')
			,(54,'Ivory Coast','CI')
			,(55,'Croatia','HR')
			,(56,'Cuba','CU')
			,(57,'Curacao','CW')
			,(58,'Cyprus','CY')
			,(59,'Czech Republic','CZ')
			,(60,'Democratic Republic of the Congo','CD')
			,(61,'Denmark','DK')
			,(62,'Djibouti','DJ')
			,(63,'Dominica','DM')
			,(64,'Dominican Republic','DO')
			,(65,'Ecuador','EC')
			,(66,'Egypt','EG')
			,(67,'El Salvador','SV')
			,(68,'Equatorial Guinea','GQ')
			,(69,'Eritrea','ER')
			,(70,'Estonia','EE')
			,(71,'Ethiopia','ET')
			,(72,'Falkland Islands (Malvinas)','FK')
			,(73,'Faroe Islands','FO')
			,(74,'Fiji','FJ')
			,(75,'Finland','FI')
			,(76,'France','FR')
			,(77,'French Guiana','GF')
			,(78,'French Polynesia','PF')
			,(79,'French Southern Territories','TF')
			,(80,'Gabon','GA')
			,(81,'Gambia','GM')
			,(82,'Georgia','GE')
			,(83,'Germany','DE')
			,(84,'Ghana','GH')
			,(85,'Gibraltar','GI')
			,(86,'Greece','GR')
			,(87,'Greenland','GL')
			,(88,'Grenada','GD')
			,(89,'Guadaloupe','GP')
			,(90,'Guam','GU')
			,(91,'Guatemala','GT')
			,(92,'Guernsey','GG')
			,(93,'Guinea','GN')
			,(94,'Guinea-Bissau','GW')
			,(95,'Guyana','GY')
			,(96,'Haiti','HT')
			,(97,'Heard Island and McDonald Islands','HM')
			,(98,'Honduras','HN')
			,(99,'Hong Kong','HK')
			,(100,'Hungary','HU')
			,(101,'Iceland','IS')
			,(102,'India','IN')
			,(103,'Indonesia','ID')
			,(104,'Iran','IR')
			,(105,'Iraq','IQ')
			,(106,'Ireland','IE')
			,(107,'Isle of Man','IM')
			,(108,'Israel','IL')
			,(109,'Italy','IT')
			,(110,'Jamaica','JM')
			,(111,'Japan','JP')
			,(112,'Jersey','JE')
			,(113,'Jordan','JO')
			,(114,'Kazakhstan','KZ')
			,(115,'Kenya','KE')
			,(116,'Kiribati','KI')
			,(117,'Kosovo','XK')
			,(118,'Kuwait','KW')
			,(119,'Kyrgyzstan','KG')
			,(120,'Laos','LA')
			,(121,'Latvia','LV')
			,(122,'Lebanon','LB')
			,(123,'Lesotho','LS')
			,(124,'Liberia','LR')
			,(125,'Libya','LY')
			,(126,'Liechtenstein','LI')
			,(127,'Lithuania','LT')
			,(128,'Luxembourg','LU')
			,(129,'Macao','MO')
			,(130,'Macedonia','MK')
			,(131,'Madagascar','MG')
			,(132,'Malawi','MW')
			,(133,'Malaysia','MY')
			,(134,'Maldives','MV')
			,(135,'Mali','ML')
			,(136,'Malta','MT')
			,(137,'Marshall Islands','MH')
			,(138,'Martinique','MQ')
			,(139,'Mauritania','MR')
			,(140,'Mauritius','MU')
			,(141,'Mayotte','YT')
			,(142,'Mexico','MX')
			,(143,'Micronesia','FM')
			,(144,'Moldava','MD')
			,(145,'Monaco','MC')
			,(146,'Mongolia','MN')
			,(147,'Montenegro','ME')
			,(148,'Montserrat','MS')
			,(149,'Morocco','MA')
			,(150,'Mozambique','MZ')
			,(151,'Myanmar (Burma)','MM')
			,(152,'Namibia','NA')
			,(153,'Nauru','NR')
			,(154,'Nepal','NP')
			,(155,'Netherlands','NL')
			,(156,'New Caledonia','NC')
			,(157,'New Zealand','NZ')
			,(158,'Nicaragua','NI')
			,(159,'Niger','NE')
			,(160,'Nigeria','NG')
			,(161,'Niue','NU')
			,(162,'Norfolk Island','NF')
			,(163,'North Korea','KP')
			,(164,'Northern Mariana Islands','MP')
			,(165,'Norway','NO')
			,(166,'Oman','OM')

	merge into [ref].[country] as target
		using @country source on target.countryId = source.countryId
	when matched then 
		update
			set countryCode = source.countryCode,
				description = source.description,
			    lastUpdate  = getutcdate()
	when not matched by target then
		insert
		(
			countryId, 
			countryCode, 
			description
		)
		values
		(
			source.countryId, 
			source.countryCode, 
			source.description
		);
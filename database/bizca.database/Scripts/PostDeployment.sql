/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\PostDeployment\role.sql
:r .\PostDeployment\channel.sql
:r .\PostDeployment\partner.sql
:r .\PostDeployment\folder.sql
:r .\PostDeployment\country.sql 
:r .\PostDeployment\pricing.sql
:r .\PostDeployment\civility.sql
:r .\PostDeployment\organism.sql
:r .\PostDeployment\procedureType.sql
:r .\PostDeployment\emailTemplateType.sql
:r .\PostDeployment\procedure.sql
:r .\PostDeployment\language.sql
:r .\PostDeployment\emailTemplate.sql
:r .\PostDeployment\economicActivity.sql
:r .\PostDeployment\subscriptionStatus.sql
USE [UniversalGym]
GO

/****** Object:  StoredProcedure [dbo].[getUserDataForMigration]    Script Date: 5/23/2015 10:56:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[getUserDataForMigration]
	
AS
BEGIN
	SELECT        Users.UserId, ProfileInfo.FirstName AS FirstName, 
                         ProfileInfo.LastName AS LastName,
						 aspnet_Membership.Email

FROM            Users 
					INNER JOIN ProfileInfo ON Users.UserId = ProfileInfo.UserId 
					INNER JOIN aspnet_Membership ON Users.UserGuid= aspnet_Membership.UserId

						
END

GO



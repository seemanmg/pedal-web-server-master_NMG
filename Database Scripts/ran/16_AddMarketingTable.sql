USE [UniversalGym]
GO

/****** Object:  Table [dbo].[UsersFromMarketingSite]    Script Date: 7/12/2015 10:45:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UsersFromMarketingSite](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](250) NULL,
	[zip] [nvarchar](10) NULL,
	[AreWeInZip] [bit] NOT NULL,
	[city] [nvarchar](250) NULL,
 CONSTRAINT [PK_UsersFromMarketingSite] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



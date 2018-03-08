USE [UniversalGym]
GO

/****** Object:  Table [dbo].[RefenceTracks]    Script Date: 11/1/2015 2:31:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RefenceTracks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceCode] [nvarchar](max) NULL,
	[When] [datetime] NULL,
 CONSTRAINT [PK_RefenceTracks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


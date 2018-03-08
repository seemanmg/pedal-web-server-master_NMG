/****** Object:  Table [dbo].[ReferalLinksClicked]    Script Date: 10/18/2015 8:08:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReferalLinksClicked](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CodeClicked] [nchar](25) NULL,
	[WhenClicked] [datetime] NULL,
 CONSTRAINT [PK_ReferalLinksClicked] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



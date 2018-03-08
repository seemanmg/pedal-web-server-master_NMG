GO

/****** Object:  Table [dbo].[GiveCredits]    Script Date: 11/14/2015 8:15:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GiveCredits](
	[GiveCreditsId] [int] IDENTITY(1,1) NOT NULL,
	[AmountToGiveNewUser] [int] NOT NULL,
	[UserIdToGiveCredits] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[UserIdWhoGiveCredits] [int] NULL,
	[WasClaimed] [bit] NOT NULL CONSTRAINT [DF_GiveCredits_WasClaimed]  DEFAULT ((0)),
	[ClaimedOn] [datetime] NULL,
	[AmountToGiveReferer] [int] NULL,
 CONSTRAINT [PK_GiveCredits] PRIMARY KEY CLUSTERED 
(
	[GiveCreditsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GiveCredits]  WITH NOCHECK ADD  CONSTRAINT [FK_GiveCredits_Users] FOREIGN KEY([UserIdToGiveCredits])
REFERENCES [dbo].[Users] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GiveCredits] CHECK CONSTRAINT [FK_GiveCredits_Users]
GO

ALTER TABLE [dbo].[GiveCredits]  WITH NOCHECK ADD  CONSTRAINT [FK_GiveCredits_Users1] FOREIGN KEY([UserIdWhoGiveCredits])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[GiveCredits] CHECK CONSTRAINT [FK_GiveCredits_Users1]
GO



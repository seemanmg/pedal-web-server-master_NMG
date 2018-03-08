
/****** Object:  Table [dbo].[GymPasses]    Script Date: 4/20/2015 8:54:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GymPasses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GymId] [int] NOT NULL,
	[DateBought] [datetime] NOT NULL,
	[DateUsed] [datetime] NULL,
	[IsUsed] [bit] NOT NULL,
	[DateExpired] [datetime] NULL,
 CONSTRAINT [PK_GymPasses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GymPasses]  WITH CHECK ADD  CONSTRAINT [FK_GymPasses_Gyms] FOREIGN KEY([GymId])
REFERENCES [dbo].[Gyms] ([GymId])
GO

ALTER TABLE [dbo].[GymPasses] CHECK CONSTRAINT [FK_GymPasses_Gyms]
GO

ALTER TABLE [dbo].[GymPasses]  WITH CHECK ADD  CONSTRAINT [FK_GymPasses_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[GymPasses] CHECK CONSTRAINT [FK_GymPasses_Users]
GO



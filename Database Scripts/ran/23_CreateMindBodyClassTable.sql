USE [UniversalGym]
GO

/****** Object:  Table [dbo].[MindBodyClassSignUpObject]    Script Date: 10/3/2015 4:34:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MindBodyClassSignUpObject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MindBodyId] [nvarchar](max) NOT NULL,
	[GymId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[ClassScheduleId] [int] NOT NULL,
	[ClassName] [nvarchar](max) NULL,
	[ClassTime] [nvarchar](max) NULL,
	[NoShow] [bit] NOT NULL,
	[DateChargedForNoShow] [datetime] NULL,
	[ClassDate] [datetime] NOT NULL,
	[DateSignedUp] [datetime] NOT NULL,
	[StacktiveUserId] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[MindBodyClassSignUpObject] ADD  CONSTRAINT [DF_MindBodyClassSignUpObject_NoShow]  DEFAULT ((0)) FOR [NoShow]
GO

ALTER TABLE [dbo].[MindBodyClassSignUpObject]  WITH CHECK ADD  CONSTRAINT [FK_MindBodyClassSignUpObject_Gyms] FOREIGN KEY([GymId])
REFERENCES [dbo].[Gyms] ([GymId])
GO

ALTER TABLE [dbo].[MindBodyClassSignUpObject] CHECK CONSTRAINT [FK_MindBodyClassSignUpObject_Gyms]
GO

ALTER TABLE [dbo].[MindBodyClassSignUpObject]  WITH CHECK ADD  CONSTRAINT [FK_MindBodyClassSignUpObject_Users] FOREIGN KEY([StacktiveUserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[MindBodyClassSignUpObject] CHECK CONSTRAINT [FK_MindBodyClassSignUpObject_Users]
GO


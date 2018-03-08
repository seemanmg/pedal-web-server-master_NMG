USE [UniversalGym]
GO

/****** Object:  Table [dbo].[SubscriptionItems]    Script Date: 1/4/2015 7:47:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubscriptionItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateToPay] [date] NULL,
	[AmountToCharge] [int] NULL,
	[PaymentInfoId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_SubscriptionItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubscriptionItems]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionItems_PaymentInfo] FOREIGN KEY([PaymentInfoId])
REFERENCES [dbo].[PaymentInfo] ([PaymentInfoId])
GO

ALTER TABLE [dbo].[SubscriptionItems] CHECK CONSTRAINT [FK_SubscriptionItems_PaymentInfo]
GO

ALTER TABLE [dbo].[SubscriptionItems]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionItems_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[SubscriptionItems] CHECK CONSTRAINT [FK_SubscriptionItems_Users]
GO



USE [UniversalGym]
GO
/****** Object:  StoredProcedure [dbo].[sp_Users_CheckInToGym]    Script Date: 1/24/2015 10:12:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Adriana Mirica>
-- Create date: <Create Date,,>
-- Description:	<check in functionality for mobile and gym web>
-- =============================================
ALTER PROCEDURE [dbo].[sp_Users_CheckInToGym]
	-- Add the parameters for the stored procedure here
	@UserId INT,
	@GymId INT,
	@CreditsNumber DECIMAL(18,0),
	@CreditDollarVal DECIMAL(18,0),
	@IsMobile BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @IsCurrentPLanInfinite bit;
	DECLARE @UserSubscriptionHistoryId int;

    -- Insert statements for procedure here
	BEGIN TRY
		  BEGIN TRANSACTION
		  
		   
		   INSERT INTO dbo.CheckInHistory
		           ( UserId ,
		             GymId ,
		             DateTime ,
		             CreditsCharge,
					 CreditDollarValue
		           )
		   VALUES  ( @UserId , -- UserId - int
		             @GymId, -- GymId - int
		             GETDATE() , -- DateTime - datetime
		             @CreditsNumber,  -- CreditsCharge - tinyint
					 @CreditDollarVal
		           )
		   
		   -- add invoice here added by MD : 1/24/2015
			DECLARE @today varchar(10)
			set @today = CONVERT (date, CURRENT_TIMESTAMP) 
			DECLARE @invoiceNumber varchar(20)
			set @invoiceNumber  = 'UG_'+ CAST(@GymId AS varchar) +'_'+ @today 

			INSERT INTO Invoices
							(GymId, Amount, InvoiceNumber, InvoiceDate,Terms)
			VALUES        (@GymId,@CreditDollarVal,@invoiceNumber ,GETDATE(), 'Empty')
		   

		   SELECT TOP 1 @IsCurrentPLanInfinite = IsInfinite, @UserSubscriptionHistoryId = UserSubscriptionHistoryId FROM dbo.UserSubscriptionHistory WHERE UserId=@UserId
				ORDER BY SubscriptionDate DESC 
		   
		   --if the current plan is infinite don't change the used credits
		   IF @IsCurrentPLanInfinite=0
			  BEGIN
				  UPDATE dbo.UserSubscriptionHistory SET UsedCredits= UsedCredits+@CreditsNumber 
					WHERE UserSubscriptionHistoryId= @UserSubscriptionHistoryId
			   
			  END
		   
		    
		  -- UPDATE dbo.UserSubscriptionHistory SET UsedCredits= UsedCredits+@CreditsNumber 
				--WHERE UserSubscriptionHistoryId= (SELECT TOP 1 UserSubscriptionHistoryId  FROM dbo.UserSubscriptionHistory WHERE UserId=@UserId
				--ORDER BY SubscriptionDate DESC)
				
		   
		   -- If the statement succeeds, commit the transaction.
		   COMMIT TRANSACTION;
	 END TRY
	 BEGIN CATCH
		  DECLARE @ErrorMessage VARCHAR(4000)
		  
		  SET @ErrorMessage = 'ErrorProcedure: ' + ISNULL(ERROR_PROCEDURE(), '') + ' Line: ' + CAST(ERROR_LINE() AS VARCHAR(10)) + ' Message: ' + ERROR_MESSAGE()

		   ROLLBACK TRANSACTION;

		   -- ADD YOUR CODE HERE --
		   
		  --Save an invalid scan record only for mobile--
		  IF @IsMobile=1
		  BEGIN
			  INSERT INTO dbo.InvalidScan
					  ( UserId, GymId, Date, CreditsCharge )
			  VALUES  ( @UserId, -- UserId - int
						@GymId, -- GymId - int
						GETDATE(), -- Date - datetime
						@CreditsNumber  -- CreditsCharge - tinyint
						)
		   
		  END
		  --END Save an invalid scan record only for mobile--
		  
		  --Save LOG--
		  INSERT INTO dbo.Logs
		          ( LogMessage, InsertDate )
		  VALUES  ( @ErrorMessage, -- LogMessage - nvarchar(max)
		            GETDATE() -- InsertDate - datetime
		            )
		 --END Save LOG--
		 
		  RAISERROR (@ErrorMessage, 16, 1)  
	END CATCH
END

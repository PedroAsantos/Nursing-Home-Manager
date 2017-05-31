CREATE PROCEDURE [dbo].[sp_updatePATIENT]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Sex	varchar(1),
	@Phone	INT,
	@Age	INT,
	@Check_in	DATE,
	@Check_out	DATE,
	@Authorization_to_leave BIT,
	@E_BedNumber	INT,
	@Entry_Date	DATE,
	@Exit_Date	DATE 
AS
BEGIN
	BEGIN TRANSACTION;
		BEGIN TRY
			UPDATE exemplo1.PATIENT
			SET		NIF = @NIF,
					Name = @Name,
					Sex = @Sex,
					Phone = @Phone,
					Age = @Age,
					Check_in = @Check_in,
					Check_out = @Check_out,
					Authorization_to_leave = @Authorization_to_leave,
					E_BedNumber = @E_BedNumber,
					Entry_Date = @Entry_Date,
					Exit_Date = @Exit_Date
			WHERE NIF=@NIF
		END TRY

		BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;
	IF @@TRANCOUNT == 0
		COMMIT TRANSACTION;
END

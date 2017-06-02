CREATE PROCEDURE dbo.updateDoctor
	@NIF	varchar(9),
	@Name	varchar(30),
	@Phone	INT,
	@Address varchar(30)
AS
BEGIN
	BEGIN TRANSACTION;
			UPDATE exemplo1.DOCTOR
			SET		NIF = @NIF,
					Name = @Name,
					Phone = @Phone,
					Address = @Address
			WHERE NIF=@NIF
	COMMIT TRANSACTION;
END

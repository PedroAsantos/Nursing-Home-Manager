CREATE PROCEDURE dbo.updateSeriousness
	@E_NIF			varchar(9),
	@E_Name			varchar(30),
	@Seriousness	INT
AS
BEGIN
	BEGIN TRANSACTION
			UPDATE exemplo1.DIAGNOSED
			SET	Seriousness = @Seriousness
			WHERE (E_NIF=@E_NIF and E_Name = @E_Name)
	COMMIT TRANSACTION;
END
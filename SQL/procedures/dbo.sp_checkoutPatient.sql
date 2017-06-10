CREATE PROCEDURE dbo.sp_checkoutPatient
	@NIF	varchar(9)
AS
BEGIN
			UPDATE exemplo1.Patient
			SET		Check_out = Convert(date, getdate())  
			WHERE NIF=@NIF
END

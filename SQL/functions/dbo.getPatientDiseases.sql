CREATE FUNCTION dbo.getPatientDiseases(@NIF INT)
RETURNS TABLE
AS
	RETURN
		SELECT E_name,Seriousness FROM exemplo1.DIAGNOSED WHERE exemplo1.DIAGNOSED.E_NIF = @NIF
GO
CREATE FUNCTION dbo.getDoctors()
RETURNS TABLE
AS
	RETURN(
			select NIF,Name,Phone,Address from exemplo1.DOCTOR
		  )		
GO
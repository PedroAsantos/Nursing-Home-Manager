CREATE FUNCTION dbo.getHumanTypes()
RETURNS TABLE
AS
	RETURN(
			select Designation from exemplo1.Type
		  )		
GO
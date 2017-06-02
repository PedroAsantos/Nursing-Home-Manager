CREATE FUNCTION dbo.getHumanTypes()
RETURNS TABLE
AS
	RETURN(
			select Designation,Id from exemplo1.Type
		  )		
GO


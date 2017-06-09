CREATE FUNCTION dbo.getHumanTypes()
RETURNS TABLE
AS
	RETURN(
			select Designation,Id from exemplo1.Type WHERE Disable = 0
		  )		
GO


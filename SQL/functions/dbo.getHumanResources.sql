CREATE FUNCTION dbo.getHumanResources()
RETURNS TABLE
AS
	RETURN(
		select * from exemplo1.HumanResources
		join exemplo1.Type on E_IDType = ID
		  )		
GO
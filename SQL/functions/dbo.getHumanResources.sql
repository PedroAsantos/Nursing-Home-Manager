CREATE FUNCTION dbo.getHumanResources()
RETURNS TABLE
AS
	RETURN(
			select NIF,Name,Phone,Address,Salary,StartDate,Designation from exemplo1.HumanResources
			join exemplo1.Type on E_IDType = ID
		  )		
GO
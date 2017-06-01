CREATE PROCEDURE [dbo].[sp_insertHumanResources]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Phone	INT,
	@Address varchar(30),
	@Salary	INT,
	@Start_Date	DATE,
	@E_IDType INT
 
AS
BEGIN
		if  (Exists(Select NIF from exemplo1.HUMANRESOURCES where NIF=@NIF ))
					return
		else
			 INSERT INTO exemplo1.HUMANRESOURCES values (
					@NIF,
					@Name,
					@Phone,
					@Address,
					@Salary,
					@Start_Date,
					@E_IDType
				  )
		return
end

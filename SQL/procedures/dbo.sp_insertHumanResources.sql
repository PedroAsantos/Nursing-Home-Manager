CREATE PROCEDURE [dbo].[sp_insertHumanResources]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Phone	INT,
	@Address varchar(30),
	@Salary	INT,
	@Start_Date	DATE = null,
	@E_IDType INT
 
AS
BEGIN
		if  (Exists(Select NIF from exemplo1.HUMANRESOURCES where NIF=@NIF ))
		begin
			UPDATE exemplo1.HUMANRESOURCES
			SET NIF = @NIF,
			Name = @Name,
			Phone = @Phone,
			Address = @Address,
			Salary = @Salary,
			StartDate = @Start_Date,
			E_IDType = @E_IDType
			where NIF = @NIF
		end
		else
		begin
			 INSERT INTO exemplo1.HUMANRESOURCES values (
					@NIF,
					@Name,
					@Phone,
					@Address,
					@Salary,
					@Start_Date,
					@E_IDType
				  )
		end
		return
end

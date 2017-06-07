CREATE PROCEDURE dbo.sp_newDoctor
	@NIF	varchar(9),
	@Name	varchar(30),
	@Phone	INT,
	@Address varchar(30)
 
AS
BEGIN
		if  (Exists(Select NIF from exemplo1.DOCTOR where NIF=@NIF ))
		begin
			UPDATE exemplo1.HUMANRESOURCES
			SET NIF = @NIF,
			Name = @Name,
			Phone = @Phone,
			Address = @Address
			where NIF = @NIF
		end
		else
		begin
			 INSERT INTO exemplo1.DOCTOR values (
					@NIF,
					@Name,
					@Phone,
					@Address
				  )
		end
		return
end

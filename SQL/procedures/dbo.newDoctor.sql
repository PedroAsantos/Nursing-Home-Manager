CREATE PROCEDURE dbo.newDoctor ( @NIF varchar(9), @Name varchar(30), @Phone int,  @Address varchar(30))
as
begin
		if  (Exists(Select NIF from exemplo1.DOCTOR where NIF=@NIF ))
			RAISERROR('O Doctor ja existe!',16,1);

		INSERT INTO exemplo1.DOCTOR values(
				@NIF,
				@Name,
				@Phone,
				@Address
		)
		RETURN
END
GO

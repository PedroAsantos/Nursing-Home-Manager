CREATE PROCEDURE dbo.newDiagnosed (@E_Name varchar(30), @E_NIF varchar(9), @Seriousness int, @Disable BIT)
as
begin
		if  (Exists(Select E_NIF, E_Name from exemplo1.DIAGNOSED where E_NIF=@E_NIF AND E_Name = @E_Name ))
			RAISERROR('Diagnosed já existente!',16,1);
		else
		begin
			if  (not Exists(Select Name from exemplo1.DISEASE where Name=@E_Name ))
				INSERT INTO exemplo1.DISEASE values(
						@E_Name
				)
			if  (Exists(Select NIF from exemplo1.PATIENT where NIF=@E_NIF ))
				INSERT INTO exemplo1.DIAGNOSED values(
						@E_Name,
						@E_NIF,
						@Seriousness,
						@Disable
				)
			else
				RAISERROR('O NIF do Patient nao existe!',16,1);
		end
		RETURN
END
GO

CREATE PROCEDURE dbo.newDiagnosed (@E_Name varchar(30), @E_NIF varchar(9), @Seriousness int)
as
begin

		if  (Exists(Select Name from exemplo1.DISEASE where Name=@E_Name ))
				if  (Exists(Select NIF from exemplo1.PATIENT where NIF=@E_NIF ))
					INSERT INTO exemplo1.DIAGNOSED values(
							@E_Name,
							@E_NIF,
							@Seriousness
					)
		RETURN
END
GO
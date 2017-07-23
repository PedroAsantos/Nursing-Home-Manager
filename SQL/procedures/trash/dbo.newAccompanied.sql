CREATE PROCEDURE dbo.newAccompanied (@NIF_Patient varchar(9), @NIF_Doctor varchar(9))
as
begin
		if  (not Exists(Select NIF from exemplo1.PATIENT where NIF=@NIF_Patient ))
			RAISERROR('O Patient nao existe!',16,1)
			return

		if  (not Exists(Select NIF from exemplo1.DOCTOR where NIF=@NIF_Doctor ))
			RAISERROR('O Doctor nao existe!',16,1);
			return

		if  (Exists(Select NIF_Patient from exemplo1.ACCOMPANIED where NIF_Patient=@NIF_Patient ))
			RAISERROR('O Patient ja possui um doctor!',16,1);
			return

		INSERT INTO exemplo1.ACCOMPANIED values(
				@NIF_Patient,
				@NIF_Doctor
		)
		RETURN
END
GO





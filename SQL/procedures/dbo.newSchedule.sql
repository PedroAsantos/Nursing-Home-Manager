CREATE PROCEDURE dbo.newSchedule (@Day varchar(7), @Hour time, @E_NIF varchar(9))
as
begin
		if  (not Exists(Select NIF from exemplo1.PATIENT where NIF=@E_NIF ))
			RAISERROR('O Patient nao existe!',16,1)

		INSERT INTO exemplo1.SCHEDULE values(
				@Day,
				@Hour,
				@E_NIF
		)
		RETURN
END
GO

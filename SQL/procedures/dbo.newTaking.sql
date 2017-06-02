CREATE PROCEDURE dbo.newTaking (@E_Day varchar(7), @E_Hour time, @E_NIF varchar(9), @E_ID int, @Dose int)
as
begin
		if  (not Exists(Select E_NIF from exemplo1.SCHEDULE where E_NIF=@E_NIF ))
			RAISERROR('O Patient nao tem tomas scheduled!',16,1)

		INSERT INTO exemplo1.TAKING values(
				@E_Day,
				@E_Hour,
				@E_NIF,
				@E_ID,
				@Dose
		)
		RETURN
END
GO
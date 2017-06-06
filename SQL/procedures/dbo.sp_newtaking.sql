CREATE PROCEDURE dbo.sp_newTaking (@E_Day varchar(15), @E_Hour time, @E_NIF varchar(9), @medicineName varchar(30), @Dose int)
as
begin
		if  (not Exists(Select * from exemplo1.PATIENT where NIF=@E_NIF ))
			RAISERROR('O Patient não existe!',16,1)

		DECLARE @medicineID INT;
		SELECT @medicineID = id from exemplo1.medicine where Name = @medicineName;
		if @@ROWCOUNT = 0
		BEGIN
			INSERT INTO exemplo1.medicine values (@medicinename)
			SELECT @medicineID = id from exemplo1.medicine where Name = @medicineName;
		END

		SELECT * from exemplo1.Schedule Where day=@E_Day and hour=@E_Hour and E_NIF = @E_NIF;
		if @@ROWCOUNT = 0
		BEGIN
			INSERT INTO exemplo1.Schedule values (@E_Day,@E_Hour, @E_NIF);
		END
		DECLARE @DISABLE BIT;
		SET @DISABLE = 0;
		IF(Exists(Select * from exemplo1.TAKING WHere E_day = @E_Day AND E_Hour = @E_Hour AND E_NIF = @E_NIF AND E_ID = @medicineID ))
		Begin
			UPDATE exemplo1.TAKING SET  Dose = @Dose WHere E_day = @E_Day AND E_Hour = @E_Hour AND E_NIF = @E_NIF AND E_ID = @medicineID;
		END
		ELSE
		BEGIN
				INSERT INTO exemplo1.TAKING values(
					@E_Day,
					@E_Hour,
					@E_NIF,
					@medicineID,
					@Dose,
					@Disable
			)
		END
		RETURN
END
GO
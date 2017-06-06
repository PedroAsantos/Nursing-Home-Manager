CREATE PROCEDURE dbo.sp_newSchedule (@NIF INT,@inicialDate Date, @finalDate Date=null, @beginofWorkShift Time, @endofWorkShift Time, @day varchar(30))
as
begin
		if  (not Exists(Select * from exemplo1.PATIENT where NIF=@E_NIF ))
			RAISERROR('O Patient não existe!',16,1)

		DECLARE @idShift INT;
		Select @idShift=ID FROM exemplo1.Shift WHERE BeginOfWorkShift = @beginofWorkShift AND EndOfWorkShift = @endofWorkShift AND Day = @day;
		
		if(@@ROWCOUNT = 0)
		BEGIN
			INSERT INTO exemplo1.Shift VALUES (@day, @beginofWorkShift, @endofWorkShift);
			Select @idShift=ID FROM exemplo1.Shift WHERE BeginOfWorkShift = @beginofWorkShift AND EndOfWorkShift = @endofWorkShift AND Day = @day;
		END

		if(not Exists(SELECT * FROM exemplo1.ShiftInstance WHERE E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			INSERT INTO exemplo1.ShiftInstance VALUES (@inicialDate,@finalDate,@NIF,@idShift);
			RETURN;
		END

		if(Exists(SELECT * FROM exemplo1.ShiftInstance WHERE FinalDate=null AND E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			return;  --RAISE ERROR JÁ INSERIDO HORARIO
		END
		if(Exists(SELECT * FROM exemplo1.ShiftInstance WHERE FinalDate!=null AND E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			INSERT INTO exemplo1.ShiftInstance VALUES (@inicialDate,@finalDate,@NIF,@idShift);
		END
		
		RETURN;
END
GO
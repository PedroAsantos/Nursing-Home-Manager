CREATE PROCEDURE dbo.sp_newSchedule (@NIF INT,@inicialDate Date, @finalDate Date=null, @beginofWorkShift Time, @endofWorkShift Time, @day varchar(30))
as
begin
		if  (not Exists(Select * from exemplo1.HumanResources where NIF=@NIF ))
		BEGIN
			RAISERROR('O recurso humano não existe!',16,1)
			return;
		END

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

		if(Exists(SELECT * FROM exemplo1.ShiftInstance WHERE FinalDate is NULL AND E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			RAISERROR('Já tem asssociado esse horário!',16,1)
			return;
		END
		if(Exists(SELECT * FROM exemplo1.ShiftInstance WHERE FinalDate IS NOT null AND E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			INSERT INTO exemplo1.ShiftInstance VALUES (@inicialDate,@finalDate,@NIF,@idShift);
		END
		
		RETURN;
END
GO
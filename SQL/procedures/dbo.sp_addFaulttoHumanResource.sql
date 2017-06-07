CREATE PROCEDURE dbo.sp_addFaulttoHumanResource (@NIF INT, @beginofWorkShift Time = null, @endofWorkShift Time = null, @day varchar(30))
as
begin
		if  (not Exists(Select * from exemplo1.HumanResources where NIF=@NIF ))
		BEGIN
			RAISERROR('O recurso humano não existe!',16,1)
			return;
		END


		DECLARE @idShiftInstace INT;

		SELECT @idShiftInstace = exemplo1.ShiftInstance.ID FROM (exemplo1.ShiftInstance JOIN exemplo1.Shift ON exemplo1.Shift.ID = E_IDShift) WHERE BeginOfWorkShift = @beginofWorkShift AND endofWorkShift = @endofWorkShift AND Day = @day;
		IF(@@ROWCOUNT = 0)
		BEGIN
			RAISERROR('Não existe esse turno',16,1);
			RETURN;
		END

		if(NOT EXISTS(SELECT * FROM exemplo1.Faults WHERE E_ID=@idShiftInstace AND Date = CONVERT (date, GETDATE())))
		BEGIN
			INSERT INTO exemplo1.Faults values(GETDATE(),@idShiftInstace); 
		END
		ELSE
		BEGIN
			RAISERROR('Não pode marcar mais que uma falta no mesmo dia na mesma data.',16,1)
			RETURN;
		END

	
		RETURN;
END
GO
CREATE FUNCTION dbo.getHumanResourceSchedule(@NIF Varchar(9))
RETURNS TABLE
AS
	RETURN(
			SELECT Day, BeginOfWorkShift, EndOfWorkShift,E_IDShift FROM (exemplo1.ShiftInstance JOIN exemplo1.Shift on E_IDShift= exemplo1.Shift.ID) Where E_nif = @NIF and FinalDate IS NULL
		  )		
GO
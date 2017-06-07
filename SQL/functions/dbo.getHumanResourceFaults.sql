CREATE FUNCTION dbo.getHumanResourceFaults(@NIF Varchar(9))
RETURNS TABLE
AS
	RETURN(
			SELECT exemplo1.Shift.Day, BeginOfWorkShift, EndOfWorkShift,E_IDShift, FinalDate,count(exemplo1.Faults.E_ID) as NumberOfFaults FROM (exemplo1.ShiftInstance JOIN exemplo1.Shift on  E_IDShift= exemplo1.Shift.ID) 
					LEFT OUTER JOIN exemplo1.Faults on exemplo1.Faults.E_ID = exemplo1.ShiftInstance.ID Where E_nif = @NIF GROUP BY Day, BeginOfWorkShift, EndOfWorkShift, E_IDShift,FinalDate 
		  )		
GO


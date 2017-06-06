CREATE FUNCTION dbo.getPatientMedicines(@NIF Varchar(9))
RETURNS TABLE
AS
	RETURN(
			select Name, Dose, E_Day,E_Hour,E_ID FROM (exemplo1.TAKING JOIN exemplo1.MEDICINE on E_ID=ID) Where E_nif = @NIF AND exemplo1.TAKING.Disable = 0
		  )		
GO
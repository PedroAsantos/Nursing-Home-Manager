CREATE FUNCTION dbo.getPatients()
RETURNS TABLE
AS
	RETURN(
		select NIF, Name, Sex, Phone, Age, Check_in, Check_out, Authorization_to_leave, E_BedNumber, Entry_Date, Exit_Date, RoomNumber from exemplo1.PATIENT 
		join exemplo1.BED on E_BedNumber = BedNumber
		join exemplo1.BEDROOM on E_RoomNumber = RoomNumber
		  )		
GO

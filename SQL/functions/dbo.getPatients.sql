CREATE FUNCTION dbo.getPatients()
RETURNS TABLE
AS
	RETURN(
		select exemplo1.PATIENT.NIF, exemplo1.PATIENT.Name, exemplo1.PATIENT.Sex, exemplo1.PATIENT.Phone, exemplo1.PATIENT.Age, exemplo1.PATIENT.Check_in, exemplo1.PATIENT.Check_out, Authorization_to_leave, E_BedNumber, Entry_Date, Exit_Date, RoomNumber, exemplo1.VISITOR.Name as DependentName from exemplo1.PATIENT 
		join exemplo1.BED on E_BedNumber = BedNumber
		join exemplo1.BEDROOM on E_RoomNumber = RoomNumber
		join exemplo1.DEPENDENT on E_PatientNIF = NIF
		join exemplo1.FAMILY on exemplo1.DEPENDENT.E_CC = exemplo1.FAMILY.E_CC
		join exemplo1.VISITOR on exemplo1.FAMILY.E_CC = exemplo1.VISITOR.CC
		  )		
GO

CREATE FUNCTION dbo.getPatients()
RETURNS TABLE
AS
	RETURN(
		select exemplo1.PATIENT.NIF, exemplo1.PATIENT.Name, exemplo1.PATIENT.Sex, exemplo1.PATIENT.Phone, exemplo1.PATIENT.Age, exemplo1.PATIENT.Check_in, exemplo1.PATIENT.Check_out, Authorization_to_leave, E_BedNumber, Entry_Date, Exit_Date, RoomNumber, exemplo1.VISITOR.Name as DependentName, exemplo1.VISITOR.CC as DependentCC, exemplo1.VISITOR.Address as DependentAddress, exemplo1.VISITOR.Phone as DependentPhone, exemplo1.FAMILY.Relationship  from exemplo1.PATIENT 
		JOIN exemplo1.BED on E_BedNumber = BedNumber
		JOIN exemplo1.BEDROOM on E_RoomNumber = RoomNumber
		FULL OUTER JOIN exemplo1.DEPENDENT on E_PatientNIF = NIF
		LEFT OUTER JOIN exemplo1.FAMILY on exemplo1.DEPENDENT.E_CC = exemplo1.FAMILY.E_CC
		LEFT OUTER JOIN exemplo1.VISITOR on exemplo1.FAMILY.E_CC = exemplo1.VISITOR.CC
		  )		
GO
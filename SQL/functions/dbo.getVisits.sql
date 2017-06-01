CREATE FUNCTION dbo.getVisits()
RETURNS TABLE
AS
	RETURN(
		select exemplo1.PATIENT.Name as PatientName, NIF, exemplo1.VISITOR.Name as VisitorName, CC  from exemplo1.VISITOR
			join exemplo1.VISITED on CC = E_CCVisitor
			join exemplo1.VISIT on E_IDVisit = ID
			join exemplo1.RECEIVED on exemplo1.RECEIVED.E_IDVisit = ID
			join exemplo1.PATIENT on E_NIF = NIF
		  )		
GO
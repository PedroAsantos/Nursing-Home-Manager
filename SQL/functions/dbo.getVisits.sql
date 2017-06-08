CREATE FUNCTION dbo.getVisits(@PatientNif varchar(9),@PatientName varchar(30),@VisitorName varchar(30),@VisitorCC varchar(9),@VisitorPhone INT,@RelationShip varchar(30),@Date Date,@PageNumber INT, @RowsPage INT)
RETURNS TABLE
AS
	RETURN(
		select exemplo1.PATIENT.Name as PatientName, NIF, exemplo1.VISITOR.Name as VisitorName, CC, exemplo1.VISITOR.Phone, exemplo1.VISITOR.Address,exemplo1.VISIT.Date, exemplo1.NOT_FAMILY.KinshipDegree,exemplo1.FAMILY.Relationship from exemplo1.VISITOR
			join exemplo1.VISITED on CC = E_CCVisitor
			join exemplo1.VISIT on E_IDVisit = ID
			join exemplo1.RECEIVED on exemplo1.RECEIVED.E_IDVisit = ID
			join exemplo1.PATIENT on E_NIF = NIF
			join exemplo1.NOT_FAMILY on CC = exemplo1.NOT_FAMILY.E_CC
			join exemplo1.FAMILY on CC = exemplo1.Family.E_CC WHERE
					(@VisitorName IS NULL OR exemplo1.VISITOR.Name = @VisitorName)
				AND
					(@VisitorCC IS NULL OR CC = @VisitorCC)
				AND
					(@VisitorPhone IS NULL OR exemplo1.VISITOR.Phone = @VisitorPhone) 
				AND
					(@RelationShip IS NULL OR KinshipDegree = @RelationShip) 
				AND
					(@RelationShip IS NULL OR Relationship = @RelationShip) 
				AND
					(@Date IS NULL OR CAST(Date as DATE) = @Date) 
				ORDER BY exemplo1.VISITOR.CC
				OFFSET ((@PageNumber - 1) * @RowsPage) ROWS FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO

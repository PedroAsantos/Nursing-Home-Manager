CREATE FUNCTION dbo.getVisits(@PatientNif varchar(9)=NULL,@PatientName varchar(30)=NULL,@VisitorName varchar(30)=NULL,@VisitorCC varchar(9)=NULL,@VisitorPhone INT=NULL,@Date Date=NULL,@PageNumber INT, @RowsPage INT,@sortOrder VARCHAR(30),@sortColumn VARCHAR(30))
RETURNS TABLE
AS
	RETURN(
		select exemplo1.PATIENT.Name as PatientName, NIF, exemplo1.VISITOR.Name as VisitorName, CC, exemplo1.VISITOR.Phone, exemplo1.VISITOR.Address,exemplo1.VISIT.Date, exemplo1.NOT_Family.KinshipDegree, exemplo1.FAMILY.Relationship from exemplo1.VISITOR
			join exemplo1.VISITED on CC = E_CCVisitor
			join exemplo1.VISIT on E_IDVisit = ID
			join exemplo1.RECEIVED on exemplo1.RECEIVED.E_IDVisit = ID
			join exemplo1.PATIENT on E_NIF = NIF
			Left Outer join exemplo1.NOT_FAMILY on CC = exemplo1.NOT_FAMILY.E_CC
			LEFT OUTER join exemplo1.FAMILY on CC = exemplo1.Family.E_CC WHERE
					(@VisitorName IS NULL OR exemplo1.VISITOR.Name = @VisitorName)
				AND
					(@VisitorCC IS NULL OR CC = @VisitorCC)
				AND
					(@VisitorPhone IS NULL OR exemplo1.VISITOR.Phone = @VisitorPhone)
				AND
					(@PatientNif IS NULL OR NIF = @PatientNif) 
				AND
					(@PatientName IS NULL OR  exemplo1.PATIENT.Name = @PatientName)  
				AND
					(@Date IS NULL OR CAST(Date as DATE) = @Date) 
				ORDER BY  case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Patient Name' then exemplo1.PATIENT.Name 
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Visitor Name' then exemplo1.VISITOR.Name
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Patient Nif' then NIF
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Visitor CC' then CC
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Date' then Date
							end ASC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Patient Name' then exemplo1.PATIENT.Name 
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Visitor Name' then exemplo1.VISITOR.Name
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Patient Nif' then NIF
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Visitor CC' then CC
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Date' then Date
							end DESC
							
				OFFSET ((@PageNumber - 1) * @RowsPage) ROWS FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO

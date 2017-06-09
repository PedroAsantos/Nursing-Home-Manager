CREATE FUNCTION dbo.getPatients(@PatientNif VARCHAR(9)=NULL,@PatientName VARCHAR(30)=NULL,@Sex varchar(1)=NULL, @authorization BIT=NULL,@RoomNumber INT=NULL,@PhoneNUmber INT=NULL,@Checkout BIT=NULL,@PageNumber INT, @RowsPage INT, @sortOrder VARCHAR(5),@sortColumn VARCHAR(30))
RETURNS TABLE
AS
	RETURN(
		select exemplo1.PATIENT.NIF, exemplo1.PATIENT.Name, exemplo1.PATIENT.Sex, exemplo1.PATIENT.Phone, exemplo1.PATIENT.Age, exemplo1.PATIENT.Check_in, exemplo1.PATIENT.Check_out, Authorization_to_leave, E_BedNumber, Entry_Date, Exit_Date, RoomNumber, exemplo1.VISITOR.Name as DependentName, exemplo1.VISITOR.CC as DependentCC, exemplo1.VISITOR.Address as DependentAddress, exemplo1.VISITOR.Phone as DependentPhone, exemplo1.FAMILY.Relationship  from( exemplo1.PATIENT 
		JOIN exemplo1.BED on E_BedNumber = BedNumber
		JOIN exemplo1.BEDROOM on E_RoomNumber = RoomNumber
		FULL OUTER JOIN exemplo1.DEPENDENT on E_PatientNIF = NIF
		LEFT OUTER JOIN exemplo1.FAMILY on exemplo1.DEPENDENT.E_CC = exemplo1.FAMILY.E_CC
		LEFT OUTER JOIN exemplo1.VISITOR on exemplo1.FAMILY.E_CC = exemplo1.VISITOR.CC )	
		WHERE    (@PatientNif IS NULL OR  exemplo1.PATIENT.NIF = @PatientNif)
				AND
					(@PatientName IS NULL OR exemplo1.PATIENT.Name = @PatientName)
				AND
					(@Sex IS NULL OR exemplo1.PATIENT.Sex = @Sex)
				AND
					(@authorization IS NULL OR Authorization_to_leave = @authorization)
				AND  
					(@RoomNumber IS NULL OR RoomNumber = @RoomNumber)
				AND
					(@PhoneNUmber IS NULL OR exemplo1.PATIENT.Phone = @PhoneNUmber)
				AND
					(@Checkout IS NOT NULL OR exemplo1.Patient.Check_out IS NULL ) ORDER BY  
							case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Name' then exemplo1.PATIENT.Name 
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Nif' then exemplo1.PATIENT.NIF
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Check in' then Check_in
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Check out' then Check_out
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Room Number' then RoomNumber
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Bed Number' then BedNumber 
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Name' then exemplo1.PATIENT.Name 
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Nif' then exemplo1.PATIENT.NIF
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Check in' then Check_in
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Check out' then Check_out
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Room Number' then RoomNumber
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Bed Number' then BedNumber 
							end DESC
			OFFSET ((@PageNumber - 1) * @RowsPage) ROWS
			FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO


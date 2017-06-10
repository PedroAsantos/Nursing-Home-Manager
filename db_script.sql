-- create schema LarIdosos

CREATE TABLE LarIdosos.BEDROOM(
	RoomNumber		INT 		NOT NULL	IDENTITY(1, 1),
	Capacity		INT,
	CONSTRAINT PKBEDROOM PRIMARY KEY (RoomNumber)
);

CREATE TABLE LarIdosos.BED(
	BedNumber		INT 		NOT NULL	IDENTITY(1, 1),
	E_RoomNumber	INT,
	CONSTRAINT PKBED PRIMARY KEY (BedNumber),
	CONSTRAINT FKBED FOREIGN KEY (E_RoomNumber) REFERENCES LarIdosos.BEDROOM(RoomNumber)
);

CREATE TABLE LarIdosos.DOCTOR(
	NIF		varchar(9) 		NOT NULL,
	Name	varchar(30),
	Phone	INT,
	Address	varchar(30),
	CONSTRAINT PKDOCTOR PRIMARY KEY (NIF)
);

CREATE TABLE LarIdosos.PATIENT(
	NIF		varchar(9),
	Name	varchar(30),
	Sex		varchar(1),
	Phone	INT,
	Age		INT,
	Check_in	DATE,
	Check_out	DATE,
	Authorization_to_leave BIT,
	E_BedNumber	INT,
	Entry_Date	DATE,
	Exit_Date	DATE,
	CHECK(Check_in < Check_out),
	CHECK(Entry_Date < Exit_Date),
	CONSTRAINT PKPATIENT PRIMARY KEY (NIF),
	CONSTRAINT FKPATIENT2 FOREIGN KEY (E_BedNumber) REFERENCES LarIdosos.BED(BedNumber)
);

CREATE TABLE LarIdosos.DISEASE(
	Name	varchar(15)		NOT NULL,
	CONSTRAINT PKDISEASE PRIMARY KEY (Name)
);

CREATE TABLE LarIdosos.DIAGNOSED(
	E_Name	varchar(15)		NOT NULL,
	E_NIF	varchar(9)		NOT NULL,
	Seriousness	INT,
	Disable	BIT,
	CONSTRAINT FKDIAGNOSED FOREIGN KEY (E_Name) REFERENCES LarIdosos.DISEASE(Name),
	CONSTRAINT FKDIAGNOSED2 FOREIGN KEY (E_NIF) REFERENCES LarIdosos.PATIENT(NIF),
	CONSTRAINT PKDIAGNOSED PRIMARY KEY (E_Name, E_NIF)
);

CREATE TABLE LarIdosos.EXITS(
	ID			INT		NOT NULL	IDENTITY(1, 1),
	Check_in	DATE,
	Check_out	DATE,
	CHECK(Check_in < Check_out),
	CONSTRAINT PKEXITS PRIMARY KEY (ID)
);

CREATE TABLE LarIdosos.SCHEDULE(
	Day		varchar(15)		NOT NULL,
	Hour	TIME		NOT NULL,
	E_NIF	varchar(9),
	CONSTRAINT FKSCHEDULE FOREIGN KEY (E_NIF) REFERENCES LarIdosos.PATIENT(NIF),
	CONSTRAINT PKSCHEDULE PRIMARY KEY (Day, Hour, E_NIF)
);

CREATE TABLE LarIdosos.MEDICINE(
	ID		INT		NOT NULL	IDENTITY(1, 1),
	Name	varchar(30)	NOT NULL,
	CONSTRAINT PKMEDICINE PRIMARY KEY (ID)
);
CREATE TABLE LarIdosos.TAKING(
	E_Day	varchar(15)	NOT NULL,
	E_Hour	TIME	NOT NULL,
	E_NIF	varchar(9)		NOT NULL,
	E_ID	INT		NOT NULL,
	Dose	INT,
	Disable Bit Default (0),
	CONSTRAINT FKTAKING FOREIGN KEY (E_Day, E_Hour, E_NIF) REFERENCES LarIdosos.SCHEDULE(Day, Hour, E_NIF),
	CONSTRAINT FKTAKING2 FOREIGN KEY (E_ID) REFERENCES LarIdosos.MEDICINE(ID),
	CONSTRAINT PKTAKING PRIMARY KEY (E_Day, E_Hour, E_NIF, E_ID)
);

CREATE TABLE LarIdosos.APPOINTMENT(
	ID				INT		NOT NULL	IDENTITY(1, 1),
	E_DoctorNIF		varchar(9),
	E_PatientNIF	varchar(9),
	Date			DATETIME,
	Speciality		varchar(30),
	Disable			BIT DEFAULT 0,
	CONSTRAINT PKAPPOINTMENT PRIMARY KEY (ID),
	CONSTRAINT FKAPPOINTMENT FOREIGN KEY (E_DoctorNIF) REFERENCES LarIdosos.DOCTOR(NIF),
	CONSTRAINT FKAPPOINTMENT2 FOREIGN KEY (E_PatientNIF) REFERENCES LarIdosos.PATIENT(NIF)
);

CREATE TABLE LarIdosos.VISIT(
	ID		INT		NOT NULL	IDENTITY(1, 1),
	Date	DATETIME,
	CONSTRAINT PKVISIT PRIMARY KEY (ID)
);

CREATE TABLE LarIdosos.RECEIVED(
	E_NIF		varchar(9)		NOT NULL,
	E_IDVisit	INT		NOT NULL,
	CONSTRAINT FKRECEIVED FOREIGN KEY (E_NIF) REFERENCES LarIdosos.PATIENT(NIF),
	CONSTRAINT FKRECEIVED2 FOREIGN KEY (E_IDVisit) REFERENCES LarIdosos.VISIT(ID),
	CONSTRAINT PKRECEIVED PRIMARY KEY (E_NIF, E_IDVisit)
);

CREATE TABLE LarIdosos.ACCOMPANIED(
	NIF_Patient		varchar(9)		NOT NULL,
	NIF_Doctor		varchar(9)		NOT NULL,
	CONSTRAINT FKACCOMPAINED FOREIGN KEY (NIF_Doctor) REFERENCES LarIdosos.DOCTOR(NIF),
	CONSTRAINT FKACCOMPAINED2 FOREIGN KEY (NIF_Patient) REFERENCES LarIdosos.PATIENT(NIF),
	CONSTRAINT PKACCOMPAINED PRIMARY KEY (NIF_Patient, NIF_Doctor)
);


CREATE TABLE LarIdosos.VISITOR(
	CC		varchar(9)		NOT NULL,
	Name	varchar(30),
	Address	varchar(30),
	Phone	INT,
	CONSTRAINT PKVISITOR PRIMARY KEY (CC)
);

CREATE TABLE LarIdosos.VISITED(
	E_IDVisit		INT		NOT NULL,
	E_CCVisitor		varchar(9)		NOT NULL,
	CONSTRAINT FKVISITED FOREIGN KEY (E_IDVisit) REFERENCES LarIdosos.VISIT(ID),
	CONSTRAINT FKVISITED2 FOREIGN KEY (E_CCVisitor) REFERENCES LarIdosos.VISITOR(CC),
	CONSTRAINT PKVISITED PRIMARY KEY (E_IDVisit, E_CCVisitor)
);

CREATE TABLE LarIdosos.FAMILY(
	E_CC			varchar(9)		NOT NULL,
	Relationship	varchar(30),
	CONSTRAINT FKFAMILY FOREIGN KEY (E_CC) REFERENCES LarIdosos.VISITOR(CC),
	CONSTRAINT PKFAMILY PRIMARY KEY (E_CC)
);

CREATE TABLE LarIdosos.NOT_FAMILY(
	E_CC			varchar(9)		NOT NULL,
	KinshipDegree	varchar(15),
	CONSTRAINT FKNOT_FAMILY FOREIGN KEY (E_CC) REFERENCES LarIdosos.VISITOR(CC),
	CONSTRAINT PKNOT_FAMILY PRIMARY KEY (E_CC)
);

CREATE TABLE LarIdosos.DEPENDENT(
	E_CC			varchar(9)		NOT NULL,
	E_PatientNIF	varchar(9)		NOT NULL,
	CONSTRAINT FKDEPENDENT FOREIGN KEY (E_CC) REFERENCES LarIdosos.FAMILY(E_CC),
	CONSTRAINT FKDEPENDENT2 FOREIGN KEY (E_PatientNIF) REFERENCES LarIdosos.PATIENT(NIF),
	CONSTRAINT PKDEPENDENT PRIMARY KEY (E_CC,E_PatientNIF)
);

CREATE TABLE LarIdosos.LEAVES(
	ID			INT		NOT NULL,
	E_NIF		varchar(9)		,
	CONSTRAINT FKLEAVES FOREIGN KEY (ID) REFERENCES LarIdosos.EXITS(ID),
	CONSTRAINT FKLEAVES2 FOREIGN KEY (E_NIF) REFERENCES LarIdosos.PATIENT(NIF),
	CONSTRAINT PKLEAVES PRIMARY KEY (ID, E_NIF)
);


--CREATE DATABASE exemplo1;
--CREATE SCHEMA exemplo1;


CREATE TABLE LarIdosos.Type(
	ID			INT 		NOT NULL	IDENTITY(1, 1),
	Designation	VARCHAR(30)		UNIQUE,
	Disable			BIT DEFAULT 0,
	PRIMARY KEY (ID)
);

CREATE TABLE LarIdosos.HumanResources(
	NIF			varchar(9) 		NOT NULL,
	Name		VARCHAR(30),
	Phone		INT		UNIQUE,
	Address		VARCHAR(30),
	Salary		INT,
	StartDate	DATE,
	E_IDType	INT 		NOT NULL,
	PRIMARY KEY (NIF),
	FOREIGN KEY (E_IDType) REFERENCES LarIdosos.Type(ID)
);

CREATE TABLE LarIdosos.Shift(
	ID					INT 		NOT NULL	IDENTITY(1, 1),
	Day					VARCHAR(30),
	BeginOfWorkShift	TIME,
	EndOfWorkShift		TIME,
	PRIMARY KEY (ID)
);

CREATE TABLE LarIdosos.ShiftInstance(
	ID					INT 		NOT NULL IDENTITY(1, 1),
	InicialDate			DATE,
	FinalDate			DATE,
	E_NIF				varchar(9)  		NOT NULL,
	E_IDShift			INT 		NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (E_NIF) REFERENCES LarIdosos.HumanResources(NIF),
	FOREIGN KEY (E_IDShift) REFERENCES LarIdosos.Shift(ID)
);

CREATE TABLE LarIdosos.Faults(
	Date		DATE,
	E_ID		INT		NOT NULL,
	FOREIGN KEY (E_ID) REFERENCES LarIdosos.ShiftInstance(ID),
	PRIMARY KEY (Date, E_ID)
);

go

CREATE FUNCTION dbo.searchAppointments(@DoctorNif VARCHAR(9)=NULL,@PatientNif VARCHAR(9)=NULL,@DoctorName varchar(30)=NULL,@PatientName varchar(30)=NULL,@Date DateTime=NULL,@Speciality varchar(30)=NULL, @PageNumber INT, @RowsPage INT,@sortOrder VARCHAR(5),@sortColumn VARCHAR(30))
RETURNS TABLE
AS
	RETURN
		(SELECT E_DoctorNIF as DoctorNIF, LarIdosos.DOCTOR.Name as DoctorName, E_PatientNIF as PatientNIF, LarIdosos.PATIENT.Name as PatientName, Date, Speciality from (LarIdosos.APPOINTMENT JOIN LarIdosos.Doctor on E_DoctorNIF = LarIdosos.DOCTOR.NIF) JOIN LarIdosos.PATIENT ON E_PatientNIF = LarIdosos.PATIENT.NIF 
		WHERE    (@DoctorNif IS NULL OR LarIdosos.APPOINTMENT.E_DoctorNIF = @DoctorNif)
				AND
					(@PatientNif IS NULL OR LarIdosos.APPOINTMENT.E_PatientNIF = @PatientNif)
				AND
					(@DoctorName IS NULL OR LarIdosos.DOCTOR.Name = @DoctorName)
				AND
					(@PatientName IS NULL OR LarIdosos.PATIENT.Name = @PatientName)
				AND  
					(@Date IS NULL OR CAST(Date as DATE) = @Date)
				AND
					(@Speciality IS NULL OR LarIdosos.APPOINTMENT.Speciality = @Speciality)  ORDER BY  
							case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Patient Name' then LarIdosos.PATIENT.Name	
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Patient NIF' then LarIdosos.PATIENT.NIF
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Doctor Name' then LarIdosos.DOCTOR.Name
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Date' then Date
							end ASC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Patient Name' then LarIdosos.PATIENT.Name 
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Patient NIF' then LarIdosos.PATIENT.NIF
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Doctor Name' then LarIdosos.DOCTOR.Name
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Date' then Date
							end DESC
							OFFSET ((@PageNumber - 1) * @RowsPage) ROWS
							FETCH NEXT @RowsPage ROWS ONLY
					)
				
GO	
	
CREATE FUNCTION dbo.getVisits(@PatientNif varchar(9)=NULL,@PatientName varchar(30)=NULL,@VisitorName varchar(30)=NULL,@VisitorCC varchar(9)=NULL,@VisitorPhone INT=NULL,@Date Date=NULL,@PageNumber INT, @RowsPage INT,@sortOrder VARCHAR(30),@sortColumn VARCHAR(30))
RETURNS TABLE
AS
	RETURN(
		select LarIdosos.PATIENT.Name as PatientName, NIF, LarIdosos.VISITOR.Name as VisitorName, CC, LarIdosos.VISITOR.Phone, LarIdosos.VISITOR.Address,LarIdosos.VISIT.Date, LarIdosos.NOT_Family.KinshipDegree, LarIdosos.FAMILY.Relationship from LarIdosos.VISITOR
			join LarIdosos.VISITED on CC = E_CCVisitor
			join LarIdosos.VISIT on E_IDVisit = ID
			join LarIdosos.RECEIVED on LarIdosos.RECEIVED.E_IDVisit = ID
			join LarIdosos.PATIENT on E_NIF = NIF
			Left Outer join LarIdosos.NOT_FAMILY on CC = LarIdosos.NOT_FAMILY.E_CC
			LEFT OUTER join LarIdosos.FAMILY on CC = LarIdosos.Family.E_CC WHERE
					(@VisitorName IS NULL OR LarIdosos.VISITOR.Name = @VisitorName)
				AND
					(@VisitorCC IS NULL OR CC = @VisitorCC)
				AND
					(@VisitorPhone IS NULL OR LarIdosos.VISITOR.Phone = @VisitorPhone)
				AND
					(@PatientNif IS NULL OR NIF = @PatientNif) 
				AND
					(@PatientName IS NULL OR  LarIdosos.PATIENT.Name = @PatientName)  
				AND
					(@Date IS NULL OR CAST(Date as DATE) = @Date) 
				ORDER BY  case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Patient Name' then LarIdosos.PATIENT.Name 
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Visitor Name' then LarIdosos.VISITOR.Name
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
							when @sortColumn = 'Patient Name' then LarIdosos.PATIENT.Name 
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Visitor Name' then LarIdosos.VISITOR.Name
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

CREATE FUNCTION dbo.getVisitors(@VisitorName varchar(30)=NULL,@VisitorCC varchar(9)=NULL,@VisitorPhone INT=NULL,@PageNumber INT, @RowsPage INT)
RETURNS TABLE
AS
	RETURN(
		select Name, CC, Phone from LarIdosos.Visitor 	WHERE
					(@VisitorName IS NULL OR  LarIdosos.VISITOR.Name = @VisitorName)
				AND
					(@VisitorCC IS NULL OR CC = @VisitorCC)
				AND
					(@VisitorPhone IS NULL OR Phone = @VisitorPhone) ORDER BY LarIdosos.VISITOR.CC
				OFFSET ((@PageNumber - 1) * @RowsPage) ROWS FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO

CREATE FUNCTION dbo.getPatients(@PatientNif VARCHAR(9)=NULL,@PatientName VARCHAR(30)=NULL,@Sex varchar(1)=NULL, @authorization BIT=NULL,@RoomNumber INT=NULL,@PhoneNUmber INT=NULL,@Checkout BIT=NULL,@PageNumber INT, @RowsPage INT, @sortOrder VARCHAR(5)='DESC',@sortColumn VARCHAR(30)='Check In')
RETURNS TABLE
AS
	RETURN(
		select LarIdosos.PATIENT.NIF, LarIdosos.PATIENT.Name, LarIdosos.PATIENT.Sex, LarIdosos.PATIENT.Phone, LarIdosos.PATIENT.Age, LarIdosos.PATIENT.Check_in, LarIdosos.PATIENT.Check_out, Authorization_to_leave, E_BedNumber, Entry_Date, Exit_Date, RoomNumber, LarIdosos.VISITOR.Name as DependentName, LarIdosos.VISITOR.CC as DependentCC, LarIdosos.VISITOR.Address as DependentAddress, LarIdosos.VISITOR.Phone as DependentPhone, LarIdosos.FAMILY.Relationship  from( LarIdosos.PATIENT 
		JOIN LarIdosos.BED on E_BedNumber = BedNumber
		JOIN LarIdosos.BEDROOM on E_RoomNumber = RoomNumber
		FULL OUTER JOIN LarIdosos.DEPENDENT on E_PatientNIF = NIF
		LEFT OUTER JOIN LarIdosos.FAMILY on LarIdosos.DEPENDENT.E_CC = LarIdosos.FAMILY.E_CC
		LEFT OUTER JOIN LarIdosos.VISITOR on LarIdosos.FAMILY.E_CC = LarIdosos.VISITOR.CC )	
		WHERE    (@PatientNif IS NULL OR  LarIdosos.PATIENT.NIF = @PatientNif)
				AND
					(@PatientName IS NULL OR LarIdosos.PATIENT.Name = @PatientName)
				AND
					(@Sex IS NULL OR LarIdosos.PATIENT.Sex = @Sex)
				AND
					(@authorization IS NULL OR Authorization_to_leave = @authorization)
				AND  
					(@RoomNumber IS NULL OR RoomNumber = @RoomNumber)
				AND
					(@PhoneNUmber IS NULL OR LarIdosos.PATIENT.Phone = @PhoneNUmber)
				AND
					(@Checkout IS NOT NULL OR LarIdosos.Patient.Check_out IS NULL ) ORDER BY  
							case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Name' then LarIdosos.PATIENT.Name 
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'NIF' then LarIdosos.PATIENT.NIF
							end ASC
						   ,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Check In' then Check_in
							end ASC
							,case
							when @sortOrder <> 'ASC' then ''
							when @sortColumn = 'Check Out' then Check_out
							end ASC
							,case
							when @sortOrder <> 'ASC' then 0
							when @sortColumn = 'Room Number' then RoomNumber
							end ASC
							,case
							when @sortOrder <> 'ASC' then 0
							when @sortColumn = 'Bed Number' then BedNumber 
							end ASC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Name' then LarIdosos.PATIENT.Name 
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'NIF' then LarIdosos.PATIENT.NIF
							end DESC
						   ,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Check In' then Check_in
							end DESC
							,case
							when @sortOrder <> 'DESC' then ''
							when @sortColumn = 'Check Out' then Check_out
							end DESC
							,case
							when @sortOrder <> 'DESC' then 0
							when @sortColumn = 'Room Number' then RoomNumber
							end DESC
							,case
							when @sortOrder <> 'DESC' then 0
							when @sortColumn = 'Bed Number' then BedNumber 
							end DESC
			OFFSET ((@PageNumber - 1) * @RowsPage) ROWS
			FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO

CREATE FUNCTION dbo.getPatientMedicines(@NIF Varchar(9))
RETURNS TABLE
AS
	RETURN(
			select Name, Dose, E_Day,E_Hour,E_ID FROM (LarIdosos.TAKING JOIN LarIdosos.MEDICINE on E_ID=ID) Where E_nif = @NIF AND LarIdosos.TAKING.Disable = 0
		  )		
GO

CREATE FUNCTION dbo.getPatientDiseases(@NIF INT)
RETURNS TABLE
AS
	RETURN
		SELECT E_name,Seriousness FROM LarIdosos.DIAGNOSED WHERE LarIdosos.DIAGNOSED.E_NIF = @NIF and LarIdosos.DIAGNOSED.Disable = 0
GO



CREATE FUNCTION dbo.getPatientAppointments(@NIF varchar(9))
RETURNS TABLE
AS
	RETURN(
			select  LarIdosos.DOCTOR.Name, Date, Speciality, LarIdosos.APPOINTMENT.ID FROM (LarIdosos.APPOINTMENT JOIN LarIdosos.DOCTOR on E_DoctorNIF = LarIdosos.DOCTOR.NIF) WHERE LarIdosos.APPOINTMENT.Disable = 0 AND LarIdosos.APPOINTMENT.E_PatientNif = @NIF
		  )		
GO

CREATE FUNCTION dbo.getHumanTypes()
RETURNS TABLE
AS
	RETURN(
			select Designation,Id from LarIdosos.Type WHERE Disable = 0
		  )		
GO

CREATE FUNCTION dbo.getHumanResourceSchedule(@NIF Varchar(9))
RETURNS TABLE
AS
	RETURN(
			SELECT Day, BeginOfWorkShift, EndOfWorkShift,E_IDShift FROM (LarIdosos.ShiftInstance JOIN LarIdosos.Shift on E_IDShift= LarIdosos.Shift.ID) Where E_nif = @NIF and FinalDate IS NULL
		  )		
GO

CREATE FUNCTION dbo.getHumanResourceFaults(@NIF Varchar(9))
RETURNS TABLE
AS
	RETURN(
			SELECT LarIdosos.Shift.Day, BeginOfWorkShift, EndOfWorkShift,E_IDShift, FinalDate,count(LarIdosos.Faults.E_ID) as NumberOfFaults FROM (LarIdosos.ShiftInstance JOIN LarIdosos.Shift on  E_IDShift= LarIdosos.Shift.ID) 
					LEFT OUTER JOIN LarIdosos.Faults on LarIdosos.Faults.E_ID = LarIdosos.ShiftInstance.ID Where E_nif = @NIF GROUP BY Day, BeginOfWorkShift, EndOfWorkShift, E_IDShift,FinalDate 
		  )		
GO

CREATE FUNCTION dbo.getHumanResources(@WorkerName varchar(30)=NULL,@WorkerNif varchar(9)=NULL,@WorkerPhone INT=NULL,@WorkerAddress varchar(30)=NULL,@Designation varchar(30)=NULL,@PageNumber INT, @RowsPage INT)
RETURNS TABLE
AS
	RETURN(
			select NIF,Name,Phone,Address,Salary,StartDate,Designation from LarIdosos.HumanResources
			join LarIdosos.Type on E_IDType = ID WHERE (@WorkerName IS NULL OR Name = @WorkerName)
				AND
					(@WorkerNif IS NULL OR NIF = @WorkerNif)
				AND
					(@WorkerPhone IS NULL OR Phone = @WorkerPhone)
				AND  
					(@WorkerAddress IS NULL OR SOUNDEX([Address]) = SOUNDEX(@WorkerAddress))
				AND
					(@Designation IS NULL OR Designation = @Designation) ORDER BY  NIF
			OFFSET ((@PageNumber - 1) * @RowsPage) ROWS
			FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO


CREATE FUNCTION dbo.getFreeRoomsAndBeds()
RETURNS TABLE
AS
	RETURN(
		select distinct RoomNumber from (select RoomNumber, BedNumber from LarIdosos.BED 
			join LarIdosos.BEDROOM on E_RoomNumber = RoomNumber

		except
		select RoomNumber, BedNumber from LarIdosos.PATIENT 
			join LarIdosos.BED on E_BedNumber = BedNumber
			join LarIdosos.BEDROOM on E_RoomNumber = RoomNumber) as tmp
			)
			
GO

CREATE FUNCTION dbo.getFreeBeds(@RoomNumber INT)
RETURNS TABLE
AS
	RETURN
		SELECT BedNumber FROM (
		SELECT E_RoomNumber, BedNumber FROM LarIdosos.BED
		EXCEPT
		SELECT E_RoomNumber, BedNumber FROM LarIdosos.PATIENT join LarIdosos.BED 
			ON E_BedNumber = BedNumber) AS tmp join LarIdosos.BEDROOM on tmp.E_RoomNumber = RoomNumber
		WHERE tmp.E_RoomNumber = @RoomNumber
GO


CREATE FUNCTION dbo.getDoctors()
RETURNS TABLE
AS
	RETURN(
			select NIF,Name,Phone,Address from LarIdosos.DOCTOR
		  )		
GO

CREATE PROCEDURE dbo.sp_deleteType (@id INT)
AS
BEGIN
	Delete FROM LarIdosos.Type WHERE (id=@id)
	RETURN
end
go

CREATE PROCEDURE dbo.sp_Visit (@Date DATETIME, @NIF varchar(9), @CC varchar(9))
AS
BEGIN
		declare @tmp int;
		if  (Exists(Select NIF from LarIdosos.PATIENT where NIF=@NIF ))
					if  (Exists(Select CC from LarIdosos.VISITOR where CC=@CC ))
								INSERT INTO LarIdosos.VISIT values (
										@Date
								)
								Select top 1 @tmp = ID from LarIdosos.VISIT order by ID desc

								INSERT INTO LarIdosos.RECEIVED(E_NIF, E_IDVisit) values(
										@NIF, @tmp 		
								)
								INSERT INTO LarIdosos.VISITED(E_IDVisit, E_CCVisitor) values(
										@tmp, @CC 		
								)		
		RETURN
end
go

CREATE PROCEDURE [dbo].[sp_updatePATIENT]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Sex	varchar(1),
	@Phone	INT,
	@Age	INT,
	@Check_in	DATE,
	@Check_out	DATE,
	@Authorization_to_leave BIT,
	@E_BedNumber	INT,
	@Entry_Date	DATE,
	@Exit_Date	DATE 
AS
BEGIN
	BEGIN TRANSACTION;
		BEGIN TRY
			UPDATE LarIdosos.PATIENT
			SET		NIF = @NIF,
					Name = @Name,
					Sex = @Sex,
					Phone = @Phone,
					Age = @Age,
					Check_in = @Check_in,
					Check_out = @Check_out,
					Authorization_to_leave = @Authorization_to_leave,
					E_BedNumber = @E_BedNumber,
					Entry_Date = @Entry_Date,
					Exit_Date = @Exit_Date
			WHERE NIF=@NIF
		END TRY

		BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;
	IF @@TRANCOUNT = 0
		COMMIT TRANSACTION;
END
go

CREATE PROCEDURE dbo.sp_newType(@type VARCHAR(30))
as
begin
		DECLARE @ID int;
		SELECT @ID = id from LarIdosos.Type WHere Designation = @type
		if(@@ROWCOUNT = 0)
		BEGIN
			insert into LarIdosos.Type (Designation,Disable) values (@type,0);
			RETURN;
		END
		
		UPDATE LarIdosos.Type SET Disable = 0 WHERE id = @ID;
		RETURN;
END
GO


CREATE PROCEDURE dbo.sp_newVisitor ( @CC varchar(9), @Name varchar(30), @Address varchar(15), @Phone int,@RelationShip VARCHAR(30),@Family BIT)
as
begin

		if  (Exists(Select CC from LarIdosos.VISITOR where CC=@CC ))
			RETURN
		if  (Exists(Select NIF from LarIdosos.PATIENT where NIF=@CC ))
			RETURN
	
		INSERT INTO LarIdosos.VISITOR values(
				@CC,
				@Name,
				@Address,
				@Phone
		)
		if (@Family = 1)
		BEGIN
			Insert Into LarIdosos.FAMILY values(@CC,@RelationShip);
		END
		ELSE
		BEGIN
			INSERT INTO LarIdosos.NOT_FAMILY values(@CC,@RelationShip)
		END
		RETURN
END
GO

CREATE PROCEDURE dbo.sp_newTaking (@E_Day varchar(15), @E_Hour time, @E_NIF varchar(9), @medicineName varchar(30), @Dose int)
as
begin
		if  (not Exists(Select * from LarIdosos.PATIENT where NIF=@E_NIF ))
			RAISERROR('O Patient não existe!',16,1)

		DECLARE @medicineID INT;
		SELECT @medicineID = id from LarIdosos.medicine where Name = @medicineName;
		if @@ROWCOUNT = 0
		BEGIN
			INSERT INTO LarIdosos.medicine values (@medicinename)
			SELECT @medicineID = id from LarIdosos.medicine where Name = @medicineName;
		END

		SELECT * from LarIdosos.Schedule Where day=@E_Day and hour=@E_Hour and E_NIF = @E_NIF;
		if @@ROWCOUNT = 0
		BEGIN
			INSERT INTO LarIdosos.Schedule values (@E_Day,@E_Hour, @E_NIF);
		END
		DECLARE @DISABLE BIT;
		SET @DISABLE = 0;
		IF(Exists(Select * from LarIdosos.TAKING WHere E_day = @E_Day AND E_Hour = @E_Hour AND E_NIF = @E_NIF AND E_ID = @medicineID ))
		Begin
			UPDATE LarIdosos.TAKING SET  Dose = @Dose WHere E_day = @E_Day AND E_Hour = @E_Hour AND E_NIF = @E_NIF AND E_ID = @medicineID;
		END
		ELSE
		BEGIN
				INSERT INTO LarIdosos.TAKING values(
					@E_Day,
					@E_Hour,
					@E_NIF,
					@medicineID,
					@Dose,
					@Disable
			)
		END
		RETURN
END
GO

CREATE PROCEDURE dbo.sp_newSchedule (@NIF INT,@inicialDate Date, @finalDate Date=null, @beginofWorkShift Time, @endofWorkShift Time, @day varchar(30))
as
begin
		if  (not Exists(Select * from LarIdosos.HumanResources where NIF=@NIF ))
		BEGIN
			RAISERROR('O recurso humano não existe!',16,1)
			return;
		END

		DECLARE @idShift INT;
		Select @idShift=ID FROM LarIdosos.Shift WHERE BeginOfWorkShift = @beginofWorkShift AND EndOfWorkShift = @endofWorkShift AND Day = @day;
		
		if(@@ROWCOUNT = 0)
		BEGIN
			INSERT INTO LarIdosos.Shift VALUES (@day, @beginofWorkShift, @endofWorkShift);
			Select @idShift=ID FROM LarIdosos.Shift WHERE BeginOfWorkShift = @beginofWorkShift AND EndOfWorkShift = @endofWorkShift AND Day = @day;
		END

		if(not Exists(SELECT * FROM LarIdosos.ShiftInstance WHERE E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			INSERT INTO LarIdosos.ShiftInstance VALUES (@inicialDate,@finalDate,@NIF,@idShift);
			RETURN;
		END

		if(Exists(SELECT * FROM LarIdosos.ShiftInstance WHERE FinalDate is NULL AND E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			RAISERROR('Já tem asssociado esse horário!',16,1)
			return;
		END
		if(Exists(SELECT * FROM LarIdosos.ShiftInstance WHERE FinalDate IS NOT null AND E_NIF=@NIF AND E_IDShift = @idShift))
		BEGIN
			INSERT INTO LarIdosos.ShiftInstance VALUES (@inicialDate,@finalDate,@NIF,@idShift);
		END
		
		RETURN;
END
GO

CREATE PROCEDURE dbo.sp_newDoctor
	@NIF	varchar(9),
	@Name	varchar(30),
	@Phone	INT,
	@Address varchar(30)
 
AS
BEGIN
		if  (Exists(Select NIF from LarIdosos.DOCTOR where NIF=@NIF ))
		begin
			UPDATE LarIdosos.HUMANRESOURCES
			SET NIF = @NIF,
			Name = @Name,
			Phone = @Phone,
			Address = @Address
			where NIF = @NIF
		end
		else
		begin
			 INSERT INTO LarIdosos.DOCTOR values (
					@NIF,
					@Name,
					@Phone,
					@Address
				  )
		end
		return
end
go

CREATE PROCEDURE dbo.sp_newRoom(@capacity INT=2, @NewRoomId int OUTPUT)
as
begin	IF(@capacity < 0 )
		BEGIN
			RAISERROR('Capacity must be a number positive',16,1)
		END
		if(NOT EXISTS(SELECT * FROM LarIdosos.BEDROOM))
		BEGIN
			SET @NewRoomId = 1;
		END
		ELSE
		BEGIN
			Select @NewRoomId = RoomNumber from LarIdosos.BEDROOM order by RoomNumber asc
		END
		
		
		insert into LarIdosos.BEDROOM (Capacity) values (@capacity);
		
		While(@capacity > 0)
		BEGIN
			INSERT INTO LarIdosos.BED (E_RoomNumber) values (@NewRoomId);
			SET @capacity = @capacity - 1;
		END

		
	RETURN
END
GO

CREATE PROCEDURE [dbo].[sp_insertPATIENT]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Sex	varchar(1),
	@Phone	INT,
	@Age	INT,
	@Check_in	DATE,
	@Check_out	DATE,
	@Authorization_to_leave BIT,
	@E_BedNumber	INT,
	@Entry_Date	DATE,
	@Exit_Date	DATE 
 
AS
BEGIN
		if  (Exists(Select NIF from LarIdosos.PATIENT where NIF=@NIF ))
			RAISERROR('O Patient ja existe!',16,1);
		else
			 INSERT INTO LarIdosos.PATIENT values (
					@NIF,
					@Name,
					@Sex,
					@Phone,
					@Age,
					@Check_in,
					@Check_out,
					@Authorization_to_leave,
					@E_BedNumber,
					@Entry_Date,
					@Exit_Date
				  )
		return
end
go

CREATE PROCEDURE dbo.sp_newDiagnosed (@E_Name varchar(30), @E_NIF varchar(9), @Seriousness int, @Disable BIT)
as
begin
		if  (Exists(Select E_NIF, E_Name from LarIdosos.DIAGNOSED where E_NIF=@E_NIF AND E_Name = @E_Name ))
			RAISERROR('Diagnosed já existente!',16,1);
		else
		begin
			if  (not Exists(Select Name from LarIdosos.DISEASE where Name=@E_Name ))
				INSERT INTO LarIdosos.DISEASE values(
						@E_Name
				)
			if  (Exists(Select NIF from LarIdosos.PATIENT where NIF=@E_NIF ))
				INSERT INTO LarIdosos.DIAGNOSED values(
						@E_Name,
						@E_NIF,
						@Seriousness,
						@Disable
				)
			else
				RAISERROR('O NIF do Patient nao existe!',16,1);
		end
		RETURN
END
GO

CREATE PROCEDURE dbo.sp_newDependent (@E_NIF varchar(9), @Name varchar(30), @CC varchar(9), @Phone int, @Address varchar(30), @Relationship varchar(30))
as
begin
		if  (Exists(Select NIF from LarIdosos.PATIENT where NIF=@E_NIF ))
				if  (not Exists(Select CC from LarIdosos.VISITOR where CC=@CC ))
					INSERT INTO LarIdosos.VISITOR values (
							@CC,
							@Name,
							@Address,
							@Phone
					)
			if(not Exists(SELECT * FROM LarIdosos.FAMILY where E_CC=@CC))
			BEGIN
				INSERT INTO LarIdosos.FAMILY values (
					@CC,
					@Relationship
			)
			END
				
			INSERT INTO LarIdosos.DEPENDENT values (
					@CC,
					@E_NIF
			)

		RETURN
END
GO

CREATE PROCEDURE dbo.sp_updateDependent
	@E_NIF			 varchar(9),
	@CC				varchar(9),
	@Name			varchar(30),
	@Address			VARCHAR(30),
	@Phone			INT,
	@RelationShip	VARCHAR(30)
AS
BEGIN
	
			
				IF(NOT EXISTS(SELECT * FROM LarIdosos.VISITOR WHERE CC = @CC))
				BEGIN
					EXEC sp_newDependent @E_NIF, @Name, @CC, @Phone, @Address, @Relationship;
					RETURN;
				END
				ELSE
				BEGIN
					IF(NOT EXISTS(SELECT * FROM LarIdosos.dependent WHERE E_CC = @CC))
					BEGIN
						UPDATE LarIdosos.VISITOR SET	Name = @Name, Phone = @Phone, Address = @Address WHERE CC=@CC;
						INSERT INTO LarIdosos.DEPENDENT values (@CC,@E_NIF)
						return;
					END
					ELSE
					BEGIN 
						UPDATE LarIdosos.VISITOR SET	Name = @Name, Phone = @Phone, Address = @Address WHERE CC=@CC;
						return;
					END
				END
END
go

CREATE PROCEDURE dbo.sp_finishShiftOfHumanResources (@ID INT)
AS
BEGIN
	Delete FROM LarIdosos.shift WHERE (ID = @ID )
	RETURN
end
go

CREATE PROCEDURE [dbo].[sp_insertHumanResources]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Phone	INT,
	@Address varchar(30),
	@Salary	INT,
	@Start_Date	DATE = null,
	@E_IDType INT
 
AS
BEGIN
		if  (Exists(Select NIF from LarIdosos.HUMANRESOURCES where NIF=@NIF ))
		begin
			UPDATE LarIdosos.HUMANRESOURCES
			SET NIF = @NIF,
			Name = @Name,
			Phone = @Phone,
			Address = @Address,
			Salary = @Salary,
			StartDate = @Start_Date,
			E_IDType = @E_IDType
			where NIF = @NIF
		end
		else
		begin
			 INSERT INTO LarIdosos.HUMANRESOURCES values (
					@NIF,
					@Name,
					@Phone,
					@Address,
					@Salary,
					@Start_Date,
					@E_IDType
				  )
		end
		return
end
go

CREATE PROCEDURE dbo.sp_deleteMedicine(@E_Day varchar(15), @E_Hour time, @E_NIF varchar(9), @medicineID int)
AS
BEGIN
	Delete FROM LarIdosos.TAKING WHERE E_day = @E_Day AND E_Hour = @E_Hour AND E_NIF = @E_NIF AND E_ID = @medicineID; 
	RETURN
end
go

CREATE PROCEDURE dbo.sp_deleteAppointment(@ID INT)
AS
BEGIN
	Delete FROM LarIdosos.APPOINTMENT WHERE ID = @ID;
	RETURN
end
go

CREATE PROCEDURE dbo.sp_deleteDisease (@E_Name varchar(30), @E_NIF varchar(9))
AS
BEGIN
	Delete FROM LarIdosos.DIAGNOSED WHERE (E_Name = @E_Name and E_NIF = @E_NIF)
	RETURN
end
go

CREATE PROCEDURE dbo.sp_checkoutPatient
	@NIF	varchar(9)
AS
BEGIN
			UPDATE LarIdosos.Patient
			SET		Check_out = Convert(date, getdate())  
			WHERE NIF=@NIF
END
go

CREATE PROCEDURE dbo.sp_addFaulttoHumanResource (@NIF INT, @beginofWorkShift Time = null, @endofWorkShift Time = null, @day varchar(30))
as
begin
		if  (not Exists(Select * from LarIdosos.HumanResources where NIF=@NIF ))
		BEGIN
			RAISERROR('O recurso humano não existe!',16,1)
			return;
		END


		DECLARE @idShiftInstace INT;

		SELECT @idShiftInstace = LarIdosos.ShiftInstance.ID FROM (LarIdosos.ShiftInstance JOIN LarIdosos.Shift ON LarIdosos.Shift.ID = E_IDShift) WHERE BeginOfWorkShift = @beginofWorkShift AND endofWorkShift = @endofWorkShift AND Day = @day;
		IF(@@ROWCOUNT = 0)
		BEGIN
			RAISERROR('Não existe esse turno',16,1);
			RETURN;
		END

		if(NOT EXISTS(SELECT * FROM LarIdosos.Faults WHERE E_ID=@idShiftInstace AND Date = CONVERT (date, GETDATE())))
		BEGIN
			INSERT INTO LarIdosos.Faults values(GETDATE(),@idShiftInstace); 
		END
		ELSE
		BEGIN
			RAISERROR('Não pode marcar mais que uma falta no mesmo dia na mesma data.',16,1)
			RETURN;
		END

	
		RETURN;
END
GO

CREATE PROCEDURE dbo.sp_addPatientAppointment
	@PatientNif	varchar(9),
	@DoctorNif	varchar(9),
	@Date	DateTime,
	@Speciality varchar(30)
 
AS
BEGIN
		if  (Exists(Select * from LarIdosos.APPOINTMENT  Where E_PatientNIF=@PatientNif AND E_DoctorNIF = @DoctorNif AND Date = @Date And Speciality = @Speciality ))
		begin
				raiserror('O paciente já tem associada esta consulta',16,1);
				return;
		end
		if(Exists(Select * from LarIdosos.APPOINTMENT  Where E_PatientNIF=@PatientNif AND Date = @Date ))
		begin
				raiserror('O paciente já tem consulta para essa hora nesse dia',16,1);
				return;
		end
		
		--Declare @dateClosest DateTime;
		Declare @disable BIT;
		SET @Disable = 0 ; 
		--SELECT TOP 1 @dateClosest = Date  FROM LarIdosos.APPOINTMENT WHERE E_PatientNIF=@PatientNif AND LarIdosos.APPOINTMENT.Date < @Date ORDER BY LarIdosos.APPOINTMENT.Date DESC;
		
	--	if(DATEDIFF(hour, @dateClosest,@Date) < 1)
	--	begin
	--		INSERT INTO LarIdosos.APPOINTMENT values (
	--				@DoctorNif,
	--				@PatientNif,
	--				@Date,
	--				@Speciality,
	--				@Disable
	--		 )
	--		return;
	--	end
    --ELSE
	--	begin
			 INSERT INTO LarIdosos.APPOINTMENT values (
					@DoctorNif,
					@PatientNif,
					@Date,
					@Speciality,
					@Disable
			 )

			return;
	--	end
end
go

CREATE TRIGGER finishShiftofHumanResource ON LarIdosos.Shift
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE LarIdosos.ShiftInstance SET FinalDate = GETDATE()
		WHERE (
				E_IDShift = (SELECT ID FROM deleted)
			  );
	END
GO

CREATE TRIGGER deleteType ON LarIdosos.Type
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE LarIdosos.Type SET Disable = 1
		WHERE (
				ID = (SELECT ID FROM deleted)
			  );
	END
GO

CREATE TRIGGER deleteMedicine ON LarIdosos.TAKING
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE LarIdosos.TAKING SET Disable = 1
		WHERE (
				E_Hour = (SELECT E_Hour FROM deleted) and  E_NIF = (SELECT E_NIF FROM deleted) and E_Day = (SELECT E_Day FROM deleted) and E_ID = (SELECT E_ID FROM deleted)
			  );
	END
GO

CREATE TRIGGER deleteAppointment ON LarIdosos.Appointment
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE LarIdosos.Appointment SET Disable = 1
		WHERE (
				ID = (SELECT ID FROM deleted)
			  );
	END
GO

CREATE TRIGGER deleteDisease ON LarIdosos.DIAGNOSED
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE LarIdosos.DIAGNOSED SET Disable = 1
		WHERE (
				E_Name = (SELECT E_Name FROM deleted) and  E_NIF = (SELECT E_NIF FROM deleted)
			  );
	END
GO





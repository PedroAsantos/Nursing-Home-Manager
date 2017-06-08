use exemplo1;

CREATE TABLE exemplo1.BEDROOM(
	RoomNumber		INT 		NOT NULL	IDENTITY(1, 1),
	Capacity		INT,
	CONSTRAINT PKBEDROOM PRIMARY KEY (RoomNumber)
);

CREATE TABLE exemplo1.BED(
	BedNumber		INT 		NOT NULL	IDENTITY(1, 1),
	E_RoomNumber	INT,
	CONSTRAINT PKBED PRIMARY KEY (BedNumber),
	CONSTRAINT FKBED FOREIGN KEY (E_RoomNumber) REFERENCES exemplo1.BEDROOM(RoomNumber)
);

CREATE TABLE exemplo1.DOCTOR(
	NIF		varchar(9) 		NOT NULL,
	Name	varchar(30),
	Phone	INT,
	Address	varchar(30),
	CONSTRAINT PKDOCTOR PRIMARY KEY (NIF)
);

CREATE TABLE exemplo1.PATIENT(
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
	CONSTRAINT FKPATIENT2 FOREIGN KEY (E_BedNumber) REFERENCES exemplo1.BED(BedNumber)
);

CREATE TABLE exemplo1.DISEASE(
	Name	varchar(15)		NOT NULL,
	CONSTRAINT PKDISEASE PRIMARY KEY (Name)
);

CREATE TABLE exemplo1.DIAGNOSED(
	E_Name	varchar(15)		NOT NULL,
	E_NIF	varchar(9)		NOT NULL,
	Seriousness	INT,
	Disable	BIT,
	CONSTRAINT FKDIAGNOSED FOREIGN KEY (E_Name) REFERENCES exemplo1.DISEASE(Name),
	CONSTRAINT FKDIAGNOSED2 FOREIGN KEY (E_NIF) REFERENCES exemplo1.PATIENT(NIF),
	CONSTRAINT PKDIAGNOSED PRIMARY KEY (E_Name, E_NIF)
);

CREATE TABLE exemplo1.EXITS(
	ID			INT		NOT NULL	IDENTITY(1, 1),
	Check_in	DATE,
	Check_out	DATE,
	CHECK(Check_in < Check_out),
	CONSTRAINT PKEXITS PRIMARY KEY (ID)
);

CREATE TABLE exemplo1.SCHEDULE(
	Day		varchar(15)		NOT NULL,
	Hour	TIME		NOT NULL,
	E_NIF	varchar(9),
	CONSTRAINT FKSCHEDULE FOREIGN KEY (E_NIF) REFERENCES exemplo1.PATIENT(NIF),
	CONSTRAINT PKSCHEDULE PRIMARY KEY (Day, Hour, E_NIF)
);

CREATE TABLE exemplo1.MEDICINE(
	ID		INT		NOT NULL	IDENTITY(1, 1),
	Name	varchar(30)	NOT NULL,
	CONSTRAINT PKMEDICINE PRIMARY KEY (ID)
);
CREATE TABLE exemplo1.TAKING(
	E_Day	varchar(15)	NOT NULL,
	E_Hour	TIME	NOT NULL,
	E_NIF	varchar(9)		NOT NULL,
	E_ID	INT		NOT NULL,
	Dose	INT,
	Disable Bit Default (0),
	CONSTRAINT FKTAKING FOREIGN KEY (E_Day, E_Hour, E_NIF) REFERENCES exemplo1.SCHEDULE(Day, Hour, E_NIF),
	CONSTRAINT FKTAKING2 FOREIGN KEY (E_ID) REFERENCES exemplo1.MEDICINE(ID),
	CONSTRAINT PKTAKING PRIMARY KEY (E_Day, E_Hour, E_NIF, E_ID)
);

CREATE TABLE exemplo1.APPOINTMENT(
	ID				INT		NOT NULL	IDENTITY(1, 1),
	E_DoctorNIF		varchar(9),
	E_PatientNIF	varchar(9),
	Date			DATETIME,
	Speciality		varchar(30),
	Disable			BIT DEFAULT 0,
	CONSTRAINT PKAPPOINTMENT PRIMARY KEY (ID),
	CONSTRAINT FKAPPOINTMENT FOREIGN KEY (E_DoctorNIF) REFERENCES exemplo1.DOCTOR(NIF),
	CONSTRAINT FKAPPOINTMENT2 FOREIGN KEY (E_PatientNIF) REFERENCES exemplo1.PATIENT(NIF)
);

CREATE TABLE exemplo1.VISIT(
	ID		INT		NOT NULL	IDENTITY(1, 1),
	Date	DATETIME,
	CONSTRAINT PKVISIT PRIMARY KEY (ID)
);

CREATE TABLE exemplo1.RECEIVED(
	E_NIF		varchar(9)		NOT NULL,
	E_IDVisit	INT		NOT NULL,
	CONSTRAINT FKRECEIVED FOREIGN KEY (E_NIF) REFERENCES exemplo1.PATIENT(NIF),
	CONSTRAINT FKRECEIVED2 FOREIGN KEY (E_IDVisit) REFERENCES exemplo1.VISIT(ID),
	CONSTRAINT PKRECEIVED PRIMARY KEY (E_NIF, E_IDVisit)
);

CREATE TABLE exemplo1.ACCOMPANIED(
	NIF_Patient		varchar(9)		NOT NULL,
	NIF_Doctor		varchar(9)		NOT NULL,
	CONSTRAINT FKACCOMPAINED FOREIGN KEY (NIF_Doctor) REFERENCES exemplo1.DOCTOR(NIF),
	CONSTRAINT FKACCOMPAINED2 FOREIGN KEY (NIF_Patient) REFERENCES exemplo1.PATIENT(NIF),
	CONSTRAINT PKACCOMPAINED PRIMARY KEY (NIF_Patient, NIF_Doctor)
);


CREATE TABLE exemplo1.VISITOR(
	CC		varchar(9)		NOT NULL,
	Name	varchar(30),
	Address	varchar(30),
	Phone	INT,
	CONSTRAINT PKVISITOR PRIMARY KEY (CC)
);

CREATE TABLE exemplo1.VISITED(
	E_IDVisit		INT		NOT NULL,
	E_CCVisitor		varchar(9)		NOT NULL,
	CONSTRAINT FKVISITED FOREIGN KEY (E_IDVisit) REFERENCES exemplo1.VISIT(ID),
	CONSTRAINT FKVISITED2 FOREIGN KEY (E_CCVisitor) REFERENCES exemplo1.VISITOR(CC),
	CONSTRAINT PKVISITED PRIMARY KEY (E_IDVisit, E_CCVisitor)
);

CREATE TABLE exemplo1.FAMILY(
	E_CC			varchar(9)		NOT NULL,
	Relationship	varchar(30),
	CONSTRAINT FKFAMILY FOREIGN KEY (E_CC) REFERENCES exemplo1.VISITOR(CC),
	CONSTRAINT PKFAMILY PRIMARY KEY (E_CC)
);

CREATE TABLE exemplo1.NOT_FAMILY(
	E_CC			varchar(9)		NOT NULL,
	KinshipDegree	varchar(15),
	CONSTRAINT FKNOT_FAMILY FOREIGN KEY (E_CC) REFERENCES exemplo1.VISITOR(CC),
	CONSTRAINT PKNOT_FAMILY PRIMARY KEY (E_CC)
);

CREATE TABLE exemplo1.DEPENDENT(
	E_CC			varchar(9)		NOT NULL,
	E_PatientNIF	varchar(9)		NOT NULL,
	CONSTRAINT FKDEPENDENT FOREIGN KEY (E_CC) REFERENCES exemplo1.FAMILY(E_CC),
	CONSTRAINT FKDEPENDENT2 FOREIGN KEY (E_PatientNIF) REFERENCES exemplo1.PATIENT(NIF),
	CONSTRAINT PKDEPENDENT PRIMARY KEY (E_CC)
);

CREATE TABLE exemplo1.LEAVES(
	ID			INT		NOT NULL,
	E_NIF		varchar(9)		,
	CONSTRAINT FKLEAVES FOREIGN KEY (ID) REFERENCES exemplo1.EXITS(ID),
	CONSTRAINT FKLEAVES2 FOREIGN KEY (E_NIF) REFERENCES exemplo1.PATIENT(NIF),
	CONSTRAINT PKLEAVES PRIMARY KEY (ID, E_NIF)
);
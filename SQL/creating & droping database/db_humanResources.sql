
-- CREATE DATABASE exemplo1;
-- CREATE SCHEMA exemplo1;


CREATE TABLE exemplo1.Type(
	ID			INT 		NOT NULL,
	Designation	VARCHAR(30),
	PRIMARY KEY (ID)
);

CREATE TABLE exemplo1.HumanResources(
	NIF			INT 		NOT NULL,
	Name		VARCHAR(30),
	Phone		INT,
	Address		VARCHAR(30),
	Salary		INT,
	StartDate	DATE,
	E_IDType	INT 		NOT NULL,
	PRIMARY KEY (NIF),
	FOREIGN KEY (E_IDType) REFERENCES exemplo1.Type(ID)
);

CREATE TABLE exemplo1.Shift(
	ID					INT 		NOT NULL,
	Day					VARCHAR(30),
	BeginOfWorkShift	TIME,
	EndOfWorkShift		TIME,
	PRIMARY KEY (ID)
);

CREATE TABLE exemplo1.ShiftInstance(
	ID					INT 		NOT NULL,
	InicialDate			TIME,
	FinalDate			TIME,
	E_NIF				INT 		NOT NULL,
	E_IDShift			INT 		NOT NULL,
	PRIMARY KEY (ID),
	FOREIGN KEY (E_NIF) REFERENCES exemplo1.HumanResources(NIF),
	FOREIGN KEY (E_IDShift) REFERENCES exemplo1.Shift(ID)
);

CREATE TABLE exemplo1.Faults(
	Date		TIME,
	E_ID		INT		NOT NULL,
	FOREIGN KEY (E_ID) REFERENCES exemplo1.ShiftInstance(ID),
	PRIMARY KEY (Date, E_ID)
);
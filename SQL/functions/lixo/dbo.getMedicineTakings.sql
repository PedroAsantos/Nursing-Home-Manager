CREATE FUNCTION dbo.getMedicineTakings(@NIF varchar(9))
RETURNS TABLE
AS
	RETURN(
		select NIF, exemplo1.PATIENT.Name as PatientName, exemplo1.MEDICINE.Name as MedicineName, E_Day, E_Hour, Dose from exemplo1.PATIENT
			join exemplo1.SCHEDULE on NIF = E_NIF
			join exemplo1.TAKING on Day = E_Day and Hour = E_Hour and exemplo1.TAKING.E_NIF = exemplo1.SCHEDULE.E_NIF
			join exemplo1.MEDICINE on E_ID = ID
			where NIF = @NIF
		)
GO
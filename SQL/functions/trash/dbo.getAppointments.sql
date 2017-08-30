CREATE FUNCTION dbo.getAppointments()
RETURNS TABLE
AS
	RETURN
		SELECT E_DoctorNIF as DoctorNIF, exemplo1.DOCTOR.Name as DoctorName, E_PatientNIF as PatientNIF, exemplo1.PATIENT.Name as PatientName, Date, Speciality from (exemplo1.APPOINTMENT JOIN exemplo1.Doctor on E_DoctorNIF = exemplo1.DOCTOR.NIF) JOIN exemplo1.PATIENT ON E_PatientNIF = exemplo1.PATIENT.NIF
GO
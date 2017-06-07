CREATE FUNCTION dbo.getPatientAppointments(@NIF varchar(9))
RETURNS TABLE
AS
	RETURN(
			select  exemplo1.DOCTOR.Name, Date, Speciality, exemplo1.APPOINTMENT.ID FROM (exemplo1.APPOINTMENT JOIN exemplo1.DOCTOR on E_DoctorNIF = exemplo1.DOCTOR.NIF)
		  )		
GO
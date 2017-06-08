	CREATE FUNCTION dbo.searchAppointments(@DoctorNif VARCHAR(9)=NULL,@PatientNif VARCHAR(9)=NULL,@DoctorName varchar(30)=NULL,@PatientName varchar(30)=NULL,@Date DateTime=NULL,@Speciality varchar(30)=NULL)
	RETURNS TABLE
	AS
		RETURN
			SELECT E_DoctorNIF as DoctorNIF, exemplo1.DOCTOR.Name as DoctorName, E_PatientNIF as PatientNIF, exemplo1.PATIENT.Name as PatientName, Date, Speciality from (exemplo1.APPOINTMENT JOIN exemplo1.Doctor on E_DoctorNIF = exemplo1.DOCTOR.NIF) JOIN exemplo1.PATIENT ON E_PatientNIF = exemplo1.PATIENT.NIF 
			WHERE    (@DoctorNif IS NULL OR exemplo1.APPOINTMENT.E_DoctorNIF = @DoctorNif)
					AND
						(@PatientNif IS NULL OR exemplo1.APPOINTMENT.E_PatientNIF = @PatientNif)
					AND
						(@DoctorName IS NULL OR exemplo1.DOCTOR.Name = @DoctorName)
					AND
						(@PatientName IS NULL OR exemplo1.PATIENT.Name = @PatientName)
					AND  
						(@Date IS NULL OR CAST(Date as DATE) = @Date)
					AND
						(@Speciality IS NULL OR exemplo1.APPOINTMENT.Speciality = @Speciality)
				
	GO	

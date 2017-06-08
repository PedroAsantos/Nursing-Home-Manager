CREATE PROCEDURE dbo.sp_addPatientAppointment
	@PatientNif	varchar(9),
	@DoctorNif	varchar(9),
	@Date	DateTime,
	@Speciality varchar(30)
 
AS
BEGIN
		if  (Exists(Select * from exemplo1.APPOINTMENT  Where E_PatientNIF=@PatientNif AND E_DoctorNIF = @DoctorNif AND Date = @Date And Speciality = @Speciality ))
		begin
				raiserror('O paciente já tem associada esta consulta',16,1);
				return;
		end
		if(Exists(Select * from exemplo1.APPOINTMENT  Where E_PatientNIF=@PatientNif AND Date = @Date ))
		begin
				raiserror('O paciente já tem consulta para essa hora nesse dia',16,1);
				return;
		end
		
		--Declare @dateClosest DateTime;
		Declare @disable BIT;
		SET @Disable = 0 ; 
		--SELECT TOP 1 @dateClosest = Date  FROM exemplo1.APPOINTMENT WHERE E_PatientNIF=@PatientNif AND exemplo1.APPOINTMENT.Date < @Date ORDER BY exemplo1.APPOINTMENT.Date DESC;
		
	--	if(DATEDIFF(hour, @dateClosest,@Date) < 1)
	--	begin
	--		INSERT INTO exemplo1.APPOINTMENT values (
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
			 INSERT INTO exemplo1.APPOINTMENT values (
					@DoctorNif,
					@PatientNif,
					@Date,
					@Speciality,
					@Disable
			 )

			return;
	--	end
end

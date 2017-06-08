CREATE TRIGGER deleteAppointment ON exemplo1.Appointment
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE exemplo1.Appointment SET Disable = 1
		WHERE (
				ID = (SELECT ID FROM deleted)
			  );
	END
GO




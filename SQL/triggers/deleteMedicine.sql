CREATE TRIGGER deleteMedicine ON exemplo1.TAKING
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE exemplo1.TAKING SET Disable = 1
		WHERE (
				E_Hour = (SELECT E_Hour FROM deleted) and  E_NIF = (SELECT E_NIF FROM deleted) and E_Day = (SELECT E_Day FROM deleted) and E_ID = (SELECT E_ID FROM deleted)
			  );
	END
GO




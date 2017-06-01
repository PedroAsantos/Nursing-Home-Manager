CREATE TRIGGER deleteDisease ON exemplo1.DIAGNOSED
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE exemplo1.DIAGNOSED SET Disable = 1
		WHERE (
				E_Name = (SELECT E_Name FROM deleted) and  E_NIF = (SELECT E_NIF FROM deleted)
			  );
	END
GO
CREATE TRIGGER deleteType ON exemplo1.Type
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE exemplo1.Type SET Disable = 1
		WHERE (
				ID = (SELECT ID FROM deleted)
			  );
	END
GO
CREATE TRIGGER finishShiftofHumanResource ON exemplo1.Shift
INSTEAD OF DELETE
AS
	BEGIN
		UPDATE exemplo1.ShiftInstance SET FinalDate = GETDATE()
		WHERE (
				E_IDShift = (SELECT ID FROM deleted)
			  );
	END
GO




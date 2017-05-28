CREATE FUNCTION dbo.getFreeBeds(@RoomNumber INT)
RETURNS TABLE
AS
	RETURN
		SELECT BedNumber FROM (
		SELECT E_RoomNumber, BedNumber FROM exemplo1.BED
		EXCEPT
		SELECT E_RoomNumber, BedNumber FROM exemplo1.PATIENT join exemplo1.BED 
			ON E_BedNumber = BedNumber) AS tmp join exemplo1.BEDROOM on tmp.E_RoomNumber = RoomNumber
		WHERE tmp.E_RoomNumber = @RoomNumber
GO

-- Execution example
-- SELECT * from dbo.getFreeBeds( id do quarto)	

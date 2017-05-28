CREATE FUNCTION dbo.getFreeRoomsAndBeds()
RETURNS TABLE
AS
	RETURN(
		select RoomNumber, BedNumber from exemplo1.BED 
			join exemplo1.BEDROOM on E_RoomNumber = RoomNumber

		except
		select RoomNumber, BedNumber from exemplo1.PATIENT 
			join exemplo1.BED on E_BedNumber = BedNumber
			join exemplo1.BEDROOM on E_RoomNumber = RoomNumber
			)
GO
-- Execution example
-- SELECT * FROM dbo.getFreeRoomsAndBeds()
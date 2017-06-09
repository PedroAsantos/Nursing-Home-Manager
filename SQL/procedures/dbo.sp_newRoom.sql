CREATE PROCEDURE dbo.sp_newRoom(@capacity INT=2, @NewRoomId int OUTPUT)
as
begin	IF(@capacity < 0 )
		BEGIN
			RAISERROR('Capacity must be a number positive',16,1)
		END

		Select @NewRoomId = RoomNumber from exemplo1.BEDROOM order by RoomNumber asc
		
		insert into exemplo1.BEDROOM (Capacity) values (@capacity);
		
		While(@capacity > 0)
		BEGIN
			INSERT INTO exemplo1.BED (E_RoomNumber) values (@NewRoomId);
			SET @capacity = @capacity - 1;
		END

		
	RETURN
END
GO
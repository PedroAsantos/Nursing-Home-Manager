CREATE PROCEDURE dbo.sp_newType(@type VARCHAR(30))
as
begin
		DECLARE @ID int;
		SELECT @ID = id from exemplo1.Type WHere Designation = @type
		if(@@ROWCOUNT = 0)
		BEGIN
			insert into exemplo1.Type (Designation,Disable) values (@type,0);
			RETURN;
		END
		
		UPDATE exemplo1.Type SET Disable = 0 WHERE id = @ID;
		RETURN;
END
GO


CREATE PROCEDURE dbo.newDisease (@Name varchar(30))
as
begin

		if  (Exists(Select Name from exemplo1.DISEASE where Name=@Name ))
			RETURN

		INSERT INTO exemplo1.DISEASE values(
				@Name
		)
		RETURN
END
GO
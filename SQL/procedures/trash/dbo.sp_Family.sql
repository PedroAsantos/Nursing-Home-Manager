CREATE PROCEDURE dbo.sp_Family (@CC varchar(9), @Relationship varchar(30))
AS
BEGIN
		if  (Exists(Select CC from exemplo1.VISITOR where CC=@CC ))
				INSERT INTO exemplo1.FAMILY values(
					@CC,
					@Relationship
				)
		RETURN
end
go




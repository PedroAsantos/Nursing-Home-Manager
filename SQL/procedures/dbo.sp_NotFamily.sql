CREATE PROCEDURE dbo.sp_NotFamily (@CC varchar(9), @Kinshipdegree varchar(30))
AS
BEGIN
		if  (Exists(Select CC from exemplo1.VISITOR where CC=@CC ))
				INSERT INTO exemplo1.NOT_FAMILY values(
					@CC,
					@Kinshipdegree
				)
		RETURN
end
go
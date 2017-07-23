CREATE PROCEDURE dbo.newVisitor ( @CC varchar(9), @Name varchar(30), @Address varchar(15), @Phone int)
as
begin

		if  (Exists(Select CC from exemplo1.VISITOR where CC=@CC ))
			RETURN
		if  (Exists(Select NIF from exemplo1.PATIENT where NIF=@CC ))
			RETURN

		INSERT INTO exemplo1.VISITOR values(
				@CC,
				@Name,
				@Address,
				@Phone
		)
		RETURN
END
GO
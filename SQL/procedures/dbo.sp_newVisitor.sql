CREATE PROCEDURE dbo.sp_newVisitor ( @CC varchar(9), @Name varchar(30), @Address varchar(15), @Phone int,@RelationShip VARCHAR(30),@Family BIT)
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
		if (@Family = 1)
		BEGIN
			Insert Into exemplo1.FAMILY values(@CC,@RelationShip);
		END
		ELSE
		BEGIN
			INSERT INTO exemplo1.NOT_FAMILY values(@CC,@RelationShip)
		END
		RETURN
END
GO
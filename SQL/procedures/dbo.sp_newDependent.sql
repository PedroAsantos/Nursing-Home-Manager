CREATE PROCEDURE dbo.sp_newDependent (@E_NIF varchar(9), @Name varchar(30), @CC varchar(9), @Phone int, @Address varchar(30), @Relationship varchar(30))
as
begin
		if  (Exists(Select NIF from exemplo1.PATIENT where NIF=@E_NIF ))
				if  (not Exists(Select CC from exemplo1.VISITOR where CC=@CC ))
					INSERT INTO exemplo1.VISITOR values (
							@CC,
							@Name,
							@Address,
							@Phone
					)
			if(not Exists(SELECT * FROM exemplo1.FAMILY where E_CC=@CC))
			BEGIN
				INSERT INTO exemplo1.FAMILY values (
					@CC,
					@Relationship
			)
			END
				
			INSERT INTO exemplo1.DEPENDENT values (
					@CC,
					@E_NIF
			)

		RETURN
END
GO
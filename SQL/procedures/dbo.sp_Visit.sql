CREATE PROCEDURE dbo.sp_Visit (@Date DATETIME, @NIF varchar(9), @CC varchar(9))
AS
BEGIN
		declare @tmp int;
		if  (Exists(Select NIF from exemplo1.PATIENT where NIF=@NIF ))
					if  (Exists(Select CC from exemplo1.VISITOR where CC=@CC ))
								INSERT INTO exemplo1.VISIT values (
										@Date
								)
								Select top 1 @tmp = ID from exemplo1.VISIT order by ID desc

								INSERT INTO exemplo1.RECEIVED(E_NIF, E_IDVisit) values(
										@NIF, @tmp 		
								)
								INSERT INTO exemplo1.VISITED(E_IDVisit, E_CCVisitor) values(
										@tmp, @CC 		
								)		
		RETURN
end
go

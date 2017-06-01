CREATE PROCEDURE [dbo].[sp_insertPATIENT]
	@NIF	varchar(9),
	@Name	varchar(30),
	@Sex	varchar(1),
	@Phone	INT,
	@Age	INT,
	@Check_in	DATE,
	@Check_out	DATE,
	@Authorization_to_leave BIT,
	@E_BedNumber	INT,
	@Entry_Date	DATE,
	@Exit_Date	DATE 
 
AS
BEGIN
		if  (Exists(Select NIF from exemplo1.PATIENT where NIF=@NIF ))
					return
		else
			 INSERT INTO exemplo1.PATIENT values (
					@NIF,
					@Name,
					@Sex,
					@Phone,
					@Age,
					@Check_in,
					@Check_out,
					@Authorization_to_leave,
					@E_BedNumber,
					@Entry_Date,
					@Exit_Date
				  )
		return
end

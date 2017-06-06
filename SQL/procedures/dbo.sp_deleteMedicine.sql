CREATE PROCEDURE dbo.sp_deleteMedicine(@E_Day varchar(15), @E_Hour time, @E_NIF varchar(9), @medicineID int)
AS
BEGIN
	Delete FROM EXEMPLO1.TAKING WHERE E_day = @E_Day AND E_Hour = @E_Hour AND E_NIF = @E_NIF AND E_ID = @medicineID; 
	RETURN
end
go
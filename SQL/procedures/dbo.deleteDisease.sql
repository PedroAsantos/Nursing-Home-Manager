CREATE PROCEDURE dbo.deleteDisease (@E_Name varchar(30), @E_NIF varchar(9))
AS
BEGIN
	Delete FROM EXEMPLO1.DIAGNOSED WHERE (E_Name = @E_Name and E_NIF = @E_NIF)
	RETURN
end
go
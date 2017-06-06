CREATE PROCEDURE dbo.sp_finishShiftOfHumanResources (@ID INT)
AS
BEGIN
	Delete FROM EXEMPLO1.shift WHERE (ID = @ID )
	RETURN
end
go
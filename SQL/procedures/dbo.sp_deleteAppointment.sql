CREATE PROCEDURE dbo.sp_deleteAppointment(@ID INT)
AS
BEGIN
	Delete FROM EXEMPLO1.APPOINTMENT WHERE ID = @ID;
	RETURN
end
go
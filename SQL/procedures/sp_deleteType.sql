CREATE PROCEDURE dbo.sp_deleteType (@id INT)
AS
BEGIN
	Delete FROM EXEMPLO1.Type WHERE (id=@id)
	RETURN
end
go
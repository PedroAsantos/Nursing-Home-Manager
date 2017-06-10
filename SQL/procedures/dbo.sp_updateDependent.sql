CREATE PROCEDURE dbo.sp_updateDependent
	@E_NIF			 varchar(9),
	@CC				varchar(9),
	@Name			varchar(30),
	@Address			VARCHAR(30),
	@Phone			INT,
	@RelationShip	VARCHAR(30)
AS
BEGIN
	
			
				IF(NOT EXISTS(SELECT * FROM exemplo1.VISITOR WHERE CC = @CC))
				BEGIN
					EXEC sp_newDependent @E_NIF, @Name, @CC, @Phone, @Address, @Relationship;
					RETURN;
				END
				ELSE
				BEGIN
					IF(NOT EXISTS(SELECT * FROM exemplo1.dependent WHERE E_CC = @CC))
					BEGIN
						UPDATE exemplo1.VISITOR SET	Name = @Name, Phone = @Phone, Address = @Address WHERE CC=@CC;
						INSERT INTO exemplo1.DEPENDENT values (@CC,@E_NIF)
						return;
					END
					ELSE
					BEGIN 
						UPDATE exemplo1.VISITOR SET	Name = @Name, Phone = @Phone, Address = @Address WHERE CC=@CC;
						return;
					END
				END
END
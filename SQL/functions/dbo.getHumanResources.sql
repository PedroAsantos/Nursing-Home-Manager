CREATE FUNCTION dbo.getHumanResources(@WorkerName varchar(30)=NULL,@WorkerNif varchar(9)=NULL,@WorkerPhone INT=NULL,@WorkerAddress varchar(30)=NULL,@Designation varchar(30)=NULL,@PageNumber INT, @RowsPage INT)
RETURNS TABLE
AS
	RETURN(
			select NIF,Name,Phone,Address,Salary,StartDate,Designation from exemplo1.HumanResources
			join exemplo1.Type on E_IDType = ID WHERE (@WorkerName IS NULL OR Name = @WorkerName)
				AND
					(@WorkerNif IS NULL OR NIF = @WorkerNif)
				AND
					(@WorkerPhone IS NULL OR Phone = @WorkerPhone)
				AND  
					(@WorkerAddress IS NULL OR SOUNDEX([Address]) = SOUNDEX(@WorkerAddress))
				AND
					(@Designation IS NULL OR Designation = @Designation) ORDER BY  NIF
			OFFSET ((@PageNumber - 1) * @RowsPage) ROWS
			FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO
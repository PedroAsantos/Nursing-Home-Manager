CREATE FUNCTION dbo.getVisitors(@VisitorName varchar(30)=NULL,@VisitorCC varchar(9)=NULL,@VisitorPhone INT=NULL,@PageNumber INT, @RowsPage INT)
RETURNS TABLE
AS
	RETURN(
		select Name, CC, Phone from exemplo1.Visitor 	WHERE
					(@VisitorName IS NULL OR  exemplo1.VISITOR.Name = @VisitorName)
				AND
					(@VisitorCC IS NULL OR CC = @VisitorCC)
				AND
					(@VisitorPhone IS NULL OR Phone = @VisitorPhone) ORDER BY exemplo1.VISITOR.CC
				OFFSET ((@PageNumber - 1) * @RowsPage) ROWS FETCH NEXT @RowsPage ROWS ONLY
		  )		
GO

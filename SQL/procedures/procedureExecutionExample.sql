EXEC dbo.sp_insertPATIENT  @nif='25547895Z', @Name='dasdsa', @Sex = 'f',@Phone=96477771, @Age=21, @Check_in = '1992-10-10', @Check_out= '1993-10-10',@Authorization_to_leave = 1, @E_BedNumber = 1, @Entry_Date='1992-10-10', @Exit_Date=null
EXEC dbo.sp_insertPATIENT  @nif='25547895Z', @Name='dasdsa', @Sex = 'f',@Phone=96477771, @Age=21, @Check_in = '1992-10-10', @Check_out= '1993-10-10',@Authorization_to_leave = 1, @E_BedNumber = 1, @Entry_Date='1992-10-10', @Exit_Date=null

Exec dbo.newDiagnosed @E_name = 'dasdsa', @E_NIF ='25547895Z', @Seriousness = 3, @Disable = 0
Exec dbo.newDiagnosed @E_name = 'bbbbb', @E_NIF ='25547895Z', @Seriousness = 3, @Disable = 0


select * from exemplo1.DIAGNOSED

'25547895Z'




update exemplo1.Diagnosed set Disable = 0

exec dbo.deleteDisease @E_Name = 'bbbbb' , @E_NIF= '25547895Z'



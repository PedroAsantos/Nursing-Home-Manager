EXEC dbo.sp_insertPATIENT  @nif='25547895Z', @Name='dasdsa', @Sex = 'f',@Phone=96477771, @Age=21, @Check_in = '1992-10-10', @Check_out= '1993-10-10',@Authorization_to_leave = 1, @E_BedNumber = 1, @Entry_Date='1992-10-10', @Exit_Date=null
EXEC dbo.sp_insertPATIENT  @nif='25547895Z', @Name='dasdsa', @Sex = 'f',@Phone=96477771, @Age=21, @Check_in = '1992-10-10', @Check_out= '1993-10-10',@Authorization_to_leave = 1, @E_BedNumber = 1, @Entry_Date='1992-10-10', @Exit_Date=null

Exec dbo.newDiagnosed @E_name = 'dasdsa', @E_NIF ='25547895Z', @Seriousness = 3, @Disable = 0
Exec dbo.newDiagnosed @E_name = 'bbbbb', @E_NIF ='25547895Z', @Seriousness = 3, @Disable = 0

Exec dbo.updateDoctor @NIF = '57432844Z', @Name = 'Joao Tinis', @Phone = '964772218' , @Address = 'Rua de Belem, Cristo, Aveiro'

select * from exemplo1.doctor

exec dbo.sp_insertHumanResources @NIF ='25588895Z', @Name = 'tiago', @Phone = 967788991, @Address = 'rua de nao sei que', @Salary = 530, @Start_Date = '1992-10-10', @E_IDType = 1

update exemplo1.Diagnosed set Disable = 0

exec dbo.deleteDisease @E_Name = 'bbbbb' , @E_NIF= '25547895Z'

Exec dbo.newAccompanied @NIF_Patient ='25547895Z'  ,@NIF_Doctor='57882373Z' 
delete from exemplo1.ACCOMPANIED where NIF_Patient ='25547895Z'

insert into exemplo1.Type values ('Enfermeiro')

select * from exemplo1.ACCOMPANIED

select * from dbo.getHumanResources()




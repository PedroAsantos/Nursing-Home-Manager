CREATE PROCEDURE dbo.genericSearch(@Sex varchar(1) = null , @age1 int = null, @age2 int= null, @authorization bit= null)
AS
begin
			if (@Sex is not null and @age1 is not null and @age2 is not null and @authorization is not null)
				Select * from exemplo1.PATIENT
				WHERE Sex = @Sex and Age <= @age2 and Age >= @age1 and Authorization_to_leave = @authorization
				
			else if (@age1 is not null and @age2 is not null and @authorization is not null)
				Select * from exemplo1.PATIENT
				WHERE Age <= @age2 and Age >= @age1 and Authorization_to_leave = @authorization
				
			else if (@Sex is not null and @age1 is not null and @age2 is not null)
				Select * from exemplo1.PATIENT
				WHERE Sex = @Sex and Age <= @age2 and Age >= @age1 

			else if (@Sex is not null and @authorization is not null )
				Select * from exemplo1.PATIENT
				WHERE Sex = @Sex and Authorization_to_leave = @authorization

			else if (@Sex is not null)
				Select * from exemplo1.PATIENT
				WHERE Sex = @Sex 
			
			else if (@authorization is not null)
				Select * from exemplo1.PATIENT
				WHERE Authorization_to_leave = @authorization 

			else if (@age1 is not null and @age2 is not null)
				Select * from exemplo1.PATIENT
				WHERE Age <= @age2 and Age >= @age1 
			else 
				return
	return
end
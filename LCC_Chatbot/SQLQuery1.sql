use [Freshers-Training]

create table rajesh_SecretQuestions
 (
  Id int identity(1,1) primary key,
  Intent nvarchar(100) not null,
  Questions nvarchar(4000) not null,
);
select * from rajesh_SecretQuestions

 insert into rajesh_SecretQuestions(Intent,Questions) values('Register','What is your pet name');
  insert into rajesh_SecretQuestions(Intent,Questions) values('Register','Who is Your favourite Player');
   insert into rajesh_SecretQuestions(Intent,Questions) values('Register','Mention your Best Friend name');
    insert into rajesh_SecretQuestions(Intent,Questions) values('Register','In which school you completed your X');
	 insert into rajesh_SecretQuestions(Intent,Questions) values('Register','what is your favourite sport');
	  insert into rajesh_SecretQuestions(Intent,Questions) values('Register','Which movie do you liked the most');


	  --------------------------------------------------------------------
create or alter procedure QuestionList
(@pIntent nvarchar(100))
as 
begin
select Questions from  rajesh_SecretQuestions where Intent=@pIntent
end

exec QuestionList 'Register'
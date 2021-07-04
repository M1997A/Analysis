select * from Clients
select * from ClientAnalysis
select * from Results 
select * from AnalysisFeatures
select * from AnalysisType

Delete from Clients where Age = 30
Delete from Results
Delete from Clients
Delete from ClientAnalysis

Delete from Clients where Doctor = 'Dr'



print Rand()*10e6

Declare @i int = 1;
While @i <= 50
Begin
insert into Clients values(CONCAT('Client-',@i),30,'male',CONCAT('Adress-',@i),RAND()*10e7,'no','Dr',RAND()*10e6,GETDATE());
set @i = @i + 1
end

select * from Clients


Create table M
(
Id int ,
Name nvarchar(20),
date Date 
)
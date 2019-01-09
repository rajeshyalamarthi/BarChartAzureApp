use ChatbotInfo

create table FinalOrder
           (
           Id int identity(1,1) primary key,
		   UserName nvarchar(1000),
		   FinalOrder nvarchar(4000),
		   OrderTime datetime,
		   Channel nvarchar(100)
		   )



create table OrderErrorlog
           (
		   Id int identity(1,1) primary key,
		   ErrorMessage nvarchar(4000),
		   ErrorTime Datetime
		   )

create table ConversationLog
           (
		   Id int identity(1,1) primary key,
		   UserName nvarchar(1000),
		    UserRequest nvarchar(4000),
		   ConversationData nvarchar(4000),
		   LoggingTime Datetime,
		   Channel nvarchar(100)
		   )





select * from FinalOrder
select * from OrderErrorlog
select * from  ConversationLog
alter table FinalOrder
add UserName nvarchar(1000);


--Final Order Procedure
 create or alter procedure FinalOrderProc
         (
		   @UserName varchar(1000),
		  @FinalOrder nvarchar(4000),
		  @OrderTime datetime,
		  @Channel nvarchar(100),		
		  @action nvarchar(10)
		  )

            as
            begin
            set nocount on;
            if @action='INSERT'	
            begin
             insert into FinalOrder
			 (
			 UserName,
			 FinalOrder,
			 OrderTime,
			 Channel
		
			 )
			  values
			  (@UserName,
			  @FinalOrder,
			  @OrderTime,
			  @Channel
			  
			  )
end
end



--Errorlog Procedure

Create or alter procedure OrderErrorlogproc
              (
			  @ErrorMessage nvarchar(4000),
			  @ErrorTime datetime,
			  @action nvarchar(10)
			  )

as
begin
set nocount on;
if @action='INSERT'	
begin
insert into OrderErrorlog(
                         ErrorMessage,
						 ErrorTime
						 ) 
						 values
						 (
						 @ErrorMessage,
						 @ErrorTime
						 )
end
end


---ConversationLog Procedure
 create or alter procedure ConersationLog
         (
		  @UserName nvarchar(1000),
		   @UserRequest nvarchar(4000),
		  @ConversationData nvarchar(4000),
		  @LoggingTime datetime,
		  @Channel nvarchar(100),
		  @action nvarchar(10)
		  )
            as
            begin
            set nocount on;
            if @action='INSERT'	
            begin
             insert into ConversationLog
			 (
			 UserName,
			 UserRequest,
			 ConversationData,
			 LoggingTime,
			 Channel
			 )
			  values
			  (
			  @UserName,
			  @UserRequest,
			  @ConversationData,
			  @LoggingTime,
			  @Channel
			  )
end
end

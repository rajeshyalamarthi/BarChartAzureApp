use [Freshers-Training]

create table rajesh_StaticQuestions
 (
  Id int identity(1,1) primary key,
  Intent nvarchar(100) not null,
  BotResponse nvarchar(4000) not null,
  Attachments nvarchar(4000)
);

select * from rajesh_StaticQuestions

insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkDefine','A heart attack is the death of heart muscle due to the loss of blood supply. Your heart is a powerful muscular pump that drives blood around your body. To keep your heart healthy, the heart muscle needs to get a constant supply of oxygen-containing blood from the coronary arteries. If one of the coronary arteries becomes blocked – for example, by a blood clot – part of your heart muscle may be starved of oxygen and may become permanently damaged. This is what happens if you have a heart attack.
<ul><li><a href="https://www.medicinenet.com/script/main/art.asp?articlekey=9658">Definition of Heart Muscle</a></li></ui>','')
----------------------------------------------------------------------------------
insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkSymptoms','The symptoms of a heart attack can range from a severe pain in the centre of the chest, to having mild chest discomfort that makes you feel generally unwell.
<ul><li>You may be having a heart attack</li> 
<li>If you get a crushing pain, or heaviness or tightness in your chest, or </li>
<li> If you get a pain in your arm, throat, neck, jaw, back or stomach</li>
<li>You may also sweat, or feel light-headed, sick, or short of breath.</li>
 Besides these more classical symptoms, there are other symptoms such as shortness of breath, weakness, sleep disturbance and fatigue. This type of clinical presentation is more commonly seen in women and in younger adults.
A heart attack may also cause the rhythm of the heart to be disturbed. However, sometimes a heart attack is ‘silent’ and produces little discomfort. For example, in some people the discomfort is so mild that they never report it to their doctor. You may not even know you have had a heart attack until you have a medical test for something else later on, or a routine medical examination reveals that you have had a heart attack.</li></ui>
','')
-------------------------------------------------------------------------------------------
insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkFirstSteps','This is what to do in case you are not sure you are having a heart attack:
<ul><li>Stop what you are doing</li> 
<li>Sit down and rest</li> 
<li>Use the medication (spray or tablets) if you are advised to do so by medical experts. If you don’t use medication, stay resting and try to stay calm.</li> 
<li>Dial your local emergency contact number if the pain, discomfort or tightness 
  continues, especially if it has not gone within 15 minutes (dont wait longer than
  this),</li> 
<li>If you are not allergic to aspirin, chew an adult aspirin tablet (300mg) if there is one 
  easily available. If you don’t have an aspirin next to you, or if you don’t know if you 
  are allergic to aspirin, just stay resting until the ambulance arrives.
  Too many people risk their lives by waiting too long to call for an ambulance. If in 
   doubt, call your local emergency contact number! It could save your life.</li></ul>','')
---------------------------------------------------------------------------------------

insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkCardiacArrest','During a heart attack, there may be disturbances in the heart rhythm. The most
 serious form of this is called ‘ventricular fibrillation’. This is when the electrical
 activity of the heart becomes so chaotic that the heart stops pumping and quivers or
 ‘fibrillates’ instead. This is a cardiac arrest. It can sometimes be corrected by giving a
 large electric shock through the chest wall, using a device called a defibrillator. This
 is often successful in restoring a normal heartbeat and afterwards the person can do 
 just as well as if they had not had the cardiac arrest. If a person has a cardiac arrest,
 they lose consciousness almost at once. There are also no other signs of life such as
 breathing. This is the most extreme emergency. Unless someone starts
 cardiopulmonary resuscitation (CPR) within three to four minutes, the person may
 suffer permanent damage to the brain and other organs.','')
------------------------------------------------------------------------------------------
insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkMeasures','If you think someone is having a heart attack and is conscious, you should:
<ul><li>Get help immediately, ask someone to stay with the person and someone else to call for assistance.</li>
<li>Get the person to sit in a comfortable position</li>
<li>Phone the local emergency number for an ambulance</li>
</br>
If you think someone is having a heart attack and seems to be unconscious, you should:
</br>
</br>
<li>Approach with care, making sure that you, the person and anybody nearby are safe.</li>
<li>To find out if the person is conscious, gently shake him or her, and shout loudly: ‘Are you all right?’ If there is no response, shout for help.</li>
<li>You will need to assess the casualty and take suitable action. Remember A-B-C = airway, breathing, circulation</li>
<li>Call the local emergency number as soon as possible.</li></ul>','')

 -------------------------------------------------------------------------------------------

 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkCPR','CPR means:
<ul><li>Rescue breathing (inflating the lungs by using mouth-to-mouth resuscitation), and</li>
<li>Chest compression (pumping the heart by external cardiac massage), to keep the
breathing and circulation going until the ambulance arrives.</li>
</br>
Ambulance staff is now trained in advanced resuscitation and all emergency
ambulances carry a defibrillator.
<li>Defibrillator</li>
How to use the defibrillator:
<li>video</li>
</br>
A-B-C
Airway: Open the person’s airway by tilting their head back and lifting the chin. Protect the cervical spine as best you can.
Breathing: Check: Look, listen and feel for signs of normal breathing. Only do this for up to 10 seconds. Action: Get help If the person is unconscious and not breathing normally, phone your local Emergency contact number! for an ambulance.
Circulation:
Check for external blood loss and pulse. Action: Cardiopulmonary Resuscitation (CPR).
<li>Cardiopulmonary Resuscitation</li>
First Aid training can save lives! If you are interested in First-Aid + CPR training
contact your local Company Health representative.<ul>','')


-----------------------------------------------------------------------------------------

 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FAQHrtAtkPrecaution','The risk factors – things which increase the risk of having coronary heart disease – 
are:
<ul><li>Physical inactivity</li>
<li>High blood pressure</li>
<li>High blood cholesterol (hypercholesterolemia)</li>
<li>Smoking</li>
<li>Being overweight or obese</li>
<li>Having a family history of heart disease, and</li>
<li>Having diabetes.</li>
Coronary heart disease is largely a preventable disease and, if you do have the
condition, there are several things you can do to prevent it from getting worse. Here
are some areas where you can pro-actively take action to reduce the risks:
<li>If you smoke – stop Smoking</li>
<li>Reduce your blood pressure</li>
<li>Improve your diet to reduce blood cholesterol levels</li>
<li>Keep active</li>
<li>Maintain a healthy weight and body shape</li>
<li>Control diabetes</li></ul>','')
------------------------------------------------------------------------------------------

 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FinanceGlobalCorporateCardApply','#,Here is a link to apply for a Global Corporate Card
<http://sww.Company.com/finance/globalcard/apply/index.html>','')
-----------------------------------------------------------------------------------------


 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('FinanceOut-of-PocketExpensesClaim','#,Here is a link to the guidelines on how to pay out-of-pocket expenses
<http://sww.Company.com/finance/globalcard/card_usage/out_pocket_expenses.html>','')

-------------------------------------------------------------------------------------------------------

 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('EnterpriseCodeofConductFind','#,Here is a link to Code of Conduct
<http://sww.Company.com/ethicsandcompliance/code_of_conduct/index.html>','')

-----------------------------------------------------------------------------------------
 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('ITITHelpdeskSupport','#,Here is a link where you can contact IT Service Desk
<http://sww-ask-gi.Company.com/self-help/helpdesk_numbers/index.aspl>','')
-------------------------------------------------------------------------------------------------
 insert into rajesh_StaticQuestions(Intent,BotResponse,Attachments) values('Welcome','Hello #, How can I help You?','')
-----------------------------------------------------------------------------------------
create table rajesh_DynamicQuestions
 (
  Id int identity(1,1) primary key,
  Intent nvarchar(100) not null,
  Entity nvarchar(100) not null,
  BotResponse nvarchar(4000) not null,
  Attachments nvarchar(4000)
);


select * from rajesh_DynamicQuestions
 insert into rajesh_DynamicQuestions(Intent,Entity,BotResponse,Attachments) values('RealEstateMeetingRoomReservationBook','Canada','# ,Here is a link to the meeting room reservation
<https://Company.condecosoftware.com/FuncLinks/master.asp>','');
 insert into rajesh_DynamicQuestions(Intent,Entity,BotResponse,Attachments) values('RealEstateMeetingRoomReservationBook','UK','# ,Here is a link to the meeting room reservation
<https://Company.condecosoftware.com/FuncLinks/master.asp>','');
 insert into rajesh_DynamicQuestions(Intent,Entity,BotResponse,Attachments) values('RealEstateReceptionRegister','','# ,Here is a link to request a visitor pass and guidelines
<https://eu001-sp.Company.com/sites/AAFAA1191/Access%20Control/Home.aspx>','');
 insert into rajesh_DynamicQuestions(Intent,Entity,BotResponse,Attachments) values('RealEstateShuttleBusSchedule','','# ,Here is a link to the shuttle bus information
<http://sww.Company.com/realestate/locations/europe/netherlands/the_hague/Shuttle-Bus-Service.html>','');
 insert into rajesh_DynamicQuestions(Intent,Entity,BotResponse,Attachments) values('TravelBookITBook','','# ,Here is a link with direct access to Company online booking tool via Carlson Wagonlit
<https://eu015-sp.Company.com/sites/SOS/Travel/Pages/Details.aspx?PKEY=3941>','');

--------------------------------------------------------------------------------------------------
create or alter procedure StaticQuestionsProc 

(@pIntent nvarchar(100),@pBotResponse nvarchar(4000) output)
as 
begin
if(@pIntent='None')
begin
 set @pBotResponse ='Sorry your request cannot be found';
end

else
begin
select @pBotResponse=BotResponse from rajesh_StaticQuestions where Intent=@pIntent ;
end

end

---------------------------------------------------------------------------------
create or alter procedure DynamicQuestionsProc 

(@pIntent nvarchar(100),@pEntity nvarchar(100),@pBotResponse nvarchar(4000) output)
as 
begin
if(@pIntent='None')
begin
 set @pBotResponse ='Sorry your request cannot be found';
end

else
begin
select @pBotResponse=BotResponse from rajesh_DynamicQuestions where Intent=@pIntent and Entity=@pEntity;
end

end
--------------------------------------------------------------------------------

create table rajesh_OrderErrorlog
           (
		   Id int identity(1,1) primary key,
		   ErrorMessage nvarchar(4000),
		   ErrorTime Datetime
		   )
select * from rajesh_OrderErrorlog		   


Create or alter procedure OrderErrorlogproc
              (
			  @ErrorMessage nvarchar(4000),
			  @ErrorTime datetime
			  )
as	
begin
insert into rajesh_OrderErrorlog(
                         ErrorMessage,
						 ErrorTime
						 ) 
						 values
						 (
						 @ErrorMessage,
						 @ErrorTime
						 )
end

-------------------------------------------------------------------------------------------
create table rajesh_ConversationLog
           (
		   Id int identity(1,1) primary key,
		   UserName nvarchar(1000),
		   UserRequest nvarchar(4000),
		   BotResponse nvarchar(4000),
		   LoggingTime Datetime,
		   Channel nvarchar(100)
		   )
select * from rajesh_ConversationLog
---ConversationLog Procedure
 create or alter procedure Conversationlogg
         (
		  @UserName nvarchar(1000),
		   @UserRequest nvarchar(4000),
		  @BotResponse nvarchar(4000),
		  @LoggingTime datetime,
		  @Channel nvarchar(100)
		  )
            as
            begin
             insert into rajesh_ConversationLog
			 (
			 UserName,
			 UserRequest,
			 BotResponse,
			 LoggingTime,
			 Channel
			 )
			  values
			  (
			  @UserName,
			  @UserRequest,
			  @BotResponse,
			  @LoggingTime,
			  @Channel
			  )
end

---------------------------------------------Generated Ticket Info Storing--------------
create table rajesh_ItTicketsGenerated(
 Id int identity(1,1) primary key,
 UserName nvarchar(1000),
 IssueCategory nvarchar(4000),
 IssueFacingWith nvarchar(4000),
 Issuedescription nvarchar(4000),
 GeneratingTime Datetime,
 Channel nvarchar(100)

)


select * from rajesh_ItTicketsGenerated
                                        


create or alter procedure ItTicketsGeneratedLog
         (
		  @UserName nvarchar(1000),
		  @IssueCategory nvarchar(4000),
		  @IssueFacingWith nvarchar(4000),
		  @IssueDescription nvarchar(4000),
		  @GeneratingTime datetime,
		  @Channel nvarchar(100)
		  )
            as
            begin
             insert into rajesh_ItTicketsGenerated
			 (
			 UserName,
			 IssueCategory,
			 IssueFacingWith,
			 IssueDescription,
			 GeneratingTime,
			 Channel
			 )
			  values
			  (
			  @UserName,
			  @IssueCategory,
			  @IssueFacingWith,
			  @IssueDescription,
			  @GeneratingTime,
			  @Channel
			  )
end

------------------------------------------------------------------------

create or alter procedure TicketsGeneratedCount
(@pUsername nvarchar(100))
as 
begin
select top 5 IssueCategory,IssueFacingWith,IssueDescription,GeneratingTime from  rajesh_ItTicketsGenerated where UserName=@pUsername order by id desc
end

exec TicketsGeneratedCount 'User'


---------------------------------------------------


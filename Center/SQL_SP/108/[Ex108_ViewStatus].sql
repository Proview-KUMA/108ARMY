-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
use Center
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<琩高挪代篈穝糤璉腹陪ボ>
-- =============================================
CREATE PROCEDURE [Ex108_ViewStatus] 
	-- Add the parameters for the stored procedure here
	(
		@type nvarchar(10),
		@value nvarchar(10)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if @type = '102,202'
	begin
		select sid,id,clothesNum,name, age, birth, (case when gender = 'M' then '╧' when gender = 'F' then '' end) as gender, 
		unit_code = (select u.unit_title from unit u where u.unit_code = r.unit_code ), 
		
	(case when substring(memo,1,1) in ('0') then (case when sit_ups is null then 'ゼ代' else CONVERT(nvarchar(10),sit_ups)end)
when substring(memo,1,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is null and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼ代' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),sit_ups/60)+':'+CONVERT(nvarchar(10),sit_ups%60) end)	
			  when substring(memo,1,1) not in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then CONVERT(nvarchar(10),sit_ups) when sit_ups is null then 'ゼ代' else '' end) end) as sit_ups, 
		(case when substring(memo,1,1) in ('F','G') then (case when (sit_ups is null or sit_ups is not null) and sit_ups_score = 9999 then '-' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then '-' when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end) 
		      when substring(memo,1,1) not in ('F','G') then (case when sit_ups_score = 999 then '-' when sit_ups_score = 9999 then '-'  when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end)end) as sit_ups_score,	 
	
		
	(case when substring(memo,2,1) in ('0') then (case when push_ups is null then 'ゼ代' else CONVERT(nvarchar(10),push_ups)end)
when substring(memo,2,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,2,1))+ (case when push_ups is null and (push_ups_score = 9999 or push_ups_score is null) then 'ゼ代' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),push_ups/60)+':'+CONVERT(nvarchar(10),push_ups%60) end)	
			  when substring(memo,2,1) not in ('F','G')  then (select rep_title + ':' from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then CONVERT(nvarchar(10),push_ups) when push_ups is null then 'ゼ代' else '' end) end) as push_ups, 
		(case when substring(memo,2,1) in ('F','G') then (case when (push_ups is null or push_ups is not null) and push_ups_score = 9999 then '-' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then '-' when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end) 
		      when substring(memo,2,1) not in ('F','G')then (case when push_ups_score = 999 then '-' when push_ups_score = 9999 then '-'  when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end)end) as push_ups_score,	
		
		
		(case when substring(memo,3,1) in ('0') then (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
	when substring(memo,3,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) +  (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)	
			  when substring(memo,3,1) not in ('0','F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) + (case when run is not null then CONVERT(nvarchar(10),run) when run is null then 'ゼ代' else '' end) end) as run, 
		(case when substring(memo,3,1) in ('0','F','G') then (case when (run is null or run is not null) and run_score = 9999 then '-' when run = 9999 and (run_score = 9999 or run_score is null) then '-' when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end) 
		      when substring(memo,3,1) not in ('0','F','G') then (case when run_score = 999 then '-' when run_score = 9999 then '-'  when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end)end) as run_score,
		CONVERT(NVARCHAR(3),CONVERT(NVARCHAR(4),r.date,20) - 1911) + '/' + SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),6,2) + '/' +
        SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),9,2) AS date,(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(select meaning from statuscode s where s.code = r.status ) as status, memo
		from Result r where r.date = CONVERT( DateTime ,@value) and status in ('102','202') order by date desc,status
	end
	if @type = '103,203'
	begin
		select sid,id,clothesNum, name, age, birth, (case when gender = 'M' then '╧' when gender = 'F' then '' end) as gender, 
		unit_code = (select u.unit_title from unit u where u.unit_code = r.unit_code ), 
		
	(case when substring(memo,1,1) in ('0') then (case when sit_ups is null then 'ゼ代' else CONVERT(nvarchar(10),sit_ups)end)
when substring(memo,1,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is null and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼ代' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),sit_ups/60)+':'+CONVERT(nvarchar(10),sit_ups%60) end)	
			  when substring(memo,1,1) not in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then CONVERT(nvarchar(10),sit_ups) when sit_ups is null then 'ゼ代' else '' end) end) as sit_ups, 
		(case when substring(memo,1,1) in ('F','G') then (case when (sit_ups is null or sit_ups is not null) and sit_ups_score = 9999 then '-' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then '-' when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end) 
		      when substring(memo,1,1) not in ('F','G') then (case when sit_ups_score = 999 then '-' when sit_ups_score = 9999 then '-'  when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end)end) as sit_ups_score,	 
	
		
	(case when substring(memo,2,1) in ('0') then (case when push_ups is null then 'ゼ代' else CONVERT(nvarchar(10),push_ups)end)
when substring(memo,2,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,2,1))+ (case when push_ups is null and (push_ups_score = 9999 or push_ups_score is null) then 'ゼ代' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),push_ups/60)+':'+CONVERT(nvarchar(10),push_ups%60) end)	
			  when substring(memo,2,1) not in ('F','G')  then (select rep_title + ':' from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then CONVERT(nvarchar(10),push_ups) when push_ups is null then 'ゼ代' else '' end) end) as push_ups, 
		(case when substring(memo,2,1) in ('F','G') then (case when (push_ups is null or push_ups is not null) and push_ups_score = 9999 then '-' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then '-' when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end) 
		      when substring(memo,2,1) not in ('F','G')then (case when push_ups_score = 999 then '-' when push_ups_score = 9999 then '-'  when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end)end) as push_ups_score,	
		
		
		(case when substring(memo,3,1) in ('0') then (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
	when substring(memo,3,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) +  (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)	
			  when substring(memo,3,1) not in ('0','F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) + (case when run is not null then CONVERT(nvarchar(10),run) when run is null then 'ゼ代' else '' end) end) as run, 
		(case when substring(memo,3,1) in ('0','F','G') then (case when (run is null or run is not null) and run_score = 9999 then '-' when run = 9999 and (run_score = 9999 or run_score is null) then '-' when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end) 
		      when substring(memo,3,1) not in ('0','F','G') then (case when run_score = 999 then '-' when run_score = 9999 then '-'  when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end)end) as run_score,
		CONVERT(NVARCHAR(3),CONVERT(NVARCHAR(4),r.date,20) - 1911) + '/' + SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),6,2) + '/' +
        SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),9,2) AS date,(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(select meaning from statuscode s where s.code = r.status ) as status, memo
		from Result r where r.date = CONVERT( DateTime ,@value) and status in ('103','203') order by date desc,status
	end
	if @type = '104,204'
	begin
		select sid,id,clothesNum, name, age, birth, (case when gender = 'M' then '╧' when gender = 'F' then '' end) as gender, 
		unit_code = (select u.unit_title from unit u where u.unit_code = r.unit_code ), 
		
	(case when substring(memo,1,1) in ('0') then (case when sit_ups is null then 'ゼ代' else CONVERT(nvarchar(10),sit_ups)end)
when substring(memo,1,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is null and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼ代' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),sit_ups/60)+':'+CONVERT(nvarchar(10),sit_ups%60) end)	
			  when substring(memo,1,1) not in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then CONVERT(nvarchar(10),sit_ups) when sit_ups is null then 'ゼ代' else '' end) end) as sit_ups, 
		(case when substring(memo,1,1) in ('F','G') then (case when (sit_ups is null or sit_ups is not null) and sit_ups_score = 9999 then '-' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then '-' when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end) 
		      when substring(memo,1,1) not in ('F','G') then (case when sit_ups_score = 999 then '-' when sit_ups_score = 9999 then '-'  when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end)end) as sit_ups_score,	 
	
		
	(case when substring(memo,2,1) in ('0') then (case when push_ups is null then 'ゼ代' else CONVERT(nvarchar(10),push_ups)end)
when substring(memo,2,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,2,1))+ (case when push_ups is null and (push_ups_score = 9999 or push_ups_score is null) then 'ゼ代' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),push_ups/60)+':'+CONVERT(nvarchar(10),push_ups%60) end)	
			  when substring(memo,2,1) not in ('F','G')  then (select rep_title + ':' from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then CONVERT(nvarchar(10),push_ups) when push_ups is null then 'ゼ代' else '' end) end) as push_ups, 
		(case when substring(memo,2,1) in ('F','G') then (case when (push_ups is null or push_ups is not null) and push_ups_score = 9999 then '-' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then '-' when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end) 
		      when substring(memo,2,1) not in ('F','G')then (case when push_ups_score = 999 then '-' when push_ups_score = 9999 then '-'  when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end)end) as push_ups_score,	
		
		
		(case when substring(memo,3,1) in ('0') then (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
	when substring(memo,3,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) +  (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)	
			  when substring(memo,3,1) not in ('0','F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) + (case when run is not null then CONVERT(nvarchar(10),run) when run is null then 'ゼ代' else '' end) end) as run, 
		(case when substring(memo,3,1) in ('0','F','G') then (case when (run is null or run is not null) and run_score = 9999 then '-' when run = 9999 and (run_score = 9999 or run_score is null) then '-' when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end) 
		      when substring(memo,3,1) not in ('0','F','G') then (case when run_score = 999 then '-' when run_score = 9999 then '-'  when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end)end) as run_score,
		CONVERT(NVARCHAR(3),CONVERT(NVARCHAR(4),r.date,20) - 1911) + '/' + SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),6,2) + '/' +
        SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),9,2) AS date,(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(select meaning from statuscode s where s.code = r.status ) as status, memo
		from Result r where r.date = CONVERT( DateTime ,@value) and status in ('104','204') order by date desc,status
	end
	if @type = '105,205'
	begin
		select sid,id,clothesNum, name, age, birth, (case when gender = 'M' then '╧' when gender = 'F' then '' end) as gender, 
		unit_code = (select u.unit_title from unit u where u.unit_code = r.unit_code ), 
		
	(case when substring(memo,1,1) in ('0') then (case when sit_ups is null then 'ゼ代' else CONVERT(nvarchar(10),sit_ups)end)
when substring(memo,1,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is null and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼ代' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),sit_ups/60)+':'+CONVERT(nvarchar(10),sit_ups%60) end)	
			  when substring(memo,1,1) not in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then CONVERT(nvarchar(10),sit_ups) when sit_ups is null then 'ゼ代' else '' end) end) as sit_ups, 
		(case when substring(memo,1,1) in ('F','G') then (case when (sit_ups is null or sit_ups is not null) and sit_ups_score = 9999 then '-' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then '-' when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end) 
		      when substring(memo,1,1) not in ('F','G') then (case when sit_ups_score = 999 then '-' when sit_ups_score = 9999 then '-'  when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end)end) as sit_ups_score,	 
	
		
	(case when substring(memo,2,1) in ('0') then (case when push_ups is null then 'ゼ代' else CONVERT(nvarchar(10),push_ups)end)
when substring(memo,2,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,2,1))+ (case when push_ups is null and (push_ups_score = 9999 or push_ups_score is null) then 'ゼ代' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),push_ups/60)+':'+CONVERT(nvarchar(10),push_ups%60) end)	
			  when substring(memo,2,1) not in ('F','G')  then (select rep_title + ':' from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then CONVERT(nvarchar(10),push_ups) when push_ups is null then 'ゼ代' else '' end) end) as push_ups, 
		(case when substring(memo,2,1) in ('F','G') then (case when (push_ups is null or push_ups is not null) and push_ups_score = 9999 then '-' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then '-' when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end) 
		      when substring(memo,2,1) not in ('F','G')then (case when push_ups_score = 999 then '-' when push_ups_score = 9999 then '-'  when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end)end) as push_ups_score,	
		
		
		(case when substring(memo,3,1) in ('0') then (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
	when substring(memo,3,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) +  (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)	
			  when substring(memo,3,1) not in ('0','F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) + (case when run is not null then CONVERT(nvarchar(10),run) when run is null then 'ゼ代' else '' end) end) as run, 
		(case when substring(memo,3,1) in ('0','F','G') then (case when (run is null or run is not null) and run_score = 9999 then '-' when run = 9999 and (run_score = 9999 or run_score is null) then '-' when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end) 
		      when substring(memo,3,1) not in ('0','F','G') then (case when run_score = 999 then '-' when run_score = 9999 then '-'  when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end)end) as run_score,
		CONVERT(NVARCHAR(3),CONVERT(NVARCHAR(4),r.date,20) - 1911) + '/' + SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),6,2) + '/' +
        SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),9,2) AS date,(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(select meaning from statuscode s where s.code = r.status ) as status, memo
		from Result r where r.date = CONVERT( DateTime ,@value) and status in ('105','205') order by date desc,status
	end
	if @type = '999'
	begin
		select sid,id,clothesNum, name, age, birth, (case when gender = 'M' then '╧' when gender = 'F' then '' end) as gender, 
		unit_code = (select u.unit_title from unit u where u.unit_code = r.unit_code ), 
		
	(case when substring(memo,1,1) in ('0') then (case when sit_ups is null then 'ゼ代' else CONVERT(nvarchar(10),sit_ups)end)
when substring(memo,1,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is null and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼ代' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),sit_ups/60)+':'+CONVERT(nvarchar(10),sit_ups%60) end)	
			  when substring(memo,1,1) not in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then CONVERT(nvarchar(10),sit_ups) when sit_ups is null then 'ゼ代' else '' end) end) as sit_ups, 
		(case when substring(memo,1,1) in ('F','G') then (case when (sit_ups is null or sit_ups is not null) and sit_ups_score = 9999 then '-' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then '-' when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end) 
		      when substring(memo,1,1) not in ('F','G') then (case when sit_ups_score = 999 then '-' when sit_ups_score = 9999 then '-'  when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end)end) as sit_ups_score,	 
	
		
	(case when substring(memo,2,1) in ('0') then (case when push_ups is null then 'ゼ代' else CONVERT(nvarchar(10),push_ups)end)
when substring(memo,2,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,2,1))+ (case when push_ups is null and (push_ups_score = 9999 or push_ups_score is null) then 'ゼ代' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),push_ups/60)+':'+CONVERT(nvarchar(10),push_ups%60) end)	
			  when substring(memo,2,1) not in ('F','G')  then (select rep_title + ':' from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then CONVERT(nvarchar(10),push_ups) when push_ups is null then 'ゼ代' else '' end) end) as push_ups, 
		(case when substring(memo,2,1) in ('F','G') then (case when (push_ups is null or push_ups is not null) and push_ups_score = 9999 then '-' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then '-' when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end) 
		      when substring(memo,2,1) not in ('F','G')then (case when push_ups_score = 999 then '-' when push_ups_score = 9999 then '-'  when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end)end) as push_ups_score,	
		
		
		(case when substring(memo,3,1) in ('0') then (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
	when substring(memo,3,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) +  (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)	
			  when substring(memo,3,1) not in ('0','F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) + (case when run is not null then CONVERT(nvarchar(10),run) when run is null then 'ゼ代' else '' end) end) as run, 
		(case when substring(memo,3,1) in ('0','F','G') then (case when (run is null or run is not null) and run_score = 9999 then '-' when run = 9999 and (run_score = 9999 or run_score is null) then '-' when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end) 
		      when substring(memo,3,1) not in ('0','F','G') then (case when run_score = 999 then '-' when run_score = 9999 then '-'  when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end)end) as run_score,
		CONVERT(NVARCHAR(3),CONVERT(NVARCHAR(4),r.date,20) - 1911) + '/' + SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),6,2) + '/' +
        SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),9,2) AS date,(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(select meaning from statuscode s where s.code = r.status ) as status, memo
		from Result r where r.date = CONVERT( DateTime ,@value) and status = '999' order by date desc,status
	end
	if @type = '001'
	begin
		select sid,id,clothesNum, name, age, birth, (case when gender = 'M' then '╧' when gender = 'F' then '' end) as gender, 
		unit_code = (select u.unit_title from unit u where u.unit_code = r.unit_code ), 
		
	(case when substring(memo,1,1) in ('0') then (case when sit_ups is null then 'ゼ代' else CONVERT(nvarchar(10),sit_ups)end)
when substring(memo,1,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is null and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼ代' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),sit_ups/60)+':'+CONVERT(nvarchar(10),sit_ups%60) end)	
			  when substring(memo,1,1) not in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,1,1)) + (case when sit_ups is not null then CONVERT(nvarchar(10),sit_ups) when sit_ups is null then 'ゼ代' else '' end) end) as sit_ups, 
		(case when substring(memo,1,1) in ('F','G') then (case when (sit_ups is null or sit_ups is not null) and sit_ups_score = 9999 then '-' when sit_ups = 9999 and (sit_ups_score = 9999 or sit_ups_score is null) then '-' when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end) 
		      when substring(memo,1,1) not in ('F','G') then (case when sit_ups_score = 999 then '-' when sit_ups_score = 9999 then '-'  when sit_ups_score is null then '-' else CONVERT(nvarchar(10), sit_ups_score)end)end) as sit_ups_score,	 
	
		
	(case when substring(memo,2,1) in ('0') then (case when push_ups is null then 'ゼ代' else CONVERT(nvarchar(10),push_ups)end)
when substring(memo,2,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,2,1))+ (case when push_ups is null and (push_ups_score = 9999 or push_ups_score is null) then 'ゼ代' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),push_ups/60)+':'+CONVERT(nvarchar(10),push_ups%60) end)	
			  when substring(memo,2,1) not in ('F','G')  then (select rep_title + ':' from repment where sid = substring(memo,2,1)) + (case when push_ups is not null then CONVERT(nvarchar(10),push_ups) when push_ups is null then 'ゼ代' else '' end) end) as push_ups, 
		(case when substring(memo,2,1) in ('F','G') then (case when (push_ups is null or push_ups is not null) and push_ups_score = 9999 then '-' when push_ups = 9999 and (push_ups_score = 9999 or push_ups_score is null) then '-' when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end) 
		      when substring(memo,2,1) not in ('F','G')then (case when push_ups_score = 999 then '-' when push_ups_score = 9999 then '-'  when push_ups_score is null then '-' else CONVERT(nvarchar(10), push_ups_score)end)end) as push_ups_score,	
		
		
		(case when substring(memo,3,1) in ('0') then (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)
	when substring(memo,3,1) in ('F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) +  (case when run is null and (run_score = 9999 or run_score is null) then 'ゼ代' when run = 9999 and (run_score = 9999 or run_score is null) then 'ゼЧ代' else CONVERT(nvarchar(10),run/60)+':'+CONVERT(nvarchar(10),run%60) end)	
			  when substring(memo,3,1) not in ('0','F','G') then (select rep_title + ':' from repment where sid = substring(memo,3,1)) + (case when run is not null then CONVERT(nvarchar(10),run) when run is null then 'ゼ代' else '' end) end) as run, 
		(case when substring(memo,3,1) in ('0','F','G') then (case when (run is null or run is not null) and run_score = 9999 then '-' when run = 9999 and (run_score = 9999 or run_score is null) then '-' when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end) 
		      when substring(memo,3,1) not in ('0','F','G') then (case when run_score = 999 then '-' when run_score = 9999 then '-'  when run_score is null then '-' else CONVERT(nvarchar(10), run_score)end)end) as run_score,
		CONVERT(NVARCHAR(3),CONVERT(NVARCHAR(4),r.date,20) - 1911) + '/' + SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),6,2) + '/' +
        SUBSTRING(CONVERT(NVARCHAR(10),r.date,20),9,2) AS date,(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(select meaning from statuscode s where s.code = r.status ) as status, memo
		from Result r where r.date = CONVERT( DateTime ,@value) and status = '001' order by date desc,status
	end
	
	
END
GO

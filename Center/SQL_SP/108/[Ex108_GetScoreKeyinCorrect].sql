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
USE Center
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<���o�H�uŲ�����W�Ǧ��Z�ɥ���>
-- =============================================
CREATE PROCEDURE Ex108_GetScoreKeyinCorrect 
	-- Add the parameters for the stored procedure here
	(
	@id varchar(10)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select name,date,memo,sit_ups,push_ups,run,
		(case when substring(memo,1,1) = '0' then (case when sit_ups is null then '���װ_��' else '���װ_��' end)
			  when substring(memo,1,1) != '0'then (select rep_title from repment where sid = substring(memo,1,1)) end) as sit_ups_name , 
		(case when substring(memo,2,1) = '0' then (case when push_ups is null then '���a����' else '���a����' end)
			  when substring(memo,2,1) != '0'then (select rep_title from repment where sid = substring(memo,2,1)) end) as push_ups_name, 
		(case when substring(memo,3,1) = '0' then (case when run is null then '�T�d����' else '�T�d����' end)
			  when substring(memo,3,1) != '0'then (select rep_title from repment where sid = substring(memo,3,1)) end) as run_name,
--�s�W�P�_�O���٬O��		  
	(case when substring(memo,1,1) = '0' then (case when sit_ups is null then '��' else '��' end)
	when substring(memo,1,1) != '0'then (select note from repment where sid = substring(memo,1,1)) end) as sit_ups_note,
(case when substring(memo,2,1) = '0' then (case when push_ups is null then '��' else '��' end)
			  when substring(memo,2,1) != '0'then (select note from repment where sid = substring(memo,2,1)) end) as push_ups_note, 
		(case when substring(memo,3,1) = '0' then (case when run is null then '��' else '��' end)
			  when substring(memo,3,1) != '0'then (select note from repment where sid = substring(memo,3,1)) end) as run_note		  
	from Result where id = @id and result='111' and status in ('102','103','105')
END
GO

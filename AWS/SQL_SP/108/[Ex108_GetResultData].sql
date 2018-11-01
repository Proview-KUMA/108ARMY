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
USE Main
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Ex108_GetResultData
	-- Add the parameters for the stored procedure here
	(
		@id nvarchar(10)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select sid,id, name, age, CONVERT(nvarchar(10),r.birth,111) as 'birth', (case when gender = 'M' then '男' when gender = 'F' then '女' end) as gender, 
		dbo.F_GetItemScore('1c',substring(memo,1,1),sit_ups) as sit_ups , 
		dbo.F_GetItemScore('1s',substring(memo,1,1),sit_ups_score) as sit_ups_score, 
		dbo.F_GetItemScore('2c',substring(memo,2,1),push_ups) as push_ups, 
		dbo.F_GetItemScore('2s',substring(memo,2,1),push_ups_score) as push_ups_score,
		dbo.F_GetItemScore('3c',substring(memo,3,1),run) as run,
		dbo.F_GetItemScore('3s',substring(memo,3,1),run_score) as run_score,
		CONVERT(nvarchar(10),r.date,111) as 'date',(select c.center_name from Center c where c.center_code = r.center_code ) as center_name,
		(case when result = '222' then (select meaning from statuscode s where s.code = r.status ) + '(人工)'  else  (select meaning from statuscode s where s.code = r.status ) end) as status, memo
		from Result r where r.id = @id and r.status in('202','203')  order by date desc 
END
GO

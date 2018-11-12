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
-- Description:	<取得成績異動記錄>
-- =============================================
CREATE PROCEDURE Ex108_GetResultCorrectLog 
	-- Add the parameters for the stored procedure here
	(
	@date nvarchar(12)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select id,name,CONVERT(varchar,date,111)as 'date',old_sit_ups,old_push_ups,old_run,new_sit_ups,new_push_ups,new_run,
account,account_id,(CONVERT(varchar,update_time,111)+'   '+CONVERT(varchar,update_time,108))as 'update_time' from ResultCorrectLog_108 where date=@date order by id
END
GO

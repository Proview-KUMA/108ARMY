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
-- Description:	<將成績補正寫入Log>
-- =============================================
CREATE PROCEDURE Ex108_AddResultCorrectLog
	-- Add the parameters for the stored procedure here
	  (
	@id varchar(10),
	@name nvarchar(10),
	@date datetime,
	@old_sit_ups nvarchar(4),
	@old_push_ups nvarchar(4),
	@old_run nvarchar(4),
	@new_sit_ups nvarchar(4),
	@new_push_ups nvarchar(4),
	@new_run nvarchar(4),
	@account nvarchar(20),
	@account_id varchar(10),
	@update_time datetime
      )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO ResultCorrectLog_108 
	(id,name,date,old_sit_ups,old_push_ups,old_run,new_sit_ups,new_push_ups,new_run,account,account_id,update_time)
	values(@id,@name,@date,@old_sit_ups,@old_push_ups,@old_run,@new_sit_ups,@new_push_ups,@new_run,@account,@account_id,@update_time)
END
GO

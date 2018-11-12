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
-- Description:	查詢完成檢錄但未上傳的人員資料，更新生日、姓名用
-- =============================================
CREATE PROCEDURE Ex108_GetUpdatePlayer 
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
	select id,name,birth,age,status,(select meaning from StatusCode where StatusCode.code=Result.status)as 'status_name',date from Result
    where id=@id and status in('001','102','103','104','105','106') order by date desc
END
GO

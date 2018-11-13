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
-- Description:	<更新已檢錄但未上傳之個人姓名、生日、年齡資料>
-- =============================================
CREATE PROCEDURE Ex108_UpdatePlayerData
	-- Add the parameters for the stored procedure here
	(
	@id varchar(10),
	@date nvarchar(12),
	@name nvarchar(10),
	@birth nvarchar(12),
	@age nvarchar(3)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Result SET name=@name,birth=@birth,age=@age where id=@id and date=@date and status in('001','102','103','104','105','106')
END
GO

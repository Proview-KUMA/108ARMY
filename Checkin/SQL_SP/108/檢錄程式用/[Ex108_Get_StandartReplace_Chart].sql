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
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Ex108_Get_StandartReplace_Chart
	(
	@year varchar(4),
	@item varchar(1)--��0�N��򥻤T���B��1�N���δ��N
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    --�򥻤T��
    if(@item='0')
    begin
    select MONTH(date) as 'month',COUNT(date) as 'count' from Result
    where YEAR(date)=@year and LF_Tag_ID is not null and memo='000' group by MONTH(date) order by 'month'
    end
    --�ϥδ��N/�h���ﶵ
    else if(@item='1')
    begin
    select MONTH(date) as 'month',COUNT(date) as 'count' from Result
    where YEAR(date)=@year and LF_Tag_ID is not null and memo!='000' group by MONTH(date) order by 'month'
    end	
    
END
GO

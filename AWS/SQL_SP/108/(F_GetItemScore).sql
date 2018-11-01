-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
use Main
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
alter FUNCTION F_GetItemScore 
(
	@type varchar(2),--肚兜Ω
	@memo varchar(1),--肚兜ヘ
	@value int--Θ罿
)
RETURNS nvarchar(20)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @itemscore nvarchar(20)

	-- Add the T-SQL statements to compute the return value here
	--兜Ω1Ω/ 
	if(@type='1c')
	begin
	set @itemscore = (
	case when @memo = '0' 
	then (case when @value is null then 'ゼ代' else CONVERT(nvarchar(10),@value)end)
	when @memo != '0' 
	then ((select rep_title + ':' from repment where sid = @memo) + (case when @value is not null then CONVERT(nvarchar(10),@value) when @value is null then 'ゼ代' else '' end))end)
	end
	--兜Ω1だ计
	if(@type='1s')
	begin
	set @itemscore = (
	case when @memo = '0' then (case when @value = 999 then '-' when @value is null then '-' else CONVERT(nvarchar(10), @value)end) 
	when @memo != '0' then (case when @value = 999 then '-' when @value = 9999 then '-' when @value = 100 then '' when @value = 0 then 'ぃ' when @value is null then '-' else CONVERT(nvarchar(10), @value)end)end)
	end
	--兜Ω2Ω/
	if(@type='2c')
	begin
	set @itemscore = (
	case when @memo = '0' then (case when @value is null then 'ゼ代' else CONVERT(nvarchar(10),@value)end)
	when @memo != '0' then ((select rep_title + ':' from repment where sid = @memo) + (case when @value is not null then CONVERT(nvarchar(10),@value) when @value is null then 'ゼ代' else '' end))end)
	end
	--兜Ω2だ计
	if(@type='2s')
	begin
	set @itemscore = (
	case when @memo = '0' then (case when @value = 999 then '-' when @value is null then '-' else CONVERT(nvarchar(10), @value)end) 
	when @memo != '0'then (case when @value = 999 then '-' when @value = 9999 then '-' when @value = 100 then '' when @value = 0 then 'ぃ' when @value is null then '-' else CONVERT(nvarchar(10), @value)end)end)
	end
	--兜Ω3Ω/
	if(@type='3c')
	begin
	set @itemscore = (
	case when @memo = '0' then (case when @value is null then 'ゼ代' else CONVERT(nvarchar(10),@value)end)
	when @memo != '0' then ((select rep_title + ':' from repment where sid = @memo) + (case when @value is not null then CONVERT(nvarchar(10),@value) when @value is null then 'ゼ代' else '' end))end)
	end
	--兜Ω3だ计
	if(@type='3s')
	begin
	set @itemscore = (
	case when @memo = '0' then (case when @value = 9999 then '-' when @value is null then '-' else CONVERT(nvarchar(10), @value)end) 
	when @memo != '0' then (case when @value = 999 then '-' when @value = 9999 then '-' when @value = 100 then '' when @value = 0 then 'ぃ' when @value is null then '-' else CONVERT(nvarchar(10), @value) end)end)
	end
	
	-- Return the result of the function
	RETURN @itemscore

END
GO


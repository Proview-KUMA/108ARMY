USE [Center]
GO
/****** Object:  StoredProcedure [dbo].[GetRepMentNonSwin]    Script Date: 11/12/2018 17:07:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetRepMentNonSwin]
	(
		@Gender nvarchar(3)
	)
AS
	 SET NOCOUNT ON 
	select rep_title,sid from RepMent where IsSwinItem = '0' and IsService = '1'  and Gender in (@Gender, 'ALL')
	
	/*****  Old Version Update by Jiaming , Date: 2012-01-04  *****/
	--ALTER PROCEDURE [dbo].[GetRepMentNonSwin]
	--(
	--	IsSwinItem bit
	--)
	--AS
	-- SET NOCOUNT ON 
	-- select rep_title from RepMent where IsSwinItem = @IsSwinItem
	
	
USE [Center]
GO
/****** Object:  StoredProcedure [dbo].[GetRepMent]    Script Date: 11/12/2018 17:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetRepMent]
	(
		@Gender nvarchar(3)
	)
AS
	 SET NOCOUNT ON 
	 select rep_title, sid from RepMent where IsService = '1'  and Gender in (@Gender, 'ALL')


/*****  Old Version Update by Jiaming , Date: 2012-01-04  *****/
--ALTER PROCEDURE [dbo].[GetRepMent]

--AS
--	 SET NOCOUNT ON 
--     select rep_title from RepMent 

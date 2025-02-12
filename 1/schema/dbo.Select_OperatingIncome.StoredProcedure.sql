USE [Yushan]
GO
/****** Object:  StoredProcedure [dbo].[Select_OperatingIncome]    Script Date: 2024/05/27 22:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_OperatingIncome]
	@Name nvarchar(20) null
AS
BEGIN
	IF (@Name='ALL')
	BEGIN
		SELECT P.[Date]
			,P.[YM]
			,C.[Code]Company_Code
			,C.[Name]Company_Name
			,I.[Name] Industry
			,[OI_TM]
			,[OI_LM]
			,[OI_TMLY]
			,[OI_LM_ID]
			,[OI_TMLY_ID]
			,[Diff_TM]
			,[Diff_LY]
			,[Diff_PC]
			,ISNULL([Remark],'-')Remark
		FROM [Yushan].[dbo].[OperatingIncome] O
		INNER JOIN [Yushan].[dbo].[Publication] P ON(O.[Publication]=P.[Idx])
		INNER JOIN [Yushan].[dbo].[Company] C ON(O.[Company]=C.[Code])
		INNER JOIN [Yushan].[dbo].[Industry] I ON(C.[Industry]=I.[ID])
		ORDER BY P.[Date],P.[YM],C.[Code];
	END
	ELSE
	BEGIN
		SELECT P.[Date]
			,P.[YM]
			,C.[Code]Company_Code
			,C.[Name]Company_Name
			,I.[Name] Industry
			,[OI_TM]
			,[OI_LM]
			,[OI_TMLY]
			,[OI_LM_ID]
			,[OI_TMLY_ID]
			,[Diff_TM]
			,[Diff_LY]
			,[Diff_PC]
			,ISNULL([Remark],'-')Remark
		FROM [Yushan].[dbo].[OperatingIncome] O
		INNER JOIN [Yushan].[dbo].[Publication] P ON(O.[Publication]=P.[Idx])
		INNER JOIN [Yushan].[dbo].[Company] C ON(O.[Company]=C.[Code] AND C.[Name] LIKE '%'+@Name+'%')
		INNER JOIN [Yushan].[dbo].[Industry] I ON(C.[Industry]=I.[ID])
		ORDER BY P.[Date],P.[YM],C.[Code];
	END
END
GO

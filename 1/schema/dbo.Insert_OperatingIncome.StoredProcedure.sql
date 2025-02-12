USE [Yushan]
GO
/****** Object:  StoredProcedure [dbo].[Insert_OperatingIncome]    Script Date: 2024/05/27 22:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insert_OperatingIncome]
	@Date char(7),
	@YM char(5) ,
	@C_Code varchar(10),
	@C_Name nvarchar(20),
	@I_Name varchar(20),
	@OI_TM int NULL,
	@OI_LM int NULL,
	@OI_TMLY int  NULL,
	@OI_LM_ID float NULL,
	@OI_TMLY_ID float NULL,
	@Diff_TM int NULL,
	@Diff_LY int NULL,
	@Diff_PC float NULL,
	@Remark nvarchar(128) NULL
AS
BEGIN
	DECLARE @IsExist bit=0;
	DECLARE @Publication smallint;
	IF EXISTS(SELECT 1 FROM [Yushan].[dbo].[Publication] WHERE [Date]=@Date AND [YM]=@YM)
	BEGIN
		SELECT @Publication=[Idx] FROM [Yushan].[dbo].[Publication] WHERE [Date]=@Date AND [YM]=@YM;
	END
	ELSE
	BEGIN
		INSERT INTO [Yushan].[dbo].[Publication]([Date],[YM]) VALUES(@Date,@YM);
		SELECT @Publication=[Idx] FROM [Yushan].[dbo].[Publication] WHERE [Date]=@Date AND [YM]=@YM;
	END
	DECLARE @Industry tinyint
	IF EXISTS(SELECT 1 FROM [Yushan].[dbo].[Industry] WHERE [Name]=@I_Name)
	BEGIN
		SELECT @Industry=[ID] FROM [Yushan].[dbo].[Industry] WHERE [Name]=@I_Name;
	END
	ELSE
	BEGIN
		INSERT INTO [Yushan].[dbo].[Industry]([Name]) VALUES(@I_Name);
		SELECT @Industry=[ID] FROM [Yushan].[dbo].[Industry] WHERE [Name]=@I_Name;
	END
	IF  EXISTS(SELECT 1 FROM [Yushan].[dbo].[Company] WHERE [Name]=@C_Name)
	BEGIN
		IF  EXISTS(SELECT 1 FROM [Yushan].[dbo].[Company] WHERE [Name]=@C_Name AND [Code]=@C_Code AND [Industry]=@Industry)
			SET @IsExist=1;
	END
	ELSE
	BEGIN
		IF  NOT EXISTS(SELECT 1 FROM [Yushan].[dbo].[Company] WHERE [Code]=@C_Code)
		BEGIN
			INSERT INTO [Yushan].[dbo].[Company]([Code],[Name],[Industry])VALUES(@C_Code,@C_Name,@Industry);
		END
	END

	IF (@IsExist=1)
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM [Yushan].[dbo].[OperatingIncome] WHERE [Publication]=@Publication AND [Company]=@C_Code)
		BEGIN
			INSERT INTO [Yushan].[dbo].[OperatingIncome] VALUES(@Publication,@C_Code,@OI_TM,@OI_LM,@OI_TMLY,@OI_LM_ID,@OI_TMLY_ID,@Diff_TM,@Diff_LY,@Diff_PC,@Remark);
			SELECT '輸入成功!' MSG;
		END
		ELSE
		BEGIN
			SELECT '資料重複輸入!' MSG;
		END
	END
	ELSE
	BEGIN
		SELECT '資料有誤，請重新輸入!' MSG;
	END
END
GO

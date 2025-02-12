USE [Yushan]
GO
/****** Object:  Table [dbo].[OperatingIncome]    Script Date: 2024/05/27 22:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatingIncome](
	[Publication] [smallint] NOT NULL,
	[Company] [varchar](10) NOT NULL,
	[OI_TM] [int] NULL,
	[OI_LM] [int] NULL,
	[OI_TMLY] [int] NULL,
	[OI_LM_ID] [float] NULL,
	[OI_TMLY_ID] [float] NULL,
	[Diff_TM] [int] NULL,
	[Diff_LY] [int] NULL,
	[Diff_PC] [float] NULL,
	[Remark] [nvarchar](128) NULL,
 CONSTRAINT [PK_OperatingIncome] PRIMARY KEY CLUSTERED 
(
	[Publication] ASC,
	[Company] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OperatingIncome]  WITH CHECK ADD  CONSTRAINT [FK_OperatingIncome_Company] FOREIGN KEY([Company])
REFERENCES [dbo].[Company] ([Code])
GO
ALTER TABLE [dbo].[OperatingIncome] CHECK CONSTRAINT [FK_OperatingIncome_Company]
GO
ALTER TABLE [dbo].[OperatingIncome]  WITH CHECK ADD  CONSTRAINT [FK_OperatingIncome_Publication] FOREIGN KEY([Publication])
REFERENCES [dbo].[Publication] ([Idx])
GO
ALTER TABLE [dbo].[OperatingIncome] CHECK CONSTRAINT [FK_OperatingIncome_Publication]
GO

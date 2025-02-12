USE [Yushan]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2024/05/27 22:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Code] [varchar](10) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Industry] [tinyint] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Industry] FOREIGN KEY([Industry])
REFERENCES [dbo].[Industry] ([ID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Industry]
GO

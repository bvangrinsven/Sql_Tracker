ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_tblDatabaseSize_WeekNumReported]
GO

ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_tblDatabaseSize_YearReported]
GO

ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_tblDatabaseSize_MonthReported]
GO

ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_Table_1_DateCreated]
GO

ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_tblDatabaseSize_TotalDBSizeMB]
GO

ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_tblDatabaseSize_CurrentFileSizeMB]
GO

ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF_tblDatabaseSize_GUIDDatabaseSize]
GO

/****** Object:  Table [dbo].[tblDatabaseSize]    Script Date: 2022-03-06 3:49:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabaseSize]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabaseSize]
GO

/****** Object:  Table [dbo].[tblDatabaseSize]    Script Date: 2022-03-06 3:49:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblDatabaseSize](
	[UIDDatabaseSize] [int] IDENTITY(1,1) NOT NULL,
	[GUIDDatabaseSize] [varchar](36) NOT NULL,
	[DatabaseGUID] [varchar](36) NOT NULL,
	[DatabaseFileName] [varchar](200) NOT NULL,
	[CurrentFileSizeMB] [int] NOT NULL,
	[TotalDBSizeMB] [int] NOT NULL,
	[DateReported] [datetime] NOT NULL,
	[MonthReported] [int] NOT NULL,
	[YearReported] [int] NOT NULL,
	[WeekNumReported] [int] NOT NULL,
 CONSTRAINT [PK_tblDatabaseSize] PRIMARY KEY CLUSTERED 
(
	[GUIDDatabaseSize] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_tblDatabaseSize_GUIDDatabaseSize]  DEFAULT (newid()) FOR [GUIDDatabaseSize]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_tblDatabaseSize_CurrentFileSizeMB]  DEFAULT ((0)) FOR [CurrentFileSizeMB]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_tblDatabaseSize_TotalDBSizeMB]  DEFAULT ((0)) FOR [TotalDBSizeMB]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_Table_1_DateCreated]  DEFAULT (getdate()) FOR [DateReported]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_tblDatabaseSize_MonthReported]  DEFAULT ((0)) FOR [MonthReported]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_tblDatabaseSize_YearReported]  DEFAULT ((0)) FOR [YearReported]
GO

ALTER TABLE [dbo].[tblDatabaseSize] ADD  CONSTRAINT [DF_tblDatabaseSize_WeekNumReported]  DEFAULT ((0)) FOR [WeekNumReported]
GO



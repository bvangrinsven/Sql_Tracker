USE [CentralMonitoring]
GO
ALTER TABLE [dbo].[tblServers] DROP CONSTRAINT [DF__tblServer__DateM__2180FB33]
GO
ALTER TABLE [dbo].[tblServers] DROP CONSTRAINT [DF__tblServer__DateC__208CD6FA]
GO
ALTER TABLE [dbo].[tblServers] DROP CONSTRAINT [DF__tblServer__IsDel__1F98B2C1]
GO
ALTER TABLE [dbo].[tblServers] DROP CONSTRAINT [DF__tblServer__GUIDS__1EA48E88]
GO
ALTER TABLE [dbo].[tblServerJobStatus] DROP CONSTRAINT [DF_tblServerJobStatus_DateReported]
GO
ALTER TABLE [dbo].[tblServerJobStatus] DROP CONSTRAINT [DF_Table_1_GUIDNAVJobEntryStatus]
GO
ALTER TABLE [dbo].[tblServerJobs] DROP CONSTRAINT [DF__tblServer__DateM__4B7734FF]
GO
ALTER TABLE [dbo].[tblServerJobs] DROP CONSTRAINT [DF__tblServer__DateC__4A8310C6]
GO
ALTER TABLE [dbo].[tblServerJobs] DROP CONSTRAINT [DF__tblServer__IsDel__498EEC8D]
GO
ALTER TABLE [dbo].[tblServerJobs] DROP CONSTRAINT [DF__tblServer__GUIDS__489AC854]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] DROP CONSTRAINT [DF_tblServerDiskSizes_WeekNumReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] DROP CONSTRAINT [DF_tblServerDiskSizes_YearReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] DROP CONSTRAINT [DF_tblServerDiskSizes_MonthReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] DROP CONSTRAINT [DF_tblServerDiskSizes_DateReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] DROP CONSTRAINT [DF_tblServerDiskSizes_TotalSizeInMB]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] DROP CONSTRAINT [DF_tblServerDiskSizes_GUIDServerDiskSize]
GO
ALTER TABLE [dbo].[tblServerDisk] DROP CONSTRAINT [DF__tblServer__DateM__793DFFAF]
GO
ALTER TABLE [dbo].[tblServerDisk] DROP CONSTRAINT [DF__tblServer__DateC__7849DB76]
GO
ALTER TABLE [dbo].[tblServerDisk] DROP CONSTRAINT [DF__tblServer__IsDel__7755B73D]
GO
ALTER TABLE [dbo].[tblServerDisk] DROP CONSTRAINT [DF__tblServer__GUIDS__76619304]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__DateM__1DB06A4F]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__DateC__1CBC4616]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__Table__1BC821DD]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__Datab__1AD3FDA4]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__Serve__19DFD96B]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__Start__18EBB532]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__RunID__17F790F9]
GO
ALTER TABLE [dbo].[tblProcessLogs] DROP CONSTRAINT [DF__tblProces__GUIDP__17036CC0]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] DROP CONSTRAINT [DF_tblNAVJobQueueLog_WeekNumReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] DROP CONSTRAINT [DF_tblNAVJobQueueLog_YearReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] DROP CONSTRAINT [DF_tblNAVJobQueueLog_MonthReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] DROP CONSTRAINT [DF_tblNAVJobQueueLog_DateReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] DROP CONSTRAINT [DF_tblNAVJobQueueLog_GUIDNAVJobQueueLog]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] DROP CONSTRAINT [DF__tblNAVJob__DateM__40F9A68C]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] DROP CONSTRAINT [DF__tblNAVJob__DateC__40058253]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] DROP CONSTRAINT [DF__tblNAVJob__IsDel__3F115E1A]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] DROP CONSTRAINT [DF__tblNAVJob__GUIDN__3E1D39E1]
GO
ALTER TABLE [dbo].[tblNAVInstance] DROP CONSTRAINT [DF__tblNAVIns__DateM__73852659]
GO
ALTER TABLE [dbo].[tblNAVInstance] DROP CONSTRAINT [DF__tblNAVIns__DateC__72910220]
GO
ALTER TABLE [dbo].[tblNAVInstance] DROP CONSTRAINT [DF__tblNAVIns__IsDel__719CDDE7]
GO
ALTER TABLE [dbo].[tblNAVInstance] DROP CONSTRAINT [DF__tblNAVIns__GUIDN__70A8B9AE]
GO
ALTER TABLE [dbo].[tblNAVCompany] DROP CONSTRAINT [DF__tblNAVCom__DateM__3A4CA8FD]
GO
ALTER TABLE [dbo].[tblNAVCompany] DROP CONSTRAINT [DF__tblNAVCom__DateC__395884C4]
GO
ALTER TABLE [dbo].[tblNAVCompany] DROP CONSTRAINT [DF__tblNAVCom__IsDel__3864608B]
GO
ALTER TABLE [dbo].[tblNAVCompany] DROP CONSTRAINT [DF__tblNAVCom__GUIDN__37703C52]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__WeekN__160F4887]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__YearR__151B244E]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__Month__14270015]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__DateR__1332DBDC]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__Unuse__123EB7A3]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__UsedS__114A936A]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__Total__10566F31]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__RowsC__0F624AF8]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] DROP CONSTRAINT [DF__tblDataba__GUIDD__0E6E26BF]
GO
ALTER TABLE [dbo].[tblDatabaseTables] DROP CONSTRAINT [DF__tblDataba__DateM__6BE40491]
GO
ALTER TABLE [dbo].[tblDatabaseTables] DROP CONSTRAINT [DF__tblDataba__DateC__6AEFE058]
GO
ALTER TABLE [dbo].[tblDatabaseTables] DROP CONSTRAINT [DF__tblDataba__IsDel__69FBBC1F]
GO
ALTER TABLE [dbo].[tblDatabaseTables] DROP CONSTRAINT [DF__tblDataba__GUIDD__690797E6]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__WeekN__09A971A2]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__YearR__08B54D69]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__Month__07C12930]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__DateR__06CD04F7]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__Total__05D8E0BE]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__Curre__04E4BC85]
GO
ALTER TABLE [dbo].[tblDatabaseSize] DROP CONSTRAINT [DF__tblDataba__GUIDD__03F0984C]
GO
ALTER TABLE [dbo].[tblDatabases] DROP CONSTRAINT [DF__tblDataba__DateM__5CA1C101]
GO
ALTER TABLE [dbo].[tblDatabases] DROP CONSTRAINT [DF__tblDataba__DateC__5BAD9CC8]
GO
ALTER TABLE [dbo].[tblDatabases] DROP CONSTRAINT [DF__tblDataba__IsDel__5AB9788F]
GO
ALTER TABLE [dbo].[tblDatabases] DROP CONSTRAINT [DF__tblDataba__GUIDD__59C55456]
GO
/****** Object:  Table [dbo].[tblServerToJob]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerToJob]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerToJob]
GO
/****** Object:  Table [dbo].[tblServerToDisk]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerToDisk]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerToDisk]
GO
/****** Object:  Table [dbo].[tblServerToDatabase]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerToDatabase]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerToDatabase]
GO
/****** Object:  Table [dbo].[tblServers]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServers]') AND type in (N'U'))
DROP TABLE [dbo].[tblServers]
GO
/****** Object:  Table [dbo].[tblServerJobStatus]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerJobStatus]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerJobStatus]
GO
/****** Object:  Table [dbo].[tblServerJobs]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerJobs]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerJobs]
GO
/****** Object:  Table [dbo].[tblServerDiskSizes]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerDiskSizes]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerDiskSizes]
GO
/****** Object:  Table [dbo].[tblServerDisk]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblServerDisk]') AND type in (N'U'))
DROP TABLE [dbo].[tblServerDisk]
GO
/****** Object:  Table [dbo].[tblProcessLogs]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblProcessLogs]') AND type in (N'U'))
DROP TABLE [dbo].[tblProcessLogs]
GO
/****** Object:  Table [dbo].[tblNAVtoCompany]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNAVtoCompany]') AND type in (N'U'))
DROP TABLE [dbo].[tblNAVtoCompany]
GO
/****** Object:  Table [dbo].[tblNAVJobQueueLog]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNAVJobQueueLog]') AND type in (N'U'))
DROP TABLE [dbo].[tblNAVJobQueueLog]
GO
/****** Object:  Table [dbo].[tblNAVJobEntry]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNAVJobEntry]') AND type in (N'U'))
DROP TABLE [dbo].[tblNAVJobEntry]
GO
/****** Object:  Table [dbo].[tblNAVInstance]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNAVInstance]') AND type in (N'U'))
DROP TABLE [dbo].[tblNAVInstance]
GO
/****** Object:  Table [dbo].[tblNAVCompanyToJobEntry]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNAVCompanyToJobEntry]') AND type in (N'U'))
DROP TABLE [dbo].[tblNAVCompanyToJobEntry]
GO
/****** Object:  Table [dbo].[tblNAVCompany]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblNAVCompany]') AND type in (N'U'))
DROP TABLE [dbo].[tblNAVCompany]
GO
/****** Object:  Table [dbo].[tblDatabaseToTable]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabaseToTable]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabaseToTable]
GO
/****** Object:  Table [dbo].[tblDatabaseToNAV]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabaseToNAV]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabaseToNAV]
GO
/****** Object:  Table [dbo].[tblDatabaseTableSizes]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabaseTableSizes]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabaseTableSizes]
GO
/****** Object:  Table [dbo].[tblDatabaseTables]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabaseTables]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabaseTables]
GO
/****** Object:  Table [dbo].[tblDatabaseSize]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabaseSize]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabaseSize]
GO
/****** Object:  Table [dbo].[tblDatabases]    Script Date: 4/15/2022 8:29:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDatabases]') AND type in (N'U'))
DROP TABLE [dbo].[tblDatabases]
GO
/****** Object:  UserDefinedTableType [dbo].[tblServerDisk]    Script Date: 4/15/2022 8:29:35 AM ******/
DROP TYPE [dbo].[tblServerDisk]
GO
/****** Object:  UserDefinedTableType [dbo].[tblDatabaseTables]    Script Date: 4/15/2022 8:29:35 AM ******/
DROP TYPE [dbo].[tblDatabaseTables]
GO
/****** Object:  UserDefinedTableType [dbo].[tblDatabases]    Script Date: 4/15/2022 8:29:35 AM ******/
DROP TYPE [dbo].[tblDatabases]
GO
/****** Object:  UserDefinedTableType [dbo].[tblDatabases]    Script Date: 4/15/2022 8:29:35 AM ******/
CREATE TYPE [dbo].[tblDatabases] AS TABLE(
	[ServerGUID] [varchar](36) NULL,
	[GUIDDatabase] [varchar](36) NULL,
	[Name] [varchar](200) NULL,
	[IsDeleted] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tblDatabaseTables]    Script Date: 4/15/2022 8:29:35 AM ******/
CREATE TYPE [dbo].[tblDatabaseTables] AS TABLE(
	[ServerGUID] [varchar](36) NULL,
	[DatabaseGUID] [varchar](36) NULL,
	[DatabaseName] [varchar](200) NULL,
	[GUIDDatabaseTable] [varchar](36) NULL,
	[SchemaName] [varchar](75) NULL,
	[Name] [varchar](200) NULL,
	[IsDeleted] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[tblServerDisk]    Script Date: 4/15/2022 8:29:35 AM ******/
CREATE TYPE [dbo].[tblServerDisk] AS TABLE(
	[GUIDServerDisk] [varchar](36) NULL,
	[ServerGUID] [varchar](36) NULL,
	[Disk] [varchar](20) NULL,
	[FileSystem] [varchar](100) NULL,
	[LogicalDriveName] [varchar](100) NULL,
	[IsDeleted] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL
)
GO
/****** Object:  Table [dbo].[tblDatabases]    Script Date: 4/15/2022 8:29:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatabases](
	[UIDDatabase] [int] IDENTITY(1,1) NOT NULL,
	[ServerGUID] [varchar](36) NOT NULL,
	[GUIDDatabase] [varchar](36) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblDatabases] PRIMARY KEY CLUSTERED 
(
	[GUIDDatabase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatabaseSize]    Script Date: 4/15/2022 8:29:36 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatabaseTables]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatabaseTables](
	[UIDDatabaseTable] [int] IDENTITY(1,1) NOT NULL,
	[ServerGUID] [varchar](36) NOT NULL,
	[DatabaseGUID] [varchar](36) NULL,
	[DatabaseName] [varchar](200) NOT NULL,
	[GUIDDatabaseTable] [varchar](36) NOT NULL,
	[SchemaName] [varchar](75) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblDatabaseTable] PRIMARY KEY CLUSTERED 
(
	[GUIDDatabaseTable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatabaseTableSizes]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatabaseTableSizes](
	[UIDDatabaseTableSize] [bigint] IDENTITY(1,1) NOT NULL,
	[GUIDDatabaseTableSize] [varchar](36) NOT NULL,
	[DatabaseTableGUID] [varchar](36) NOT NULL,
	[RowsCount] [bigint] NOT NULL,
	[TotalSpaceMB] [bigint] NOT NULL,
	[UsedSpaceMB] [bigint] NOT NULL,
	[UnusedSpaceMB] [bigint] NOT NULL,
	[DateReported] [datetime] NOT NULL,
	[MonthReported] [int] NOT NULL,
	[YearReported] [int] NOT NULL,
	[WeekNumReported] [int] NOT NULL,
 CONSTRAINT [PK_tblDatabaseTableSizes] PRIMARY KEY CLUSTERED 
(
	[GUIDDatabaseTableSize] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatabaseToNAV]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatabaseToNAV]
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatabaseToTable]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatabaseToTable]
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNAVCompany]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNAVCompany](
	[GUIDNAVCompany] [varchar](36) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblNAVCompany] PRIMARY KEY CLUSTERED 
(
	[GUIDNAVCompany] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNAVCompanyToJobEntry]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNAVCompanyToJobEntry]
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNAVInstance]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNAVInstance](
	[GUIDNAVInstance] [varchar](36) NOT NULL,
	[ServerGUID] [varchar](36) NOT NULL,
	[DatabaseGUID] [varchar](36) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ServerComputerName] [varchar](200) NULL,
	[ServerInstanceName] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblNAVInstance] PRIMARY KEY CLUSTERED 
(
	[GUIDNAVInstance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNAVJobEntry]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNAVJobEntry](
	[GUIDNAVJobEntry] [varchar](36) NOT NULL,
	[NAVJobID] [uniqueidentifier] NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[ObjectIDToRun] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblNAVJobEntry] PRIMARY KEY CLUSTERED 
(
	[GUIDNAVJobEntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNAVJobQueueLog]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNAVJobQueueLog](
	[UIDJobQueueLog] [bigint] IDENTITY(1,1) NOT NULL,
	[GUIDNAVJobQueueLog] [varchar](36) NULL,
	[EarliestStartDateTime] [datetime] NULL,
	[JobStatus] [int] NULL,
	[SystemTaskID] [uniqueidentifier] NOT NULL,
	[IsReady] [bit] NULL,
	[NotBefore] [datetime] NULL,
	[DateReported] [datetime] NOT NULL,
	[MonthReported] [int] NOT NULL,
	[YearReported] [int] NOT NULL,
	[WeekNumReported] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNAVtoCompany]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNAVtoCompany]
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProcessLogs]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProcessLogs](
	[UIDProcessLog] [bigint] IDENTITY(1,1) NOT NULL,
	[GUIDProcessLog] [varchar](36) NOT NULL,
	[RunID] [varchar](36) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
	[ServersProcessed] [int] NOT NULL,
	[DatabasesProcessed] [int] NOT NULL,
	[TablesProcessed] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblProcessLogs] PRIMARY KEY CLUSTERED 
(
	[GUIDProcessLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerDisk]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerDisk](
	[UIDServerDisk] [int] IDENTITY(1,1) NOT NULL,
	[GUIDServerDisk] [varchar](36) NOT NULL,
	[ServerGUID] [varchar](36) NOT NULL,
	[Disk] [varchar](20) NOT NULL,
	[FileSystem] [varchar](100) NOT NULL,
	[LogicalDriveName] [varchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblServerDisk] PRIMARY KEY CLUSTERED 
(
	[GUIDServerDisk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerDiskSizes]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerDiskSizes](
	[UIDServerDiskSize] [int] IDENTITY(1,1) NOT NULL,
	[GUIDServerDiskSize] [varchar](36) NOT NULL,
	[ServerDiskGUID] [varchar](36) NOT NULL,
	[TotalSizeInMB] [decimal](18, 2) NOT NULL,
	[AvailableInMB] [decimal](18, 2) NOT NULL,
	[FreeSpacePercent] [decimal](18, 2) NOT NULL,
	[DateReported] [datetime] NOT NULL,
	[MonthReported] [int] NOT NULL,
	[YearReported] [int] NOT NULL,
	[WeekNumReported] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerJobs]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerJobs](
	[GUIDServerJob] [varchar](36) NOT NULL,
	[Job_Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblServerJobs] PRIMARY KEY CLUSTERED 
(
	[GUIDServerJob] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerJobStatus]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerJobStatus](
	[UIDServerJobStatus] [bigint] IDENTITY(1,1) NOT NULL,
	[GUIDServerJobStatus] [varchar](36) NOT NULL,
	[ServerJobGUID] [varchar](36) NOT NULL,
	[RunStatus] [varchar](50) NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[DateReported] [datetime] NOT NULL,
 CONSTRAINT [PK_tblServerJobStatus] PRIMARY KEY CLUSTERED 
(
	[GUIDServerJobStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServers]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServers](
	[GUIDServer] [varchar](36) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ConnectionString] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblServers] PRIMARY KEY CLUSTERED 
(
	[GUIDServer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
AS NODE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerToDatabase]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerToDatabase]
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerToDisk]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerToDisk]
AS EDGE ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblServerToJob]    Script Date: 4/15/2022 8:29:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblServerToJob]
AS EDGE ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblDatabases] ADD  DEFAULT (newid()) FOR [GUIDDatabase]
GO
ALTER TABLE [dbo].[tblDatabases] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblDatabases] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblDatabases] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT (newid()) FOR [GUIDDatabaseSize]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT ((0)) FOR [CurrentFileSizeMB]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT ((0)) FOR [TotalDBSizeMB]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT (getdate()) FOR [DateReported]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT ((0)) FOR [MonthReported]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT ((0)) FOR [YearReported]
GO
ALTER TABLE [dbo].[tblDatabaseSize] ADD  DEFAULT ((0)) FOR [WeekNumReported]
GO
ALTER TABLE [dbo].[tblDatabaseTables] ADD  DEFAULT (newid()) FOR [GUIDDatabaseTable]
GO
ALTER TABLE [dbo].[tblDatabaseTables] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblDatabaseTables] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblDatabaseTables] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT (newid()) FOR [GUIDDatabaseTableSize]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [RowsCount]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [TotalSpaceMB]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [UsedSpaceMB]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [UnusedSpaceMB]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT (getdate()) FOR [DateReported]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [MonthReported]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [YearReported]
GO
ALTER TABLE [dbo].[tblDatabaseTableSizes] ADD  DEFAULT ((0)) FOR [WeekNumReported]
GO
ALTER TABLE [dbo].[tblNAVCompany] ADD  DEFAULT (newid()) FOR [GUIDNAVCompany]
GO
ALTER TABLE [dbo].[tblNAVCompany] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblNAVCompany] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblNAVCompany] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblNAVInstance] ADD  DEFAULT (newid()) FOR [GUIDNAVInstance]
GO
ALTER TABLE [dbo].[tblNAVInstance] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblNAVInstance] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblNAVInstance] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] ADD  DEFAULT (newid()) FOR [GUIDNAVJobEntry]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblNAVJobEntry] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] ADD  CONSTRAINT [DF_tblNAVJobQueueLog_GUIDNAVJobQueueLog]  DEFAULT (newid()) FOR [GUIDNAVJobQueueLog]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] ADD  CONSTRAINT [DF_tblNAVJobQueueLog_DateReported]  DEFAULT (getdate()) FOR [DateReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] ADD  CONSTRAINT [DF_tblNAVJobQueueLog_MonthReported]  DEFAULT ((0)) FOR [MonthReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] ADD  CONSTRAINT [DF_tblNAVJobQueueLog_YearReported]  DEFAULT ((0)) FOR [YearReported]
GO
ALTER TABLE [dbo].[tblNAVJobQueueLog] ADD  CONSTRAINT [DF_tblNAVJobQueueLog_WeekNumReported]  DEFAULT ((0)) FOR [WeekNumReported]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT (newid()) FOR [GUIDProcessLog]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT (newid()) FOR [RunID]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT (getdate()) FOR [StartTime]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT ((0)) FOR [ServersProcessed]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT ((0)) FOR [DatabasesProcessed]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT ((0)) FOR [TablesProcessed]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblProcessLogs] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblServerDisk] ADD  DEFAULT (newid()) FOR [GUIDServerDisk]
GO
ALTER TABLE [dbo].[tblServerDisk] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblServerDisk] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblServerDisk] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] ADD  CONSTRAINT [DF_tblServerDiskSizes_GUIDServerDiskSize]  DEFAULT (newid()) FOR [GUIDServerDiskSize]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] ADD  CONSTRAINT [DF_tblServerDiskSizes_TotalSizeInMB]  DEFAULT ((0)) FOR [TotalSizeInMB]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] ADD  CONSTRAINT [DF_tblServerDiskSizes_DateReported]  DEFAULT (getdate()) FOR [DateReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] ADD  CONSTRAINT [DF_tblServerDiskSizes_MonthReported]  DEFAULT ((0)) FOR [MonthReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] ADD  CONSTRAINT [DF_tblServerDiskSizes_YearReported]  DEFAULT ((0)) FOR [YearReported]
GO
ALTER TABLE [dbo].[tblServerDiskSizes] ADD  CONSTRAINT [DF_tblServerDiskSizes_WeekNumReported]  DEFAULT ((0)) FOR [WeekNumReported]
GO
ALTER TABLE [dbo].[tblServerJobs] ADD  DEFAULT (newid()) FOR [GUIDServerJob]
GO
ALTER TABLE [dbo].[tblServerJobs] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblServerJobs] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblServerJobs] ADD  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tblServerJobStatus] ADD  CONSTRAINT [DF_Table_1_GUIDNAVJobEntryStatus]  DEFAULT (newid()) FOR [GUIDServerJobStatus]
GO
ALTER TABLE [dbo].[tblServerJobStatus] ADD  CONSTRAINT [DF_tblServerJobStatus_DateReported]  DEFAULT (getdate()) FOR [DateReported]
GO
ALTER TABLE [dbo].[tblServers] ADD  DEFAULT (newid()) FOR [GUIDServer]
GO
ALTER TABLE [dbo].[tblServers] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[tblServers] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tblServers] ADD  DEFAULT (getdate()) FOR [DateModified]
GO

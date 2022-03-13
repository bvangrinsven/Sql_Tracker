SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblServers](
	[UIDServer] [int] IDENTITY(1,1) NOT NULL,
	[GUIDServer] [varchar](36) NOT NULL,
	[ServerGUID] [varchar](36) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[ConnectionString] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblServers] PRIMARY KEY CLUSTERED 
(
	[GUIDServer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblServers] ADD  CONSTRAINT [DF_tblServers_GUIDServer]  DEFAULT (newid()) FOR [GUIDServer]
GO

ALTER TABLE [dbo].[tblServers] ADD  CONSTRAINT [DF_tblServers_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[tblServers] ADD  CONSTRAINT [DF_tblServers_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO

CREATE TABLE [dbo].[tblDatabases](
	[UIDDatabase] [int] IDENTITY(1,1) NOT NULL,
	[GUIDDatabase] [varchar](36) NOT NULL,
	[ServerGUID] [varchar](36) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblDatabases] PRIMARY KEY CLUSTERED 
(
	[GUIDDatabase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblDatabases] ADD  CONSTRAINT [DF_tblDatabases_GUIDDatabase]  DEFAULT (newid()) FOR [GUIDDatabase]
GO

ALTER TABLE [dbo].[tblDatabases] ADD  CONSTRAINT [DF_tblDatabases_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[tblDatabases] ADD  CONSTRAINT [DF_tblDatabases_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO

CREATE TABLE [dbo].[tblDatabaseTables](
	[UIDDatabaseTable] [int] IDENTITY(1,1) NOT NULL,
	[GUIDDatabaseTable] [varchar](36) NOT NULL,
	[DatabaseGUID] [varchar](36) NOT NULL,
	[SchemaName] [varchar](75) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_tblDatabaseTable] PRIMARY KEY CLUSTERED 
(
	[GUIDDatabaseTable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblDatabaseTables] ADD  CONSTRAINT [DF_tblDatabaseTable_GUIDDatabaseTable]  DEFAULT (newid()) FOR [GUIDDatabaseTable]
GO

ALTER TABLE [dbo].[tblDatabaseTables] ADD  CONSTRAINT [DF_tblDatabaseTable_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[tblDatabaseTables] ADD  CONSTRAINT [DF_tblDatabaseTable_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
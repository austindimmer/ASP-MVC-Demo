USE [Powerfront]
GO
ALTER TABLE [dbo].[CustomerRecords] DROP CONSTRAINT [FK_TypeId]
GO
ALTER TABLE [dbo].[CustomerRecords] DROP CONSTRAINT [FK_PropertyId]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 10/09/2016 13:23:49 ******/
DROP TABLE [dbo].[Type]
GO
/****** Object:  Table [dbo].[Property]    Script Date: 10/09/2016 13:23:49 ******/
DROP TABLE [dbo].[Property]
GO
/****** Object:  Table [dbo].[CustomerRecords]    Script Date: 10/09/2016 13:23:49 ******/
DROP TABLE [dbo].[CustomerRecords]
GO
/****** Object:  Table [dbo].[CustomerRecords]    Script Date: 10/09/2016 13:23:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerRecords](
	[TypeId] [varchar](255) NOT NULL,
	[PropertyId] [varchar](255) NOT NULL,
	[Value] [varchar](255) NOT NULL,
	[CustomerId] [varchar](255) NOT NULL,
	[RecordId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RecordId] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Property]    Script Date: 10/09/2016 13:23:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Property](
	[PropertyId] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
 CONSTRAINT [PK_AttributeId] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Type]    Script Date: 10/09/2016 13:23:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Type](
	[TypeId] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
 CONSTRAINT [PK_EntityId] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[CustomerRecords] ([TypeId], [PropertyId], [Value], [CustomerId], [RecordId]) VALUES (N'1', N'1', N'Hadar', N'1', N'bad541e7-8fec-4e65-8969-ddeb4ba53433')
INSERT [dbo].[CustomerRecords] ([TypeId], [PropertyId], [Value], [CustomerId], [RecordId]) VALUES (N'1', N'2', N'47', N'1', N'bad541e7-8fec-4e65-8969-ddeb4ba53434')
INSERT [dbo].[CustomerRecords] ([TypeId], [PropertyId], [Value], [CustomerId], [RecordId]) VALUES (N'1', N'1', N'Greg', N'2', N'bad541e7-8fec-4e65-8969-ddeb4ba53435')
INSERT [dbo].[CustomerRecords] ([TypeId], [PropertyId], [Value], [CustomerId], [RecordId]) VALUES (N'1', N'2', N'38', N'2', N'bad541e7-8fec-4e65-8969-ddeb4ba53436')
INSERT [dbo].[CustomerRecords] ([TypeId], [PropertyId], [Value], [CustomerId], [RecordId]) VALUES (N'1', N'1', N'Michael', N'3', N'bad541e7-8fec-4e65-8969-ddeb4ba53437')
INSERT [dbo].[CustomerRecords] ([TypeId], [PropertyId], [Value], [CustomerId], [RecordId]) VALUES (N'1', N'2', N'45', N'3', N'bad541e7-8fec-4e65-8969-ddeb4ba53438')
INSERT [dbo].[Property] ([PropertyId], [Name]) VALUES (N'1', N'Name')
INSERT [dbo].[Property] ([PropertyId], [Name]) VALUES (N'2', N'Age')
INSERT [dbo].[Type] ([TypeId], [Name]) VALUES (N'1', N'Customer')
ALTER TABLE [dbo].[CustomerRecords]  WITH CHECK ADD  CONSTRAINT [FK_PropertyId] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Property] ([PropertyId])
GO
ALTER TABLE [dbo].[CustomerRecords] CHECK CONSTRAINT [FK_PropertyId]
GO
ALTER TABLE [dbo].[CustomerRecords]  WITH CHECK ADD  CONSTRAINT [FK_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Type] ([TypeId])
GO
ALTER TABLE [dbo].[CustomerRecords] CHECK CONSTRAINT [FK_TypeId]
GO

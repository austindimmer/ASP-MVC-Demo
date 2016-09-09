

/****** Object:  Table [dbo].[Customers]    Script Date: 09/09/2016 20:04:09 ******/


CREATE TABLE [dbo].[Customers](
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

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_PropertyId] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Property] ([PropertyId])
GO

ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_PropertyId]
GO

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Type] ([TypeId])
GO

ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_TypeId]
GO


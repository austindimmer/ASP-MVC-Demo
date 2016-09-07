
CREATE TABLE [dbo].[ObjectPropertyTable](
	[id] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Age] [varchar](255) NOT NULL,
	[TypeId] [varchar](255) NOT NULL,
 CONSTRAINT [PK_] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ObjectPropertyTable]  WITH CHECK ADD  CONSTRAINT [FK_TypeId] FOREIGN KEY([TypeId])
REFERENCES [dbo].[ObjectTypeTable] ([id])
GO

ALTER TABLE [dbo].[ObjectPropertyTable] CHECK CONSTRAINT [FK_TypeId]
GO


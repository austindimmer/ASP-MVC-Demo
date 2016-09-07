
CREATE TABLE [dbo].[ObjectDataTable](
	[id] [varchar](255) NOT NULL,
	[PropertiesId] [varchar](255) NOT NULL,
 CONSTRAINT [PK_ObjectId] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[ObjectDataTable]  WITH CHECK ADD  CONSTRAINT [FK_PropertiesId] FOREIGN KEY([id])
REFERENCES [dbo].[ObjectPropertyTable] ([id])
GO

ALTER TABLE [dbo].[ObjectDataTable] CHECK CONSTRAINT [FK_PropertiesId]
GO


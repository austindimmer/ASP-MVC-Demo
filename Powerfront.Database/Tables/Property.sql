/****** Object:  Table [dbo].[Property]    Script Date: 09/09/2016 14:04:37 ******/

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
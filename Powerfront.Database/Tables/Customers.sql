/****** Object:  Table [dbo].[Customers]    Script Date: 09/09/2016 14:04:37 ******/

CREATE TABLE [dbo].[Customers](
	[CustomerId] [varchar](255) NOT NULL,
	[TypeId] [varchar](255) NOT NULL,
	[PropertyId] [varchar](255) NOT NULL,
	[Value] [varchar](255) NOT NULL,
 CONSTRAINT [PK_CustomerId] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
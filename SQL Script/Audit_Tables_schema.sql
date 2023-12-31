USE [AuditCompliance]
GO
/****** Object:  Table [dbo].[AuditCategory]    Script Date: 11/19/2023 3:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditCategory](
	[AuditCategoryCode] [nvarchar](50) NOT NULL,
	[AuditCategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AuditCategory] PRIMARY KEY CLUSTERED 
(
	[AuditCategoryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditClass]    Script Date: 11/19/2023 3:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditClass](
	[AuditClassCode] [nvarchar](50) NOT NULL,
	[AuditClassName] [nvarchar](50) NOT NULL,
	[AuditCategoryCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AuditClass] PRIMARY KEY CLUSTERED 
(
	[AuditClassCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditRiskItem]    Script Date: 11/19/2023 3:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditRiskItem](
	[RiskItemCode] [nvarchar](50) NOT NULL,
	[RiskItemName] [nvarchar](50) NOT NULL,
	[RiskItemSubClassCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AuditRiskItem] PRIMARY KEY CLUSTERED 
(
	[RiskItemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiskItemCategory]    Script Date: 11/19/2023 3:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiskItemCategory](
	[RiskItemCategoryCode] [nvarchar](50) NOT NULL,
	[RiskItemCategoryName] [nvarchar](50) NOT NULL,
	[AuditClassCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RiskItemCategory] PRIMARY KEY CLUSTERED 
(
	[RiskItemCategoryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiskItemClass]    Script Date: 11/19/2023 3:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiskItemClass](
	[RiskItemClassCode] [nvarchar](50) NOT NULL,
	[RiskItemClassName] [nvarchar](50) NOT NULL,
	[RiskItemCategoryCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RiskItemClass] PRIMARY KEY CLUSTERED 
(
	[RiskItemClassCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiskItemSubClass]    Script Date: 11/19/2023 3:09:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiskItemSubClass](
	[RiskItemSubClassCode] [nvarchar](50) NOT NULL,
	[RiskItemSubClassName] [nvarchar](50) NOT NULL,
	[RiskItemClassCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RiskItemSubClass] PRIMARY KEY CLUSTERED 
(
	[RiskItemSubClassCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditClass]  WITH CHECK ADD  CONSTRAINT [FK_AuditClass_AuditCategory] FOREIGN KEY([AuditCategoryCode])
REFERENCES [dbo].[AuditCategory] ([AuditCategoryCode])
GO
ALTER TABLE [dbo].[AuditClass] CHECK CONSTRAINT [FK_AuditClass_AuditCategory]
GO
ALTER TABLE [dbo].[AuditRiskItem]  WITH CHECK ADD  CONSTRAINT [FK_AuditRiskItem_RiskItemSubClass] FOREIGN KEY([RiskItemSubClassCode])
REFERENCES [dbo].[RiskItemSubClass] ([RiskItemSubClassCode])
GO
ALTER TABLE [dbo].[AuditRiskItem] CHECK CONSTRAINT [FK_AuditRiskItem_RiskItemSubClass]
GO
ALTER TABLE [dbo].[RiskItemCategory]  WITH CHECK ADD  CONSTRAINT [FK_RiskItemCategory_AuditClass] FOREIGN KEY([AuditClassCode])
REFERENCES [dbo].[AuditClass] ([AuditClassCode])
GO
ALTER TABLE [dbo].[RiskItemCategory] CHECK CONSTRAINT [FK_RiskItemCategory_AuditClass]
GO
ALTER TABLE [dbo].[RiskItemClass]  WITH CHECK ADD  CONSTRAINT [FK_RiskItemClass_RiskItemCategory] FOREIGN KEY([RiskItemCategoryCode])
REFERENCES [dbo].[RiskItemCategory] ([RiskItemCategoryCode])
GO
ALTER TABLE [dbo].[RiskItemClass] CHECK CONSTRAINT [FK_RiskItemClass_RiskItemCategory]
GO
ALTER TABLE [dbo].[RiskItemSubClass]  WITH CHECK ADD  CONSTRAINT [FK_RiskItemSubClass_RiskItemClass] FOREIGN KEY([RiskItemClassCode])
REFERENCES [dbo].[RiskItemClass] ([RiskItemClassCode])
GO
ALTER TABLE [dbo].[RiskItemSubClass] CHECK CONSTRAINT [FK_RiskItemSubClass_RiskItemClass]
GO

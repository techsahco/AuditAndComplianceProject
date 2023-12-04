USE [AuditCompliance]
GO

/****** Object:  Table [dbo].[RiskItem]    Script Date: 10/17/2023 12:08:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RiskItem](
	[RiskItemID] [int] IDENTITY(1,1) NOT NULL,
	[RiskItemName] [varchar](255) NULL,
	[Priority] [varchar](20) NULL,
 CONSTRAINT [PK__RiskItem__EB769779AA57F1DD] PRIMARY KEY CLUSTERED 
(
	[RiskItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



USE [AuditCompliance]
GO

/****** Object:  Table [dbo].[AuditRiskItemSecondaryDepartment]    Script Date: 12/4/2023 11:54:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuditRiskItemSecondaryDepartment](
	[DepartmentId] [int] NOT NULL,
	[AuditRiskItemCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AuditRiskItemSecondaryDepartment] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC,
	[AuditRiskItemCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



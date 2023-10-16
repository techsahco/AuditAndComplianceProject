USE [AuditCompliance]
GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/16/2023 9:31:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DepartmentUser](
	[UserId] [nvarchar](128) NOT NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.DepartmentUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DepartmentUser]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DepartmentUser_dbo.Department_DepartmentId] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DepartmentUser] CHECK CONSTRAINT [FK_dbo.DepartmentUser_dbo.Department_DepartmentId]
GO

ALTER TABLE [dbo].[DepartmentUser]  WITH CHECK ADD  CONSTRAINT [FK_dbo.DepartmentUser_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DepartmentUser] CHECK CONSTRAINT [FK_dbo.DepartmentUser_dbo.AspNetUsers_UserId]
GO



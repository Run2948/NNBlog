CREATE DATABASE [NNBLOG]
USE [NNBlog]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 2018-01-02 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UserName] [nvarchar](64) NULL,
	[Password] [nvarchar](64) NULL,
	[Remark] [nvarchar](2048) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Blog]    Script Date: 2018-01-02 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Title] [nvarchar](256) NULL,
	[Body] [ntext] NULL,
	[Body_md] [ntext] NULL,
	[VisitNo] [int] NOT NULL,
	[CateNo] [nvarchar](64) NULL,
	[CateName] [nvarchar](64) NULL,
	[Remark] [nvarchar](2048) NULL,
	[Sort] [int] NOT NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 2018-01-02 14:26:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CateName] [nvarchar](64) NULL,
	[CateNo] [nvarchar](64) NULL,
	[ParentCate] [nvarchar](64) NULL,
	[Remark] [nvarchar](2048) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

GO
INSERT [dbo].[Admin] ([Id], [CreateDate], [UserName], [Password], [Remark]) VALUES (1, CAST(0x0000A85C00EC9963 AS DateTime), N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'管理员')
GO
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([Id], [CreateDate], [CateName], [CateNo], [ParentCate], [Remark]) VALUES (1, CAST(0x0000A85C00ED382E AS DateTime), N'ASP.NET', N'01', N'0', NULL)
GO
INSERT [dbo].[Category] ([Id], [CreateDate], [CateName], [CateNo], [ParentCate], [Remark]) VALUES (2, CAST(0x0000A85C00ED431C AS DateTime), N'PHP', N'02', N'0', NULL)
GO
INSERT [dbo].[Category] ([Id], [CreateDate], [CateName], [CateNo], [ParentCate], [Remark]) VALUES (3, CAST(0x0000A85C00ED50FD AS DateTime), N'Android', N'03', N'0', NULL)
GO
INSERT [dbo].[Category] ([Id], [CreateDate], [CateName], [CateNo], [ParentCate], [Remark]) VALUES (4, CAST(0x0000A85C00ED5D24 AS DateTime), N'JAVA', N'04', N'0', NULL)
GO
INSERT [dbo].[Category] ([Id], [CreateDate], [CateName], [CateNo], [ParentCate], [Remark]) VALUES (5, CAST(0x0000A85C00ED6BE0 AS DateTime), N'IOS', N'05', N'0', NULL)
GO
INSERT [dbo].[Category] ([Id], [CreateDate], [CateName], [CateNo], [ParentCate], [Remark]) VALUES (6, CAST(0x0000A85C00EDA27E AS DateTime), N'随笔', N'06', N'0', NULL)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [DF_Admin_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Blog] ADD  CONSTRAINT [DF_Blog_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Blog] ADD  CONSTRAINT [DF_Blog_VisitNo]  DEFAULT ((0)) FOR [VisitNo]
GO
ALTER TABLE [dbo].[Blog] ADD  CONSTRAINT [DF_Blog_Sort]  DEFAULT ((0)) FOR [Sort]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'管理员表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Admin', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'博客表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'博客标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'博客内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'Body'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MarkDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'Body_md'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'VisitNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'CateNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'CateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序（从小到大）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Blog', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'CateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'CateNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父分类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'ParentCate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Remark'
GO

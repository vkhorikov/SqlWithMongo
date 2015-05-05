CREATE DATABASE [SqlWithMongo]
GO
USE [SqlWithMongo]
GO
/****** Object:  Table [dbo].[Ids]    Script Date: 5/5/2015 8:22:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ids](
	[EntityName] [nvarchar](100) NOT NULL,
	[NextHigh] [int] NOT NULL,
 CONSTRAINT [PK_Ids] PRIMARY KEY CLUSTERED 
(
	[EntityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MongoNode]    Script Date: 5/5/2015 8:22:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MongoNode](
	[MongoNodeId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_MongoNode] PRIMARY KEY CLUSTERED 
(
	[MongoNodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Node]    Script Date: 5/5/2015 8:22:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Node](
	[NodeId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_NetworkNode] PRIMARY KEY CLUSTERED 
(
	[NodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Node_Node]    Script Date: 5/5/2015 8:22:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Node_Node](
	[NodeId1] [int] NOT NULL,
	[NodeId2] [int] NOT NULL,
 CONSTRAINT [PK_NetworkNode_NetworkNode] PRIMARY KEY CLUSTERED 
(
	[NodeId1] ASC,
	[NodeId2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Node_Node]  WITH CHECK ADD  CONSTRAINT [FK_NetworkNode_NetworkNode_NetworkNode] FOREIGN KEY([NodeId1])
REFERENCES [dbo].[Node] ([NodeId])
GO
ALTER TABLE [dbo].[Node_Node] CHECK CONSTRAINT [FK_NetworkNode_NetworkNode_NetworkNode]
GO
ALTER TABLE [dbo].[Node_Node]  WITH CHECK ADD  CONSTRAINT [FK_NetworkNode_NetworkNode_NetworkNode1] FOREIGN KEY([NodeId2])
REFERENCES [dbo].[Node] ([NodeId])
GO
ALTER TABLE [dbo].[Node_Node] CHECK CONSTRAINT [FK_NetworkNode_NetworkNode_NetworkNode1]
GO

INSERT dbo.Ids (EntityName, NextHigh)
VALUES ('MongoNode', 0)
INSERT dbo.Ids (EntityName, NextHigh)
VALUES ('Node', 0)

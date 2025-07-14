USE [master]
GO

-- Drop the database if it already exists to ensure a clean slate
-- Uncomment the following lines if you want to drop the database before creation
-- IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MyStore')
-- BEGIN
--     ALTER DATABASE [MyStore] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--     DROP DATABASE [MyStore];
-- END
-- GO

/****** Object:  Database [MyStore]  Script Date: 5/6/2024 11:32:56 AM ******/
CREATE DATABASE [MyStore]
GO

USE [MyStore]
GO

/****** Object:  Table [dbo].[AccountMember]  Script Date: 5/6/2024 11:32:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountMember](
	[MemberID] [nvarchar](20) NOT NULL,
	[MemberPassword] [nvarchar](80) NOT NULL,
	[FullName] [nvarchar](80) NOT NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[MemberRole] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Categories]  Script Date: 5/6/2024 11:32:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Products]  Script Date: 5/6/2024 11:32:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](40) NOT NULL,
	[CategoryID] [int] NULL,
	[UnitsInStock] [smallint] NULL,
	[UnitPrice] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Initial Data for AccountMember
INSERT [dbo].[AccountMember] ([MemberID], [MemberPassword], [FullName], [EmailAddress], [MemberRole]) VALUES (N'PS0001', N'@1', N'Administrator', N'admin@CompanyName.com', 1)
GO
INSERT [dbo].[AccountMember] ([MemberID], [MemberPassword], [FullName], [EmailAddress], [MemberRole]) VALUES (N'PS0002', N'@2', N'Staff', N'staff@CompanyName.com', 2)
GO
INSERT [dbo].[AccountMember] ([MemberID], [MemberPassword], [FullName], [EmailAddress], [MemberRole]) VALUES (N'PS0003', N'@3', N'Member 1', N'member1@CompanyName.com', 3)
GO
INSERT [dbo].[AccountMember] ([MemberID], [MemberPassword], [FullName], [EmailAddress], [MemberRole]) VALUES (N'PS0004', N'@3', N'Member 2', N'member2@CompanyName.com', 3)
GO

-- Additional Data for AccountMember
INSERT [dbo].[AccountMember] ([MemberID], [MemberPassword], [FullName], [EmailAddress], [MemberRole]) VALUES (N'PS0005', N'@5', N'New Member', N'new.member@CompanyName.com', 3)
GO
INSERT [dbo].[AccountMember] ([MemberID], [MemberPassword], [FullName], [EmailAddress], [MemberRole]) VALUES (N'PS0006', N'@6', N'Another Member', N'another.member@CompanyName.com', 3)
GO

-- Initial Data for Categories
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'Beverages')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Condiments')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Confections')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Dairy Products')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (5, N'Grains/Cereals')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (6, N'Meat/Poultry')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (7, N'Produce')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (8, N'Seafood')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO

-- Additional Data for Categories
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (9, N'Baked Goods')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (10, N'Snacks')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO

-- Initial Data for Products
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (1, N'Chai', 3, 12, 18.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (2, N'Chang', 1, 23, 19.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (3, N'Aniseed Syrup', 2, 23, 10.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (4, N'Chef Anton''s Cajun Seasoning', 2, 34, 22.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (5, N'Chef Anton''s Gumbo Mix', 2, 45, 21.3500)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (6, N'Grandma''s Boysenberry Spread', 2, 21, 25.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (7, N'Uncle Bob''s Organic Dried Pears', 7, 22, 30.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (8, N'Northwoods Cranberry Sauce', 2, 10, 40.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (9, N'Mishi Kobe Niku', 6, 12, 97.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (10, N'Ikura', 8, 13, 31.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (11, N'Tofu', 4, 35, 23.2500)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (12, N'Sir Rodney''s Marmalade', 2, 40, 81.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (13, N'Sir Rodney''s Scones', 2, 3, 10.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (14, N'Sir Rodney''s Clotted Cream', 4, 6, 15.5000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (15, N'Sir Rodney''s Shortbread', 2, 40, 10.0000)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO

-- Additional Data for Products
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (16, N'Gorgonzola Telino', 4, 18, 12.5000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (17, N'Fløtemysost', 4, 25, 26.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (18, N'Mozzarella di Giovanni', 4, 14, 34.8000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (19, N'Rössle Sauerkraut', 7, 36, 45.6000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (20, N'Thüringer Rostbratwurst', 6, 21, 123.7900)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (21, N'Nord-Ost Matjeshering', 8, 10, 25.8900)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (22, N'Gudbrandsdalsost', 4, 20, 36.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (23, N'Camembert Pierrot', 4, 16, 34.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (24, N'Schoggi Schokolade', 3, 49, 43.9000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (25, N'Zaanse koeken', 9, 36, 9.5000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (26, N'Wimmers gute Semmelknödel', 5, 42, 33.2500)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (27, N'Louisiana Fiery Hot Pepper Sauce', 2, 25, 21.0500)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (28, N'Louisiana Hot Spiced Okra', 2, 4, 17.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (29, N'Laughing Lumberjack Lager', 1, 52, 14.0000)
GO
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitsInStock], [UnitPrice]) VALUES (30, N'Scottish Longbreads', 9, 6, 12.5000)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [UQ__AccountM__49A147404558FAB7]  Script Date: 5/6/2024 11:32:57 AM ******/
ALTER TABLE [dbo].[AccountMember] ADD UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products] WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO

/****** Object:  StoredProcedure [dbo].[spCountProductsUsingOutputValue]  Script Date: 5/6/2024 11:32:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[spCountProductsUsingOutputValue](@CategoryID int,@NumberOfProducts int Output) As
	Select  @NumberOfProducts = Count(ProductID)
	From Products
	where CategoryID = @CategoryID
	Group by CategoryID
GO

/****** Object:  StoredProcedure [dbo].[spCountProductsUsingReturnValue]  Script Date: 5/6/2024 11:32:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[spCountProductsUsingReturnValue](@CategoryID int) As
	Declare  @NumberOfProducts int
	Select  @NumberOfProducts = Count(ProductID)
	From Products
	where CategoryID = @CategoryID
	Group by CategoryID
	Return  @NumberOfProducts
GO

USE [master]
GO

ALTER DATABASE [MyStore] SET READ_WRITE 
GO
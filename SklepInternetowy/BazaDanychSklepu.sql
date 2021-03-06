USE [master]
GO
/****** Object:  Database [StoreDatabase]    Script Date: 2 lut 2022 15:10:04 ******/
CREATE DATABASE [StoreDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\StoreDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StoreDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\StoreDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [StoreDatabase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StoreDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StoreDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StoreDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [StoreDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StoreDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StoreDatabase] SET QUERY_STORE = OFF
GO
USE [StoreDatabase]
GO
/****** Object:  UserDefinedFunction [dbo].[AvalilableProducts]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[AvalilableProducts](@valueId int)

RETURNS int
AS
BEGIN

	DECLARE @resultVar int=0
	SELECT  @resultVar= SUM(Quantity)FROM dbo.HelpToQuantity(@valueId)


	RETURN @resultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[IsUserNick]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[IsUserNick] (@valueNick varchar(50),@valueHash char(64))
RETURNS int
AS
BEGIN

	DECLARE @ResultVar int=0 

	if exists(Select * FROM Users WHERE Nick=@valueNick and HashPassword=@valueHash )  
	BEGIN
		SELECT @ResultVar=1;
	END

	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[ReadQuantity]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ReadQuantity](@valueId int)
	
RETURNS int
AS
BEGIN
	DECLARE @ResultVar int,@sumActualQuantity int=0
	SELECT @ResultVar = MaxQuantity FROM Product WHERE Id_Product=@valueId
	
	if(exists (SELECT Id_RetailSales FROM ViewAvailableQuantity WHERE Id_Product=@valueId) )
	BEGIN
		SELECT @sumActualQuantity=SUM(Quantity) FROM ViewAvailableQuantity WHERE Id_Product=@valueId
	END
	SELECT @ResultVar=@ResultVar-@sumActualQuantity

	RETURN @ResultVar

END
GO
/****** Object:  UserDefinedFunction [dbo].[ValueLimitStringBank]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[ValueLimitStringBank] (@valueId AS INT)
RETURNS int
AS
BEGIN
	DECLARE @tempLimit AS INT;
	SELECT @tempLimit= (SELECT LimitString FROM TypePayment WHERE Id_TypePayment=@valueId)
	RETURN @tempLimit;
END
GO
/****** Object:  UserDefinedFunction [dbo].[valueMaxStringBank]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[valueMaxStringBank](@valueId int)

RETURNS int
AS
BEGIN
	DECLARE @valueResult as int
	SELECT @valueResult=LimitString FROM TypePayment WHERE Id_TypePayment=@valueId
	RETURN @valueResult
END
GO
/****** Object:  Table [dbo].[Delivery]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery](
	[Id_Delivery] [int] NOT NULL,
	[NameDelivery] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Deliverce] PRIMARY KEY CLUSTERED 
(
	[Id_Delivery] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id_Product] [int] NOT NULL,
	[Id_Seller] [int] NOT NULL,
	[Name] [varchar](25) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Price] [money] NOT NULL,
	[Id_Vat] [int] NOT NULL,
	[Id_Condition] [int] NOT NULL,
	[MaxQuantity] [int] NOT NULL,
	[NameParameter] [varchar](50) NULL,
	[Parameter] [varchar](50) NULL,
	[Id_Warranty] [int] NOT NULL,
	[WarrantyDays] [int] NOT NULL,
	[Id_Brand] [int] NOT NULL,
	[Id_Category] [int] NOT NULL,
	[Image] [image] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id_Product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warranty]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warranty](
	[Id_Warranty] [int] NOT NULL,
	[TypeWarranty] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id_Warranty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VAT]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VAT](
	[Id_Vat] [int] NOT NULL,
	[Vat_rate] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_VAT] PRIMARY KEY CLUSTERED 
(
	[Id_Vat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Condition]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Condition](
	[Id_Condition] [int] NOT NULL,
	[NameCondition] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Condition] PRIMARY KEY CLUSTERED 
(
	[Id_Condition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[Id_Brand] [int] NOT NULL,
	[NameBrand] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[Id_Brand] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id_Category] [int] NOT NULL,
	[NameCategory] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id_Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RetailSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetailSales](
	[Id_RetailSales] [int] NOT NULL,
	[Id_Product] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[DateStartSales] [date] NOT NULL,
	[DateClosing] [date] NOT NULL,
	[DateClosed] [date] NULL,
	[DayReturn] [int] NOT NULL,
	[DayDelivery] [int] NOT NULL,
	[Id_Delivery] [int] NOT NULL,
	[Visitors] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_RetailSales] PRIMARY KEY CLUSTERED 
(
	[Id_RetailSales] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewUsersSalers]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUsersSalers]
AS
SELECT        dbo.RetailSales.Id_RetailSales, dbo.RetailSales.Quantity, dbo.RetailSales.DateStartSales, dbo.RetailSales.DateClosing, dbo.RetailSales.DateClosed, dbo.RetailSales.DayDelivery, dbo.RetailSales.DayReturn, 
                         dbo.RetailSales.Visitors, dbo.RetailSales.Status, dbo.Delivery.NameDelivery, dbo.RetailSales.Id_Product, dbo.Product.Id_Seller, dbo.Product.Name, dbo.Product.Description, dbo.Product.Price, dbo.VAT.Vat_rate, 
                         dbo.Condition.NameCondition, dbo.Product.MaxQuantity, dbo.Product.NameParameter, dbo.Product.Parameter, dbo.Warranty.TypeWarranty, dbo.Product.WarrantyDays, dbo.Brand.NameBrand, dbo.Category.NameCategory, 
                         dbo.Product.Image, dbo.Product.Status AS StatusProduct
FROM            dbo.RetailSales INNER JOIN
                         dbo.Delivery ON dbo.RetailSales.Id_Delivery = dbo.Delivery.Id_Delivery INNER JOIN
                         dbo.Product ON dbo.RetailSales.Id_Product = dbo.Product.Id_Product INNER JOIN
                         dbo.Brand ON dbo.Product.Id_Brand = dbo.Brand.Id_Brand INNER JOIN
                         dbo.Category ON dbo.Product.Id_Category = dbo.Category.Id_Category INNER JOIN
                         dbo.Condition ON dbo.Product.Id_Condition = dbo.Condition.Id_Condition INNER JOIN
                         dbo.VAT ON dbo.Product.Id_Vat = dbo.VAT.Id_Vat INNER JOIN
                         dbo.Warranty ON dbo.Product.Id_Warranty = dbo.Warranty.Id_Warranty
GO
/****** Object:  View [dbo].[ViewAvailableQuantity]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewAvailableQuantity]
AS
SELECT        dbo.RetailSales.Id_RetailSales, dbo.Product.Id_Product, dbo.RetailSales.Quantity
FROM            dbo.RetailSales INNER JOIN
                         dbo.Product ON dbo.RetailSales.Id_Product = dbo.Product.Id_Product
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[Id_Comment] [int] NOT NULL,
	[Id_Product] [int] NOT NULL,
 CONSTRAINT [PK_Rainting] PRIMARY KEY CLUSTERED 
(
	[Id_Comment] ASC,
	[Id_Product] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id_Comment] [int] NOT NULL,
	[Comment] [varchar](100) NOT NULL,
	[Stars] [smallint] NOT NULL,
	[Id_User] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id_Comment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewRating]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewRating]
AS
SELECT        dbo.Rating.Id_Product, dbo.Rating.Id_Comment, dbo.Comment.Id_User, dbo.Comment.Comment, dbo.Comment.Status, dbo.Comment.Stars
FROM            dbo.Rating INNER JOIN
                         dbo.Comment ON dbo.Rating.Id_Comment = dbo.Comment.Id_Comment INNER JOIN
                         dbo.Product ON dbo.Rating.Id_Product = dbo.Product.Id_Product
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id_User] [int] NOT NULL,
	[IsActive] [smallint] NOT NULL,
	[Nick] [varchar](50) NOT NULL,
	[HashPassword] [char](64) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Surname] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [bigint] NOT NULL,
	[Adress] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[CountVisitors] [int] NULL,
	[Id_Company] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UsersSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UsersSales]
AS
SELECT        dbo.Users.Nick, dbo.Product.Name AS NameProduct, dbo.RetailSales.Id_RetailSales AS NumberSales, dbo.RetailSales.Status AS StatusSales
FROM            dbo.Product INNER JOIN
                         dbo.RetailSales ON dbo.Product.Id_Product = dbo.RetailSales.Id_Product INNER JOIN
                         dbo.Users ON dbo.Product.Id_Seller = dbo.Users.Id_User
GO
/****** Object:  View [dbo].[ProductsSallers]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductsSallers]
AS
SELECT        dbo.Product.Name, dbo.Product.Description, dbo.Product.Price, dbo.VAT.Vat_rate, dbo.Product.Id_Product
FROM            dbo.Product INNER JOIN
                         dbo.VAT ON dbo.Product.Id_Vat = dbo.VAT.Id_Vat
GO
/****** Object:  View [dbo].[UsersProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UsersProduct]
AS
SELECT        dbo.Product.Id_Product, dbo.Product.Name, dbo.Product.Description, dbo.Product.Price, dbo.Product.MaxQuantity, dbo.Product.NameParameter, dbo.Product.Parameter, dbo.Product.Image, dbo.Product.Status, 
                         dbo.Warranty.TypeWarranty, dbo.Product.WarrantyDays, dbo.Brand.NameBrand, dbo.Category.NameCategory, dbo.VAT.Vat_rate, dbo.Users.Id_User, dbo.Condition.NameCondition
FROM            dbo.Product INNER JOIN
                         dbo.Users ON dbo.Product.Id_Seller = dbo.Users.Id_User INNER JOIN
                         dbo.VAT ON dbo.Product.Id_Vat = dbo.VAT.Id_Vat INNER JOIN
                         dbo.Warranty ON dbo.Product.Id_Warranty = dbo.Warranty.Id_Warranty INNER JOIN
                         dbo.Brand ON dbo.Product.Id_Brand = dbo.Brand.Id_Brand INNER JOIN
                         dbo.Category ON dbo.Product.Id_Category = dbo.Category.Id_Category INNER JOIN
                         dbo.Condition ON dbo.Product.Id_Condition = dbo.Condition.Id_Condition
GO
/****** Object:  View [dbo].[ProductsViews]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductsViews]
AS
SELECT        dbo.Product.Name, dbo.Product.Price, dbo.Product.Description, dbo.Brand.NameBrand
FROM            dbo.Product INNER JOIN
                         dbo.Brand ON dbo.Product.Id_Brand = dbo.Brand.Id_Brand
GO
/****** Object:  View [dbo].[ViewCommentRating]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewCommentRating]
AS
SELECT        dbo.Rating.Id_Comment, dbo.Rating.Id_Product, dbo.Comment.Comment, dbo.Comment.Stars, dbo.Comment.Id_User, dbo.Comment.Status
FROM            dbo.Rating INNER JOIN
                         dbo.Comment ON dbo.Rating.Id_Comment = dbo.Comment.Id_Comment
GO
/****** Object:  Table [dbo].[UsersPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersPayment](
	[Id_User] [int] NOT NULL,
	[Id_Payment] [int] NOT NULL,
 CONSTRAINT [PK_UsersPayment] PRIMARY KEY CLUSTERED 
(
	[Id_User] ASC,
	[Id_Payment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypePayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypePayment](
	[Id_TypePayment] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LimitString] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TypePayment] PRIMARY KEY CLUSTERED 
(
	[Id_TypePayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id_Payment] [int] NOT NULL,
	[Id_TypePayment] [int] NOT NULL,
	[PaymentString] [varchar](30) NOT NULL,
	[NameBank] [varchar](50) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id_Payment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewUsersPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUsersPayment]
AS
SELECT        dbo.Users.Id_User, dbo.Payment.Id_Payment, dbo.Payment.Id_TypePayment, dbo.Payment.PaymentString, dbo.Payment.NameBank, dbo.Payment.Status, dbo.TypePayment.Name AS TypePayment, 
                         dbo.TypePayment.LimitString, dbo.TypePayment.Status AS StatusTypePayment
FROM            dbo.UsersPayment INNER JOIN
                         dbo.Users ON dbo.UsersPayment.Id_User = dbo.Users.Id_User INNER JOIN
                         dbo.Payment ON dbo.UsersPayment.Id_Payment = dbo.Payment.Id_Payment INNER JOIN
                         dbo.TypePayment ON dbo.Payment.Id_TypePayment = dbo.TypePayment.Id_TypePayment
GO
/****** Object:  Table [dbo].[ProductsBuyed]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsBuyed](
	[Id_ListProductsBuyed] [int] NOT NULL,
	[Id_RetailSalers] [int] NOT NULL,
	[Id_Transation] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ProductsBuyed] PRIMARY KEY CLUSTERED 
(
	[Id_ListProductsBuyed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewActualQuantity]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewActualQuantity]
AS
SELECT        dbo.ProductsBuyed.Id_ListProductsBuyed, dbo.ProductsBuyed.Id_RetailSalers, dbo.ProductsBuyed.Quantity, dbo.RetailSales.Quantity AS QuantitySales
FROM            dbo.ProductsBuyed INNER JOIN
                         dbo.RetailSales ON dbo.ProductsBuyed.Id_RetailSalers = dbo.RetailSales.Id_RetailSales
GO
/****** Object:  UserDefinedFunction [dbo].[HelpToQuantity]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[HelpToQuantity](@valueId int)

RETURNS TABLE 
AS
RETURN 
(
	SELECT Quantity FROM ViewActualQuantity WHERE Id_RetailSalers=@valueId
)
GO
/****** Object:  Table [dbo].[TypeUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeUser](
	[Id_TypeUser] [int] NOT NULL,
	[NameTypeUser] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_TypeUser] PRIMARY KEY CLUSTERED 
(
	[Id_TypeUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPermission]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPermission](
	[Id_TypeUser] [int] NOT NULL,
	[Id_User] [int] NOT NULL,
 CONSTRAINT [PK_UserPermission] PRIMARY KEY CLUSTERED 
(
	[Id_User] ASC,
	[Id_TypeUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewUserPermision]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUserPermision]
AS
SELECT        dbo.TypeUser.Id_TypeUser, dbo.Users.Id_User, dbo.Users.Nick, dbo.TypeUser.NameTypeUser
FROM            dbo.TypeUser INNER JOIN
                         dbo.UserPermission ON dbo.TypeUser.Id_TypeUser = dbo.UserPermission.Id_TypeUser INNER JOIN
                         dbo.Users ON dbo.UserPermission.Id_User = dbo.Users.Id_User
GO
/****** Object:  View [dbo].[UsersMethodsPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UsersMethodsPayment]
AS
SELECT        dbo.Users.Nick, dbo.TypePayment.Name AS TypePayment, dbo.Payment.PaymentString, dbo.Payment.NameBank
FROM            dbo.Users INNER JOIN
                         dbo.UsersPayment ON dbo.Users.Id_User = dbo.UsersPayment.Id_User INNER JOIN
                         dbo.Payment ON dbo.UsersPayment.Id_Payment = dbo.Payment.Id_Payment INNER JOIN
                         dbo.TypePayment ON dbo.Payment.Id_TypePayment = dbo.TypePayment.Id_TypePayment
GO
/****** Object:  View [dbo].[ProductRetailSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductRetailSales]
AS
SELECT        dbo.RetailSales.Quantity, dbo.RetailSales.DateClosing, dbo.RetailSales.DateStartSales, dbo.RetailSales.DateClosed, dbo.RetailSales.DayReturn, dbo.RetailSales.DayDelivery, dbo.RetailSales.Visitors, 
                         dbo.RetailSales.Status AS RetailStatus, dbo.Delivery.NameDelivery, dbo.Product.Name, dbo.Product.Description, dbo.Product.Price, dbo.Product.NameParameter, dbo.Product.MaxQuantity, dbo.Product.Parameter, 
                         dbo.Product.WarrantyDays, dbo.Product.Image, dbo.Product.Status AS StatusProduct, dbo.Category.NameCategory, dbo.Condition.Condition, dbo.VAT.Vat_rate, dbo.Warranty.TypeWarranty
FROM            dbo.RetailSales INNER JOIN
                         dbo.Product ON dbo.RetailSales.Id_Product = dbo.Product.Id_Product INNER JOIN
                         dbo.Delivery ON dbo.RetailSales.Id_Delivery = dbo.Delivery.Id_Delivery INNER JOIN
                         dbo.Brand ON dbo.Product.Id_Brand = dbo.Brand.Id_Brand INNER JOIN
                         dbo.Category ON dbo.Product.Id_Category = dbo.Category.Id_Category INNER JOIN
                         dbo.Condition ON dbo.Product.Id_Condition = dbo.Condition.Id_Condition INNER JOIN
                         dbo.VAT ON dbo.Product.Id_Vat = dbo.VAT.Id_Vat INNER JOIN
                         dbo.Warranty ON dbo.Product.Id_Warranty = dbo.Warranty.Id_Warranty
GO
/****** Object:  Table [dbo].[Transation]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transation](
	[Id_Transation] [int] NOT NULL,
	[Id_Payment] [int] NOT NULL,
	[Id_User] [int] NOT NULL,
	[Date_Transation] [date] NOT NULL,
	[SumPay] [money] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Transation] PRIMARY KEY CLUSTERED 
(
	[Id_Transation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id_Invoice] [int] NOT NULL,
	[Id_Transation] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Date_Of_Make_Invoice] [date] NOT NULL,
	[Date_Of_Pay] [date] NOT NULL,
	[Date_Of_Service] [date] NOT NULL,
	[Code_Invoice] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UserBuyProducts]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserBuyProducts]
AS
SELECT        dbo.Transation.Id_Transation, dbo.Transation.Id_Payment, dbo.Transation.Id_User, dbo.Transation.SumPay, dbo.Transation.Status AS StatusTransation, dbo.ProductsBuyed.Id_ListProductsBuyed, 
                         dbo.ProductsBuyed.Id_RetailSalers, dbo.ProductsBuyed.Quantity AS QuantityBuyed, dbo.Invoice.Id_Invoice, dbo.Invoice.Date_Of_Make_Invoice, dbo.Invoice.Date_Of_Pay, dbo.Invoice.Date_Of_Service, 
                         dbo.Invoice.Code_Invoice, dbo.Transation.Date_Transation, dbo.Invoice.Status AS StatusInvoice, dbo.Product.Id_Seller, dbo.Product.Id_Product, dbo.Product.Name, dbo.Product.Price, dbo.VAT.Vat_rate
FROM            dbo.Transation INNER JOIN
                         dbo.ProductsBuyed ON dbo.Transation.Id_Transation = dbo.ProductsBuyed.Id_Transation INNER JOIN
                         dbo.Invoice ON dbo.Transation.Id_Transation = dbo.Invoice.Id_Transation INNER JOIN
                         dbo.RetailSales ON dbo.ProductsBuyed.Id_RetailSalers = dbo.RetailSales.Id_RetailSales INNER JOIN
                         dbo.Product ON dbo.RetailSales.Id_Product = dbo.Product.Id_Product INNER JOIN
                         dbo.VAT ON dbo.Product.Id_Vat = dbo.VAT.Id_Vat
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id_Company] [int] NOT NULL,
	[NameCompany] [varchar](32) NOT NULL,
	[Adress] [varchar](50) NOT NULL,
	[City] [varchar](20) NOT NULL,
	[Phone] [bigint] NOT NULL,
	[Email] [varchar](40) NOT NULL,
	[NIP] [varchar](10) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id_Company] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UserCompany]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserCompany]
AS
SELECT        dbo.Users.Nick, dbo.Users.Name, dbo.Users.Surname, dbo.Users.Email AS UserEmail, dbo.Users.Phone AS UserPhone, dbo.Users.Adress AS UserAdress, dbo.Users.City AS UserCity, dbo.Users.CountVisitors, 
                         dbo.Company.NameCompany, dbo.Company.Adress AS CompanyAdress, dbo.Company.City AS CompanyCity, dbo.Company.Phone AS CompanyPhone, dbo.Company.Email AS CompanyEmail, dbo.Company.NIP, 
                         dbo.Users.Id_User, dbo.Users.Id_Company
FROM            dbo.Users INNER JOIN
                         dbo.Company ON dbo.Users.Id_Company = dbo.Company.Id_Company
GO
/****** Object:  UserDefinedFunction [dbo].[ListVat]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[ListVat]()
RETURNS TABLE 
AS
RETURN (SELECT Vat_rate FROM VAT)
GO
/****** Object:  View [dbo].[ViewUsersTransation]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUsersTransation]
AS
SELECT        dbo.Transation.Id_Transation, dbo.Transation.Id_Payment, dbo.Transation.Id_User, dbo.Transation.Date_Transation, dbo.Transation.SumPay, dbo.Invoice.Id_Invoice, dbo.Invoice.Date_Of_Make_Invoice, 
                         dbo.Invoice.Date_Of_Pay, dbo.Invoice.Date_Of_Service, dbo.Invoice.Code_Invoice, dbo.Transation.Status AS StatusTransation, dbo.Invoice.Status AS StatusInvoice, dbo.ProductsBuyed.Quantity, 
                         dbo.ProductsBuyed.Id_RetailSalers
FROM            dbo.Invoice INNER JOIN
                         dbo.Transation ON dbo.Invoice.Id_Transation = dbo.Transation.Id_Transation INNER JOIN
                         dbo.ProductsBuyed ON dbo.Transation.Id_Transation = dbo.ProductsBuyed.Id_Transation INNER JOIN
                         dbo.RetailSales ON dbo.ProductsBuyed.Id_RetailSalers = dbo.RetailSales.Id_RetailSales
GO
/****** Object:  View [dbo].[UsersView]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UsersView]
AS
SELECT        Id_User, Name, Surname
FROM            dbo.[User]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Users] FOREIGN KEY([Id_User])
REFERENCES [dbo].[Users] ([Id_User])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Users]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Transation] FOREIGN KEY([Id_Transation])
REFERENCES [dbo].[Transation] ([Id_Transation])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Transation]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_TypePayment] FOREIGN KEY([Id_TypePayment])
REFERENCES [dbo].[TypePayment] ([Id_TypePayment])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_TypePayment]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand] FOREIGN KEY([Id_Brand])
REFERENCES [dbo].[Brand] ([Id_Brand])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([Id_Category])
REFERENCES [dbo].[Category] ([Id_Category])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Condition] FOREIGN KEY([Id_Condition])
REFERENCES [dbo].[Condition] ([Id_Condition])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Condition]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Users] FOREIGN KEY([Id_Seller])
REFERENCES [dbo].[Users] ([Id_User])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Users]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_VAT] FOREIGN KEY([Id_Vat])
REFERENCES [dbo].[VAT] ([Id_Vat])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_VAT]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Warranty] FOREIGN KEY([Id_Warranty])
REFERENCES [dbo].[Warranty] ([Id_Warranty])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Warranty]
GO
ALTER TABLE [dbo].[ProductsBuyed]  WITH CHECK ADD  CONSTRAINT [FK_ProductsBuyed_RetailSales] FOREIGN KEY([Id_RetailSalers])
REFERENCES [dbo].[RetailSales] ([Id_RetailSales])
GO
ALTER TABLE [dbo].[ProductsBuyed] CHECK CONSTRAINT [FK_ProductsBuyed_RetailSales]
GO
ALTER TABLE [dbo].[ProductsBuyed]  WITH CHECK ADD  CONSTRAINT [FK_ProductsBuyed_Transation] FOREIGN KEY([Id_Transation])
REFERENCES [dbo].[Transation] ([Id_Transation])
GO
ALTER TABLE [dbo].[ProductsBuyed] CHECK CONSTRAINT [FK_ProductsBuyed_Transation]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Comment] FOREIGN KEY([Id_Comment])
REFERENCES [dbo].[Comment] ([Id_Comment])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Comment]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Product] FOREIGN KEY([Id_Product])
REFERENCES [dbo].[Product] ([Id_Product])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Product]
GO
ALTER TABLE [dbo].[RetailSales]  WITH CHECK ADD  CONSTRAINT [FK_RetailSales_Delivery] FOREIGN KEY([Id_Delivery])
REFERENCES [dbo].[Delivery] ([Id_Delivery])
GO
ALTER TABLE [dbo].[RetailSales] CHECK CONSTRAINT [FK_RetailSales_Delivery]
GO
ALTER TABLE [dbo].[RetailSales]  WITH CHECK ADD  CONSTRAINT [FK_RetailSales_Product] FOREIGN KEY([Id_Product])
REFERENCES [dbo].[Product] ([Id_Product])
GO
ALTER TABLE [dbo].[RetailSales] CHECK CONSTRAINT [FK_RetailSales_Product]
GO
ALTER TABLE [dbo].[Transation]  WITH CHECK ADD  CONSTRAINT [FK_Id_Transation_Payment] FOREIGN KEY([Id_Payment])
REFERENCES [dbo].[Payment] ([Id_Payment])
GO
ALTER TABLE [dbo].[Transation] CHECK CONSTRAINT [FK_Id_Transation_Payment]
GO
ALTER TABLE [dbo].[Transation]  WITH CHECK ADD  CONSTRAINT [FK_Transation_Users] FOREIGN KEY([Id_User])
REFERENCES [dbo].[Users] ([Id_User])
GO
ALTER TABLE [dbo].[Transation] CHECK CONSTRAINT [FK_Transation_Users]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_TypeUser] FOREIGN KEY([Id_TypeUser])
REFERENCES [dbo].[TypeUser] ([Id_TypeUser])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_TypeUser]
GO
ALTER TABLE [dbo].[UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserPermission_Users] FOREIGN KEY([Id_User])
REFERENCES [dbo].[Users] ([Id_User])
GO
ALTER TABLE [dbo].[UserPermission] CHECK CONSTRAINT [FK_UserPermission_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Id_Company] FOREIGN KEY([Id_Company])
REFERENCES [dbo].[Company] ([Id_Company])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Id_Company]
GO
ALTER TABLE [dbo].[UsersPayment]  WITH CHECK ADD  CONSTRAINT [FK_UsersPayment_Payment] FOREIGN KEY([Id_Payment])
REFERENCES [dbo].[Payment] ([Id_Payment])
GO
ALTER TABLE [dbo].[UsersPayment] CHECK CONSTRAINT [FK_UsersPayment_Payment]
GO
ALTER TABLE [dbo].[UsersPayment]  WITH CHECK ADD  CONSTRAINT [FK_UsersPayment_Users] FOREIGN KEY([Id_User])
REFERENCES [dbo].[Users] ([Id_User])
GO
ALTER TABLE [dbo].[UsersPayment] CHECK CONSTRAINT [FK_UsersPayment_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddBrand]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddBrand](@valueName as varchar(20)) 
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @valueSame as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Brand)FROM Brand
	WHILE @tempNumber!=@Number
	BEGIN
		IF(SELECT NameBrand FROM Brand WHERE Id_Brand=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END

		IF not exists(SELECT NameBrand FROM Brand WHERE Id_Brand=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END
	
	IF @valueSame=0
	BEGIN
		INSERT INTO Brand(Id_Brand,NameBrand,Status) VALUES (@Number,@valueName,1);
	END;
END
GO
/****** Object:  StoredProcedure [dbo].[AddCategory]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCategory](@valueName varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @Number as int;
	DECLARE @ValueSame as int=0;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Category)FROM Category
	
	WHILE @tempNumber!=@Number
	BEGIN
		IF (SELECT NameCategory FROM Category WHERE Id_Category=@tempNumber)=@valueName
	BEGIN 

		IF not exists(SELECT NameCategory FROM Category WHERE Id_Category=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END

		SELECT @ValueSame=1;
	END

	SELECT @tempNumber=@tempNumber+1;
	END

	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

    IF @ValueSame=0
	BEGIN
	INSERT INTO Category(Id_Category,NameCategory,Status) VALUES (@Number,@valueName,1);
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddComment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddComment](@valueComment varchar(100),@valueStars smallint,@valueIdUser int,@valueIdProduct int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Comment)FROM Comment
	WHILE @tempNumber!=@Number
	BEGIN
		IF not exists(SELECT Id_Comment FROM Comment WHERE Id_Comment=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	INSERT INTO Comment(Id_Comment,Comment,Stars,Id_User,Status) VALUES (@Number,@valueComment,@valueStars,@valueIdUser,1)
	INSERT INTO Rating(Id_Comment,Id_Product) VALUES(@Number,@valueIdProduct)
END
GO
/****** Object:  StoredProcedure [dbo].[AddCompany]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCompany](@valueName as varchar(20),@valueAdress AS VARCHAR(50),@valueCity AS VARCHAR(20),
									@valuePhone as bigint,@valueEmail AS varchar(40), @valueNip AS VARCHAR(10),@valueUser AS int) 
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @valueSame as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Company)FROM Company
	WHILE @tempNumber!=@Number
	BEGIN
		IF(SELECT NIP FROM Company WHERE Id_Company=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END

		IF not exists(SELECT NIP FROM Company WHERE Id_Company=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END
	
	IF @valueSame=0
	BEGIN
		INSERT INTO Company(Id_Company,NameCompany,Adress,City,Phone,Email,NIP,Status) VALUES 
						(@Number,@valueName,@valueAdress,@valueCity,@valuePhone,@valueEmail,@valueNip,1);
		UPDATE Users SET Id_Company=@Number WHERE Id_User=@valueUser;
	END;
END
GO
/****** Object:  StoredProcedure [dbo].[AddCondition]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCondition](@valueName as varchar(20)) 	
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS int=0;
	DECLARE @number AS int;
	DECLARE @tempNumber AS int=0;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @number=COUNT(Id_Condition)FROM Condition

	WHILE @tempNumber!=@number
	BEGIN

		IF(SELECT NameCondition FROM Condition WHERE Id_Condition=@tempNumber)=@valueName
		BEGIN
			SELECT @valueSame=1;
		END

		IF not exists(SELECT NameCondition FROM Condition WHERE Id_Condition=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END

		SELECT @tempNumber=@tempNumber+1;

	END

	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	IF @valueSame=0
	BEGIN
		INSERT INTO Condition(Id_Condition,NameCondition,Status) VALUES (@number,@valueName,1);
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddDelivery]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddDelivery](@valueName as varchar(20)) 
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @valueSame as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Delivery)FROM Delivery
	WHILE @tempNumber!=@Number
	BEGIN
		IF(SELECT NameDelivery FROM Delivery WHERE Id_Delivery=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END

		IF not exists(SELECT NameDelivery FROM Delivery WHERE Id_Delivery=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END
	
	IF @valueSame=0
	BEGIN
		INSERT INTO Delivery(Id_Delivery,NameDelivery,Status) VALUES (@Number,@valueName,1);
	END;
END
GO
/****** Object:  StoredProcedure [dbo].[AddInvoice]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddInvoice](@valueIdTransation int,@valueDateOfMakeInvoice Date,@valueDateOfPay Date,@valueDateOfService Date,@valueCodeInvoice int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Invoice)FROM Invoice
	WHILE @tempNumber!=@Number
	BEGIN
		IF not exists(SELECT Id_Invoice FROM Invoice WHERE Id_Invoice=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	INSERT INTO Invoice(Id_Invoice,Id_Transation,Status,Date_Of_Make_Invoice,Date_Of_Pay,Date_Of_Service,Code_Invoice) VALUES (@Number,@valueIdTransation,1,@valueDateOfMakeInvoice,@valueDateOfPay,@valueDateOfService,@valueCodeInvoice);
END
GO
/****** Object:  StoredProcedure [dbo].[AddPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddPayment](@valueIdUser int,@valueNameType varchar(50),@valueString varchar(30),@valueName varchar(50)) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Number AS int;
	DECLARE @TempNumber AS int=0;
	DECLARE @valueSame AS INT=0;
	DECLARE @CurrentNumber as int=(-1);
	DECLARE @valueIdType int;
	SELECT @Number=COUNT(Id_Payment)FROM Payment;
	WHILE @TempNumber!=@Number
	BEGIN
		IF not exists(SELECT PaymentString FROM Payment WHERE Id_Payment=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @TempNumber=@TempNumber+1;
	END

	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	SELECT @valueIdType= Id_TypePayment FROM TypePayment WHERE Name=@valueNameType

    IF(@valueSame=0)
	BEGIN
		INSERT INTO Payment(Id_Payment,Id_TypePayment,PaymentString,NameBank,Status) VALUES(@Number,@valueIdType,@valueString,@valueName,1)
		INSERT INTO UsersPayment(Id_Payment,Id_User) VALUES(@Number,@valueIdUser)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddProduct](@valueSeller int,@valueName varchar(25),
									@valueDescription varchar(100),@valuePrice money,
									@valueVat int,@valueCondition int, @valueMaxQuantity int,
									@valueNameParameter varchar(50),@valueParameter varchar(50),
									@valueWarranty int, @valueWarrantyDays int,@valueBrand int,
									@valueCategory int,@valueImage image)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Number as int;
	SELECT @Number=COUNT(Id_Product)FROM Product
	
	INSERT INTO Product(Id_Product,Id_Seller,Name,Description,Price,Id_Vat,
						Id_Condition,MaxQuantity,NameParameter,Parameter,
						Id_Warranty,WarrantyDays,Id_Brand,Id_Category,Image,Status) 
	VALUES (@Number,@valueSeller,@valueName,@valueDescription, @valuePrice,
			@valueVat,@valueCondition,@valueMaxQuantity,@valueNameParameter,
			@valueParameter,@valueWarranty, @valueWarrantyDays,@valueBrand,
			@valueCategory,@valueImage,1);
END
GO
/****** Object:  StoredProcedure [dbo].[AddProductsBuyed]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddProductsBuyed](@valueIdRetailSalers int,@valueIdTransation int,@valueQuantity int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_ListProductsBuyed)FROM ProductsBuyed
	WHILE @tempNumber!=@Number
	BEGIN
		IF not exists(SELECT Id_RetailSalers FROM ProductsBuyed WHERE Id_ListProductsBuyed=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	INSERT INTO ProductsBuyed(Id_ListProductsBuyed,Id_RetailSalers,Id_Transation,Quantity,Status) VALUES (@Number,@valueIdRetailSalers,@valueIdTransation,@valueQuantity,1);
END
GO
/****** Object:  StoredProcedure [dbo].[AddRetailSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddRetailSales](@valueIdProduct int,@valueQuantity int,@valueDateStartSales Date,
									@valueDateClosing Date,@valueDayReturn int, @valueDayDelivery int,
									@valueNameDelivery varchar(20)) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	DECLARE @valueIdDelivery int;
	SELECT @Number=COUNT(Id_RetailSales)FROM RetailSales
	WHILE @tempNumber!=@Number
	BEGIN
		IF not exists(SELECT Id_RetailSales FROM RetailSales WHERE Id_RetailSales=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	SELECT @valueIdDelivery= Id_Delivery FROM Delivery WHERE NameDelivery=@valueNameDelivery 

	INSERT INTO RetailSales(Id_RetailSales,Id_Product,Quantity,DateStartSales,DateClosing,DayReturn,DayDelivery,
				Id_Delivery,Visitors,Status) VALUES (@Number,@valueIdProduct,@valueQuantity,@valueDateStartSales,@valueDateClosing,
				@valueDayReturn,@valueDayDelivery,@valueIdDelivery,0,1);
END
GO
/****** Object:  StoredProcedure [dbo].[AddTransation]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddTransation](@valuePayment int,@valueIdUser int,@valueDateTransation Date,@valueSumPay money
									 ,@valueIdRetailSalers int,@valueQuantity int,@valueIsInvoice int, @valueDateOfMakeInvoice Date,
									 @valueDateOfPay Date,@valueDateOfService Date, @valueCodeInvoice int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Transation)FROM Transation
	WHILE @tempNumber!=@Number
	BEGIN
		IF not exists(SELECT Id_Transation FROM Transation WHERE Id_Transation=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	INSERT INTO Transation(Id_Transation,Id_Payment,Id_User,Date_Transation,SumPay,Status) 
	VALUES (@Number,@valuePayment,@valueIdUser,@valueDateTransation,@valueSumPay,2);

	DECLARE  @valueListProductsBuyed int
	SELECT  @valueListProductsBuyed=COUNT(Id_ListProductsBuyed) FROM ProductsBuyed

	INSERT INTO ProductsBuyed(Id_ListProductsBuyed,Id_RetailSalers,Id_Transation,Quantity,Status) 
	VALUES (@valueListProductsBuyed,@valueIdRetailSalers,@Number,@valueQuantity,1);

	IF(@valueIsInvoice=1)
	BEGIN
		DECLARE @valueInvoice int
		SELECT @valueInvoice=COUNT(Id_Invoice)FROM Invoice
		INSERT INTO Invoice(Id_Invoice,Id_Transation,Status,Date_Of_Make_Invoice,Date_Of_Pay,Date_Of_Service,Code_Invoice)
		VALUES (@valueInvoice,@Number,1,@valueDateOfMakeInvoice,@valueDateOfPay,@valueDateOfService,@valueCodeInvoice);
	END

END
GO
/****** Object:  StoredProcedure [dbo].[AddTypePayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddTypePayment](@valueName varchar(50),@value int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Number AS int;
	DECLARE @TempNumber AS int=0;
	DECLARE @valueSame AS INT=0;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_TypePayment)FROM TypePayment;
	WHILE @TempNumber!=@Number
	BEGIN
		IF(SELECT Name FROM TypePayment WHERE Id_TypePayment=@TempNumber)=@valueName
		BEGIN
			SELECT @valueSame=1;
		END

		IF not exists(SELECT Name FROM TypePayment WHERE Id_TypePayment=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END

		SELECT @TempNumber=@TempNumber+1;
	END

	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

    IF(@valueSame=0)
	BEGIN
		INSERT INTO TypePayment(Id_TypePayment,Name,LimitString,Status) VALUES(@Number,@valueName,@value,1)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddTypeUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddTypeUser](@valueName as varchar(20)) 
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0;
	DECLARE @valueSame as int=0;
	DECLARE @Number as int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_TypeUser)FROM TypeUser
	WHILE @tempNumber!=@Number
	BEGIN
		IF(SELECT NameTypeUser FROM TypeUser WHERE Id_TypeUser=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END

		IF not exists(SELECT NameTypeUser FROM TypeUser WHERE Id_TypeUser=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
    
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END
	
	IF @valueSame=0
	BEGIN
		INSERT INTO TypeUser(Id_TypeUser,NameTypeUser,Status) VALUES (@Number,@valueName,1);
	END;
END
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddUser](@valueNick AS varchar(50),@valueHash AS char(64),@valueName AS varchar(50),@valueSurname AS varchar(50), @valueEmail AS varchar(50), 
						 @numberPhone AS int,@valueAdress AS varchar(50),@valueCity AS varchar(50))
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0, @ValueSame as int=0,@Number as int,@tempNumberTypeUser AS int;
	SELECT @Number=COUNT(Id_User)FROM Users
	SELECT @tempNumberTypeUser=(SELECT Id_TypeUser FROM TypeUser WHERE NameTypeUser='Użytkownik'); 
	WHILE @tempNumber!=@Number
	BEGIN
		IF (SELECT Nick FROM Users WHERE Id_User=@tempNumber)=@valueNick
		BEGIN 
			SELECT @ValueSame=1;
		END

		SELECT @tempNumber=@tempNumber+1;
	END

    IF @ValueSame=0
	BEGIN
		INSERT INTO Users(Id_User,IsActive,Nick,HashPassword,Name,Surname,Email,Phone,Adress,City,CountVisitors) 
		VALUES(@Number,1,@valueNick,@valueHash,@valueName,@valueSurname,@valueEmail,@numberPhone,@valueAdress,@valueCity,0)

		INSERT INTO UserPermission(Id_User,Id_TypeUser) VALUES(@Number,@tempNumberTypeUser)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserPermission]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddUserPermission](@valueNick varchar(50),@valueNameTypeUser varchar(20))
	
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueIdTypeUser int, @valueIdUser int
	DECLARE @tempNumber AS int=0,@number int,@valueSame as int=0;
	SELECT @number=Count(Id_TypeUser)FROM UserPermission

	SELECT @valueIdTypeUser=Id_TypeUser FROM TypeUser WHERE NameTypeUser=@valueNameTypeUser
	SELECT @valueIdUser= Id_User FROM Users WHERE Nick=@valueNick
	
	WHILE @tempNumber!=@number
	BEGIN 
		IF EXISTS (SELECT * FROM UserPermission WHERE Id_TypeUser=@valueIdTypeUser and Id_User=@valueIdUser)  
		BEGIN
			SELECT @valueSame=1
		END

		SELECT @tempNumber=@tempNumber+1
	END
	

	IF (@valueSame=0)
	BEGIN
	INSERT INTO UserPermission(Id_TypeUser,Id_User) VALUES(@valueIdTypeUser,@valueIdUser)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddUserProduct](@valueSeller int,@valueName varchar(25),
									@valueDescription varchar(100),@valuePrice money,
									@valueVat int,@valueCondition varchar(20), @valueMaxQuantity int,
									@valueNameParameter varchar(50),@valueParameter varchar(50),
									@valueWarranty varchar(20), @valueWarrantyDays int,@valueBrand varchar(20),
									@valueCategory varchar(20),@valueImage image)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Number as int;
	SELECT @Number=COUNT(Id_Product)FROM Product
	DECLARE @valueIdVat int,@valueIdCondition int,@valueIdWarranty int,@valueIdBrand int,@valueIdCategory int
	SELECT @valueIdVat=Id_Vat FROM VAT WHERE Vat_rate=@valueVat 
	SELECT @valueIdCondition=Id_Condition FROM Condition WHERE NameCondition=@valueCondition
	SELECT @valueIdWarranty=Id_Warranty FROM Warranty WHERE TypeWarranty=@valueWarranty
	SELECT @valueIdBrand=Id_Brand FROM Brand WHERE NameBrand=@valueBrand
	SELECT @valueIdCategory=Id_Category FROM Category WHERE NameCategory=@valueCategory

	INSERT INTO Product(Id_Product,Id_Seller,Name,Description,Price,Id_Vat,
						Id_Condition,MaxQuantity,NameParameter,Parameter,
						Id_Warranty,WarrantyDays,Id_Brand,Id_Category,Image,Status) 
	VALUES (@Number,@valueSeller,@valueName,@valueDescription, @valuePrice,
			@valueIdVat,@valueIdCondition,@valueMaxQuantity,@valueNameParameter,
			@valueParameter,@valueIdWarranty, @valueWarrantyDays,@valueIdBrand,
			@valueIdCategory,@valueImage,1);
END
GO
/****** Object:  StoredProcedure [dbo].[AddVat]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddVat](@value int) 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	DECLARE @Number int;
	DECLARE @TempNumber int=0;
	DECLARE @ValueSame int=0;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @Number=COUNT(Id_Vat)FROM VAT;
	WHILE @TempNumber!=@Number  
     BEGIN
		IF (SELECT Vat_rate FROM VAT WHERE Id_Vat=@TempNumber)=@value
		BEGIN
			SELECT @ValueSame=1;
		END;

		IF not exists(SELECT Vat_rate FROM VAT WHERE Id_Vat=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END
		SELECT @TempNumber=@TempNumber+1;
	 END;

	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	IF @ValueSame=0
	BEGIN;
		INSERT INTO VAT(Id_Vat,Vat_rate,Status) VALUES(@Number,@value,1);
	END;

END
GO
/****** Object:  StoredProcedure [dbo].[AddVisitor]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddVisitor](@valueId int,@valueIdProduct int)
AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @tempVisitors int
    SELECT @tempVisitors=  Visitors FROM RetailSales WHERE Id_Product=@valueIdProduct
	 SELECT @tempVisitors= @tempVisitors+1
	IF(NOT exists (SELECT * FROM Product WHERE Id_Product=@valueIdProduct and Id_Seller=@valueId))
	BEGIN
		Update RetailSales Set Visitors= @tempVisitors  WHERE Id_Product=@valueIdProduct
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddWarranty]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddWarranty](@valueName AS varchar(10))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @number AS int;
	DECLARE @CurrentNumber as int=(-1);
	SELECT @number=COUNT(Warranty.Id_Warranty)FROM Warranty;
	DECLARE @TempNumber AS int=0;
	DECLARE @ValueSame AS int =0;
	WHILE @TempNumber!=@Number  
     BEGIN
		 IF (SELECT TypeWarranty FROM Warranty WHERE Id_Warranty=@TempNumber)=@valueName
		 BEGIN
			 SELECT @ValueSame=1;
		 END;

		IF not exists(SELECT TypeWarranty FROM Warranty WHERE Id_Warranty=@tempNumber) and  @CurrentNumber=-1
		BEGIN 
			SELECT @CurrentNumber=@tempNumber;
		END

		SELECT @TempNumber=@TempNumber+1;
	END;
	
	IF @CurrentNumber!=(-1)
	BEGIN
		SELECT @Number=@CurrentNumber;
	END

	IF @ValueSame=0
	BEGIN
	INSERT INTO Warranty(Id_Warranty,TypeWarranty,Status) VALUES (@number,@valueName,1);
	END;

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteBrand]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteBrand](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Brand SET Status=0 WHERE Id_Brand=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCategory](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Category SET Status=0 WHERE Id_Category=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCompany](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Users SET Id_Company=NULL WHERE Id_Company=@valueId 
	DELETE FROM Company WHERE Id_Company=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteCondition]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCondition](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	Update Condition SET Status=0 WHERE Id_Condition=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDelivery]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDelivery](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Delivery SET Status=0 WHERE Id_Delivery=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteProduct](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Product SET Status=0 WHERE Id_Product=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTypePayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteTypePayment](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE TypePayment SET Status=0 WHERE Id_TypePayment=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DeleteUser](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Users SET IsActive=0 WHERE Id_User=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteVat]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteVat](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE VAT SET Status=0 WHERE Id_Vat=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteWarranty]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteWarranty](@valueId AS INT) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Warranty SET Status=0 WHERE Id_Warranty=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Login](@valueNick varchar(50),@valueHash char(64)) 
AS
BEGIN
	SET NOCOUNT ON;
   
	SELECT Id_User,IsActive,Nick,Name,Surname,Email,Phone,Adress,City,
	CountVisitors,Id_Company FROM Users 
	WHERE Nick=@valueNick and HashPassword=@valueHash
END
GO
/****** Object:  StoredProcedure [dbo].[NewPassword]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[NewPassword](@valueNick varchar(50),@valuePassword char(64))
AS
BEGIN

	SET NOCOUNT ON;


	Update Users SET HashPassword=@valuePassword WHERE Nick=@valueNick
END
GO
/****** Object:  StoredProcedure [dbo].[RefreshUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RefreshUser](@valueIdUser int )
AS
BEGIN

	SET NOCOUNT ON;

	SELECT Id_User,IsActive,Nick,Name,Surname,Email,Phone,Adress,City,
	CountVisitors,Id_Company FROM Users 
	WHERE Id_User=@valueIdUser
END
GO
/****** Object:  StoredProcedure [dbo].[StartApp]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[StartApp](@valueToday date)
	
AS
BEGIN
	
	SET NOCOUNT ON;
	Update RetailSales SET DateClosed=DateClosing WHERE DateClosing<@valueToday
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateBrand]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateBrand](@valueId AS INT,@valueName AS varchar(20),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_Brand)FROM Brand;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT NameBrand FROM Brand WHERE Id_Brand=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE Brand SET NameBrand=@valueName WHERE Id_Brand=@valueId
	END
	UPDATE Brand SET Status=@valueStatus WHERE Id_Brand=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCategory](@valueId AS INT,@valueName AS varchar(20),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_Category)FROM Category;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT NameCategory FROM Category WHERE Id_Category=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE Category SET NameCategory=@valueName, Status=@valueStatus WHERE Id_Category=@valueId
	END
	UPDATE Category SET Status=@valueStatus WHERE Id_Category=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateComment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateComment](@valueIdComment INT,@valueComment  varchar(100),@valueStars smallint,@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Comment SET Comment=@valueComment,Stars=@valueStars,Status=@valueStatus WHERE Id_Comment=@valueIdComment

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCompany]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- ============================================
CREATE PROCEDURE [dbo].[UpdateCompany](@valueId int,@valueNameCompany varchar(20),@valueAdress varchar(50),
									@valueCity varchar(20),@valuePhone bigint,@valueEmail varchar(40),
									@valueNIP varchar(10),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Company SET NameCompany=@valueNameCompany,Adress=@valueAdress, City=@valueCity,Phone=@valuePhone,  
	Email=@valueEmail,NIP=@valueNIP,Status=@valueStatus WHERE Id_Company=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCondition]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateCondition](@valueId AS INT,@valueName AS varchar(20),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_Condition)FROM Condition;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT NameCondition FROM Condition WHERE Id_Condition=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE Condition SET NameCondition=@valueName, Status=@valueStatus WHERE Id_Condition=@valueId
	END
	UPDATE Condition SET Status=@valueStatus WHERE Id_Condition=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDelivery]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateDelivery](@valueId AS INT,@valueName AS varchar(20),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_Delivery)FROM Delivery;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT NameDelivery FROM Delivery WHERE Id_Delivery=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE Delivery SET NameDelivery=@valueName WHERE Id_Delivery=@valueId;
	END
	UPDATE Delivery SET Status=@valueStatus WHERE Id_Delivery=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateInvoice]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- ============================================
CREATE PROCEDURE [dbo].[UpdateInvoice](@valueId int,@valueDateOfMakeInvoice Date,
									@valueDateOfPay Date,@valueDateOfService Date,
									@valueCode_Invoice int,@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Invoice SET Date_Of_Make_Invoice=@valueDateOfMakeInvoice,Date_Of_Pay=@valueDateOfPay,  
		Date_Of_Service=@valueDateOfService,Code_Invoice=@valueCode_Invoice,Status=@valueStatus 
		WHERE Id_Invoice=@valueId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePayment](@valueId INT,@valueIdTypePayment INT,@valuePaymentString varchar(30),
									@valueNameBank varchar(50),@valueStatus Int) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Payment SET Id_TypePayment=@valueIdTypePayment,PaymentString=@valuePaymentString,
					NameBank=@valueNameBank, Status=@valueStatus WHERE Id_Payment=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePayment2]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePayment2](@valueId INT,@valueNameTypePayment varchar(50),@valuePaymentString varchar(30),
									@valueNameBank varchar(50),@valueStatus Int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueIdTypePayment int
	SELECT @valueIdTypePayment=Id_TypePayment FROM TypePayment WHERE Name=@valueNameTypePayment
	UPDATE Payment SET Id_TypePayment=@valueIdTypePayment,PaymentString=@valuePaymentString,
					NameBank=@valueNameBank, Status=@valueStatus WHERE Id_Payment=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProduct](@valueId int,@valueSeller int,@valueName varchar(25),
									@valueDescription varchar(100),@valuePrice money,
									@valueVat int,@valueCondition int, @valueMaxQuantity int,
									@valueNameParameter varchar(50),@valueParameter varchar(50),
									@valueWarranty int, @valueWarrantyDays int,@valueBrand int,
									@valueCategory int,@valueImage image,@valueStatus int)
AS
BEGIN
	SET NOCOUNT ON;
	
	Update Product SET Id_Seller=@valueSeller,Name=@valueName,Description=@valueDescription,
						Price=@valuePrice,Id_Vat=@valueVat,Id_Condition=@valueCondition,
						MaxQuantity=@valueMaxQuantity,NameParameter=@valueNameParameter,
						Parameter=@valueParameter,Id_Warranty=@valueWarranty,
						WarrantyDays=@valueWarrantyDays,Id_Brand=@valueBrand,
						Id_Category=@valueCategory,Image=@valueImage,Status=@valueStatus
						WHERE Id_Product=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct2]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProduct2](@valueId int,@valueSeller int,@valueName varchar(25),
									@valueDescription varchar(100),@valuePrice money,
									@valueVat int,@valueCondition varchar(20), @valueMaxQuantity int,
									@valueNameParameter varchar(50),@valueParameter varchar(50),
									@valueWarranty varchar(20), @valueWarrantyDays int,@valueBrand varchar(20),
									@valueCategory varchar(20),@valueImage image,@valueStatus int)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueIdVat int,@valueIdCondition int,@valueIdWarranty int,@valueIdBrand int,@valueIdCategory int
	SELECT @valueIdVat=Id_Vat FROM VAT WHERE Vat_rate=@valueVat 
	SELECT @valueIdCondition=Id_Condition FROM Condition WHERE NameCondition=@valueCondition
	SELECT @valueIdWarranty=Id_Warranty FROM Warranty WHERE TypeWarranty=@valueWarranty
	SELECT @valueIdBrand=Id_Brand FROM Brand WHERE NameBrand=@valueBrand
	SELECT @valueIdCategory=Id_Category FROM Category WHERE NameCategory=@valueCategory


	Update Product SET Id_Seller=@valueSeller,Name=@valueName,Description=@valueDescription,
						Price=@valuePrice,Id_Vat=@valueIdVat,Id_Condition=@valueIdCondition,
						MaxQuantity=@valueMaxQuantity,NameParameter=@valueNameParameter,
						Parameter=@valueParameter,Id_Warranty=@valueIdWarranty,
						WarrantyDays=@valueWarrantyDays,Id_Brand=@valueIdBrand,
						Id_Category=@valueIdCategory,Image=@valueImage,Status=@valueStatus
						WHERE Id_Product=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductsBuyed]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateProductsBuyed](@valueId int,@valueIdReitalSalers int,@valueIdTransation int,
											@valueQuantity int,@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE ProductsBuyed SET Id_RetailSalers=@valueIdReitalSalers,Id_Transation=@valueIdTransation,
	Quantity=@valueQuantity,Status=@valueStatus WHERE Id_ListProductsBuyed=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRating]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRating](@valueId int,@valueComment varchar(100), @valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	Update Comment SET Comment=@valueComment, Status=@valueStatus WHERE Id_Comment=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRetailSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRetailSales](@valueId int,@valueIdProduct int,@valueQuantity int,@valueDateStartSales Date,
									@valueDateClosing Date,@valueDateClosed Date,@valueDayReturn int, @valueDayDelivery int,
									@valueIdDelivery int,@valueVisitors int, @valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE RetailSales SET Id_Product=@valueIdProduct,Quantity=@valueQuantity,DateStartSales=@valueDateStartSales,
						DateClosing=@valueDateClosing,DateClosed=@valueDateClosed,DayReturn=@valueDayReturn,
						DayDelivery=@valueDayDelivery,Id_Delivery=@valueIdDelivery,Visitors=@valueVisitors,
						Status=@valueStatus  WHERE Id_RetailSales=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRetailSales2]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRetailSales2](@valueId int,@valueIdProduct int,@valueQuantity int,@valueDateStartSales Date,
									@valueDateClosing Date,@valueDateClosed Date,@valueDayReturn int, @valueDayDelivery int,
									@valueNameDelivery varchar(20),@valueVisitors int, @valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueIdDelivery int
	SELECT @valueIdDelivery= Id_Delivery FROM Delivery WHERE NameDelivery=@valueNameDelivery

	
	UPDATE RetailSales SET Id_Product=@valueIdProduct,Quantity=@valueQuantity,DateStartSales=@valueDateStartSales,
						DateClosing=@valueDateClosing,DayReturn=@valueDayReturn,
						DayDelivery=@valueDayDelivery,Id_Delivery=@valueIdDelivery,Visitors=@valueVisitors,
						Status=@valueStatus  WHERE Id_RetailSales=@valueId

	IF @valueStatus=0
	BEGIN
	UPDATE RetailSales SET DateClosed=@valueDateClosed   WHERE Id_RetailSales=@valueId
	END

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTransation]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTransation](@valueId int,@valuePayment int,@valueIdUser int,@valueSumPay money,@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	Update Transation SET Id_Payment=@valuePayment, Id_User=@valueIdUser,
	SumPay=@valueSumPay,Status=@valueStatus WHERE Id_Transation=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTypePayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTypePayment](@valueId AS INT,@valueName AS varchar(20),@value int,@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_TypePayment)FROM TypePayment;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT Name FROM TypePayment WHERE Id_TypePayment=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE TypePayment SET Name=@valueName, LimitString=@value WHERE Id_TypePayment=@valueId
	END
	UPDATE TypePayment SET Status=@valueStatus WHERE Id_TypePayment=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTypeUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateTypeUser](@valueId INT,@valueName AS varchar(20),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_TypeUser)FROM TypeUser;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT NameTypeUser FROM TypeUser WHERE Id_TypeUser=@tempNumber)=@valueName 
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE TypeUser SET NameTypeUser=@valueName WHERE Id_TypeUser=@valueId
	END
	UPDATE TypeUser SET Status=@valueStatus WHERE Id_TypeUser=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsers]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUsers](@valueId int,@valueIsActive smallint,@valueNick AS varchar(50),
									@valueName AS varchar(50),@valueSurname AS varchar(50), @valueEmail AS varchar(50), 
									@numberPhone AS int,@valueAdress AS varchar(50),@valueCity AS varchar(50),
									@valueCountVisitors int,@valueIdCompany int)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0, @ValueSame as int=0,@Number as int,@tempNumberTypeUser AS int;
	SELECT @Number=COUNT(Id_User)FROM Users
	WHILE @tempNumber!=@Number
	BEGIN
		IF (SELECT Nick FROM Users WHERE Id_User=@tempNumber)=@valueNick
		BEGIN 
			IF not exists (SELECT Nick FROM Users WHERE Id_User=@valueId)
				SELECT @ValueSame=1;
		END

		SELECT @tempNumber=@tempNumber+1;
	END

    IF @ValueSame=0
	BEGIN
		Update Users SET IsActive=@valueIsActive,Nick=@valueNick,
		Name=@valueName,Surname=@valueSurname,Email=@valueEmail,Phone=@numberPhone,Adress=@valueAdress,
		City=@valueCity, CountVisitors=@valueCountVisitors,Id_Company=@valueIdCompany WHERE Id_User=@valueId

		
	END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsers2]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUsers2](@valueId int,@valueIsActive smallint,@valueNick AS varchar(50),
									@valueName AS varchar(50),@valueSurname AS varchar(50), @valueEmail AS varchar(50), 
									@numberPhone AS int,@valueAdress AS varchar(50),@valueCity AS varchar(50),
									@valueCountVisitors int)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @tempNumber as int=0, @ValueSame as int=0,@Number as int,@tempNumberTypeUser AS int;
	SELECT @Number=COUNT(Id_User)FROM Users
	WHILE @tempNumber!=@Number
	BEGIN
		IF (SELECT Nick FROM Users WHERE Id_User=@tempNumber)=@valueNick 
	 	BEGIN 
			IF not exists (SELECT Nick FROM Users WHERE Id_User=@valueId)
				SELECT @ValueSame=1;
		END

		SELECT @tempNumber=@tempNumber+1;
	END

    IF @ValueSame=0
	BEGIN
		Update Users SET IsActive=@valueIsActive,Nick=@valueNick,
		Name=@valueName,Surname=@valueSurname,Email=@valueEmail,Phone=@numberPhone,Adress=@valueAdress,
		City=@valueCity, CountVisitors=@valueCountVisitors WHERE Id_User=@valueId

		
	END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUsersPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUsersPayment](@valueId int,@valuePaymentString varchar(30),@valueNameBank varchar(50),@valueStatus int)
AS
BEGIN
	SET NOCOUNT ON;

   
	UPDATE Payment SET PaymentString=@valuePaymentString,NameBank=@valueNameBank,Status=@valueStatus WHERE Id_Payment=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVat]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateVat](@valueId AS INT,@value AS INT,@valueStatus Int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_Vat)FROM VAT;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT Vat_rate FROM VAT WHERE Id_Vat=@tempNumber)=@value
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE VAT SET Vat_rate=@value WHERE Id_Vat=@valueId;
	END
	UPDATE VAT SET Status=@valueStatus WHERE Id_Vat=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateWarranty]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateWarranty](@valueId AS INT,@valueName AS varchar(20),@valueStatus int) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @valueSame AS INT=0,@tempNumber AS int=0,@number AS int
	SELECT @number=COUNT(Id_Warranty)FROM Warranty;

	WHILE  @tempNumber!=@Number
	BEGIN
		IF(SELECT TypeWarranty FROM Warranty WHERE Id_Warranty=@tempNumber)=@valueName
		BEGIN 
			SELECT @valueSame=1;
		END
		SELECT @tempNumber=@tempNumber+1;
	END
	IF @valueSame=0
	BEGIN
		UPDATE Warranty SET TypeWarranty=@valueName WHERE Id_Warranty=@valueId
	END
	UPDATE Warranty SET Status=@valueStatus WHERE Id_Warranty=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[UsersProducts]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersProducts]
AS
	SET NOCOUNT ON;
SELECT Name, Description, Price, MaxQuantity, NameParameter, Parameter, Image, Status, TypeWarranty, WarrantyDays, NameBrand, NameCategory, Vat_rate, Condition, Id_User FROM dbo.UsersProduct
GO
/****** Object:  StoredProcedure [dbo].[ValueComment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValueComment](@valueId int)
AS
BEGIN
	
	SET NOCOUNT ON;

    
	SELECT Id_Comment,Id_Product,Comment,Stars,Status  FROM ViewCommentRating WHERE Id_User=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[ValueCompany]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValueCompany](@valueIdCompany int)
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Id_Company,NameCompany,Adress,City,Phone,Email,
	NIP,Status FROM Company WHERE Id_Company=@valueIdCompany
END
GO
/****** Object:  StoredProcedure [dbo].[ValueProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValueProduct](@valueId int,@valueName varchar(25))
	
AS
BEGIN
	SET NOCOUNT ON;

    
	SELECT Name,Description,Price,Vat_rate,NameCondition,MaxQuantity,NameParameter, 
	Parameter,TypeWarranty,WarrantyDays,NameBrand,NameCategory,Image,Status
	FROM UsersProduct  WHERE Name=@valueName and Id_User=@valueId

END
GO
/****** Object:  StoredProcedure [dbo].[ValueProducts]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValueProducts](@valueId int,@valueName varchar(25))
	
AS
BEGIN
	SET NOCOUNT ON;

    
	SELECT Name,Description,Price,Vat_rate,NameCondition,MaxQuantity,NameParameter, 
	Parameter,TypeWarranty,WarrantyDays,NameBrand,NameCategory,Image,Status
	FROM UsersProduct  WHERE Name=@valueName and Id_User=@valueId

END
GO
/****** Object:  StoredProcedure [dbo].[ValuesBrand]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesBrand] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT NameBrand,Status FROM Brand
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesCategory]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesCategory] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT NameCategory,Status FROM Category
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesComment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesComment] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Comment,Stars,Id_User,Status FROM Comment
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesCompany]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesCompany] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT NameCompany,Adress,City,Phone,Email,NIP,Status FROM Company
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesCondition]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesCondition] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT NameCondition,Status FROM Condition
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesDelivery]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesDelivery] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT NameDelivery,Status FROM Delivery
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesInvoice]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesInvoice]
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Id_Transation,Date_Of_Make_Invoice,Date_Of_Pay,Date_Of_Service,Code_Invoice,Status FROM Invoice
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesPayment]
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Id_TypePayment,PaymentString,NameBank,Status FROM Payment
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesProduct] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id_Seller,Name,Description,Price,Id_Vat,Id_Condition,MaxQuantity,NameParameter,
	Parameter,Id_Warranty,WarrantyDays,Id_Brand,Id_Category,Image,Status FROM Product
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesProductsBuyed]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesProductsBuyed]
	
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id_RetailSalers,Id_Transation,Quantity,Status FROM ProductsBuyed
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesRating]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesRating]
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT Id_Product,Id_Comment, Id_User, Comment,Status  FROM ViewRating
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesRetailSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesRetailSales]
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Id_Product,Quantity,DateStartSales,DateClosing,DateClosed,DayReturn,DayDelivery,
	Id_Delivery,Visitors,Status FROM RetailSales
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesTransation]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesTransation]
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Id_Payment,Id_User,SumPay,Status FROM Transation
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesTypePayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesTypePayment] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Name,LimitString,Status FROM TypePayment
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesTypeUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesTypeUser] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT NameTypeUser,Status FROM TypeUser
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesUserPermission]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesUserPermission]
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Nick,NameTypeUser FROM ViewUserPermision
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesUsers]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesUsers] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Nick,IsActive,Name,Surname,Email,Phone,Adress,City,CountVisitors,Id_Company FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesUsersPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesUsersPayment]
AS
BEGIN
	SET NOCOUNT ON;

   
	SELECT Id_User,Id_Payment,PaymentString,LimitString,NameBank,Status FROM ViewUsersPayment
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesVat]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesVat]
	
AS
BEGIN

	SET NOCOUNT ON;
	SELECT Vat_rate,Status FROM VAT
END
GO
/****** Object:  StoredProcedure [dbo].[ValuesWarranty]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValuesWarranty] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TypeWarranty,Status FROM Warranty
END
GO
/****** Object:  StoredProcedure [dbo].[ValueUser]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValueUser](@valueId int)
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT * FROM UserCompany WHERE Id_User=@valueId)
	BEGIN
		SELECT Name, Surname, UserEmail, UserPhone,UserAdress, UserCity,NameCompany,CompanyEmail,CompanyPhone,CompanyAdress,CompanyCity,NIP FROM UserCompany WHERE Id_User=@valueId
	END
	ELSE
	BEGIN
		SELECT Name, Surname, Email, Phone,Adress, City FROM Users WHERE Id_User=@valueId
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ValueUserPermision]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ValueUserPermision](@valueId int)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT NameTypeUser FROM ViewUserPermision WHERE Id_User=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[ViewCurrentInvoice]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewCurrentInvoice](@valueId int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id_Transation, Id_Payment,Id_User,Date_Transation
	SumPay,StatusTransation,Id_Invoice,StatusInvoice,Date_Of_Make_Invoice,
	Date_Of_Pay,Date_Of_Service,Code_Invoice,Id_RetailSalers,Quantity FROM ViewUsersTransation WHERE Id_User=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[ViewMainSales]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewMainSales]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id_RetailSales,Quantity,DateStartSales,DateClosing,DateClosed,DayDelivery,
	DayReturn,Visitors,Status,NameDelivery,Id_Product,Id_Seller,Name,Description,
	Price,Vat_rate,NameCondition,MaxQuantity,NameParameter,Parameter,TypeWarranty,
	WarrantyDays,NameBrand,NameCategory,Image,StatusProduct FROM ViewUsersSalers 
END
GO
/****** Object:  StoredProcedure [dbo].[ViewProductRating]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewProductRating](@valueId int)
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id_Comment,Id_User,Comment,Stars,Status FROM ViewRating WHERE Id_Product=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[ViewSeller]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewSeller](@valueId int)
AS
BEGIN
	SET NOCOUNT ON;

	
	IF  exists(SELECT * FROM UserCompany WHERE Id_User=@valueId)	
	SELECT Nick,Name,Surname,CompanyAdress,CompanyCity,CompanyPhone,CompanyEmail,NIP FROM UserCompany WHERE Id_User= @valueId
	
	ELSE

	SELECT Nick, Name,Surname,Email,Phone,Adress,City FROM Users WHERE Id_User=@valueId

END
GO
/****** Object:  StoredProcedure [dbo].[ViewUserBuyed]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewUserBuyed] (@valueId int)
AS
BEGIN

	SET NOCOUNT ON;

    
	SELECT Id_Transation, Id_Payment,Id_User,Date_Transation,SumPay,StatusTransation,
	Id_ListProductsBuyed,Id_RetailSalers,QuantityBuyed,Id_Invoice,Date_Of_Make_Invoice,
	Date_Of_Pay,Date_Of_Service,Code_Invoice,StatusInvoice,Id_Seller,Id_Product,Name,Price,Vat_rate FROM UserBuyProducts 
	WHERE Id_User=@valueId 
END
GO
/****** Object:  StoredProcedure [dbo].[ViewUserPayment]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewUserPayment](@valueId int)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Id_User,Id_Payment,Id_TypePayment,PaymentString,NameBank,Status,TypePayment,
	LimitString,StatusTypePayment FROM ViewUsersPayment WHERE Id_User=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[ViewUserSalers]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewUserSalers](@valueId int)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT Id_RetailSales,Quantity,DateStartSales,DateClosing,DateClosed,DayReturn,NameDelivery,DayDelivery,Visitors,
	Status, Id_Product,Name,Description,Price,Vat_rate,NameCondition,MaxQuantity,NameParameter,
	Parameter,TypeWarranty,WarrantyDays,NameBrand,NameCategory,Image,StatusProduct FROM ViewUsersSalers 
	WHERE Id_Seller=@valueId
END
GO
/****** Object:  StoredProcedure [dbo].[ViewUserSell]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewUserSell] (@valueId int)
AS
BEGIN

	SET NOCOUNT ON;

    
	SELECT Id_Transation, Id_Payment,Id_User,Date_Transation,SumPay,StatusTransation,
	Id_ListProductsBuyed,Id_RetailSalers,QuantityBuyed,Id_Invoice,Date_Of_Make_Invoice,
	Date_Of_Pay,Date_Of_Service,Code_Invoice,StatusInvoice, Id_Seller,Id_Product,Name,Price,Vat_rate FROM UserBuyProducts 
	WHERE Id_Seller=@valueId 
END
GO
/****** Object:  StoredProcedure [dbo].[ViewUsersProduct]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewUsersProduct]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Name AS Nazwa,Description,Price As Cena,MaxQuantity,NameParameter, 
	Parameter,Image,Status,TypeWarranty,WarrantyDays,NameBrand,
	NameCategory,Vat_rate,Condition FROM UsersProduct 
END
GO
/****** Object:  StoredProcedure [dbo].[ViewUsersProducts]    Script Date: 2 lut 2022 15:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ViewUsersProducts](@valueId int)
	
AS
BEGIN
	SET NOCOUNT ON;

    
	SELECT Id_Product,Name,Description,Price,Vat_rate,NameCondition,MaxQuantity,NameParameter, 
	Parameter,TypeWarranty,WarrantyDays,NameBrand,NameCategory,Image,Status
	FROM UsersProduct  WHERE Id_User=@valueId

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[30] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 12
               Left = 223
               Bottom = 299
               Right = 393
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 11
               Left = 529
               Bottom = 385
               Right = 704
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Delivery"
            Begin Extent = 
               Top = 168
               Left = 2
               Bottom = 264
               Right = 172
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Brand"
            Begin Extent = 
               Top = 248
               Left = 788
               Bottom = 344
               Right = 958
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 343
               Left = 788
               Bottom = 439
               Right = 958
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Condition"
            Begin Extent = 
               Top = 81
               Left = 780
               Bottom = 177
               Right = 950
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VAT"
            Begin Extent = 
               Top = 0
               Left = 778
               Bottom = 96
               Right = 948
            End
            DisplayFlags = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductRetailSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'280
            TopColumn = 0
         End
         Begin Table = "Warranty"
            Begin Extent = 
               Top = 164
               Left = 787
               Bottom = 260
               Right = 957
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductRetailSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductRetailSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[21] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Product"
            Begin Extent = 
               Top = 2
               Left = 490
               Bottom = 331
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VAT"
            Begin Extent = 
               Top = 6
               Left = 703
               Bottom = 102
               Right = 873
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2565
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductsSallers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductsSallers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[25] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 501
               Bottom = 343
               Right = 690
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Brand"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 138
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3435
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductsViews'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductsViews'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[31] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -1070
      End
      Begin Tables = 
         Begin Table = "Transation"
            Begin Extent = 
               Top = 40
               Left = 1068
               Bottom = 263
               Right = 1238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductsBuyed"
            Begin Extent = 
               Top = 193
               Left = 1349
               Bottom = 354
               Right = 1551
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Invoice"
            Begin Extent = 
               Top = 1
               Left = 1334
               Bottom = 186
               Right = 1542
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 62
               Left = 1624
               Bottom = 361
               Right = 1794
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 52
               Left = 1845
               Bottom = 378
               Right = 2020
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "VAT"
            Begin Extent = 
               Top = 6
               Left = 2058
               Bottom = 119
               Right = 2228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Colum' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserBuyProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'n = 2655
         Alias = 1755
         Table = 2100
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserBuyProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserBuyProducts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[31] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 308
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Company"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 273
               Right = 419
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1365
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserCompany'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserCompany'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 291
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UsersPayment"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 197
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Payment"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 217
               Right = 630
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TypePayment"
            Begin Extent = 
               Top = 6
               Left = 668
               Bottom = 119
               Right = 844
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersMethodsPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersMethodsPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[44] 4[27] 2[18] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Product"
            Begin Extent = 
               Top = 4
               Left = 531
               Bottom = 366
               Right = 706
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 0
               Left = 60
               Bottom = 124
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VAT"
            Begin Extent = 
               Top = 145
               Left = 23
               Bottom = 249
               Right = 193
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Warranty"
            Begin Extent = 
               Top = 6
               Left = 744
               Bottom = 150
               Right = 914
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Brand"
            Begin Extent = 
               Top = 159
               Left = 777
               Bottom = 290
               Right = 947
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 313
               Left = 796
               Bottom = 435
               Right = 966
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Condition"
            Begin Extent = 
               Top = 287
               Left = 38
               Bottom = 414
               Right = 208
            End
            DisplayFlags = 280
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersProduct'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersProduct'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersProduct'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[48] 4[17] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 562
               Bottom = 292
               Right = 732
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 52
               Left = 313
               Bottom = 350
               Right = 488
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 96
               Left = 105
               Bottom = 375
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1620
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersSales'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 257
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UsersView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ProductsBuyed"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 284
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 260
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2085
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewActualQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewActualQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 259
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 288
               Right = 421
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAvailableQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAvailableQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Rating"
            Begin Extent = 
               Top = 17
               Left = 244
               Bottom = 134
               Right = 414
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Comment"
            Begin Extent = 
               Top = 72
               Left = 15
               Bottom = 257
               Right = 185
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCommentRating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewCommentRating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Rating"
            Begin Extent = 
               Top = 16
               Left = 546
               Bottom = 224
               Right = 716
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Comment"
            Begin Extent = 
               Top = 52
               Left = 202
               Bottom = 248
               Right = 372
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 72
               Left = 861
               Bottom = 298
               Right = 1036
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRating'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TypeUser"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 189
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserPermission"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 140
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 323
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUserPermision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUserPermision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[18] 2[3] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = -300
      End
      Begin Tables = 
         Begin Table = "UsersPayment"
            Begin Extent = 
               Top = 22
               Left = 258
               Bottom = 184
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Users"
            Begin Extent = 
               Top = 5
               Left = 42
               Bottom = 299
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Payment"
            Begin Extent = 
               Top = 4
               Left = 510
               Bottom = 250
               Right = 686
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TypePayment"
            Begin Extent = 
               Top = 6
               Left = 724
               Bottom = 248
               Right = 900
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1965
         Alias = 2775
         Table = 1860
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[30] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 4
               Left = 217
               Bottom = 259
               Right = 387
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Delivery"
            Begin Extent = 
               Top = 180
               Left = 0
               Bottom = 302
               Right = 170
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 651
               Bottom = 367
               Right = 826
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Brand"
            Begin Extent = 
               Top = 224
               Left = 911
               Bottom = 337
               Right = 1081
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Category"
            Begin Extent = 
               Top = 350
               Left = 916
               Bottom = 463
               Right = 1086
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Condition"
            Begin Extent = 
               Top = 2
               Left = 918
               Bottom = 115
               Right = 1092
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VAT"
            Begin Extent = 
               Top = 103
               Left = 418
               Bottom = 216
               Right = 588
            End
            DisplayFlags = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersSalers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'280
            TopColumn = 0
         End
         Begin Table = "Warranty"
            Begin Extent = 
               Top = 115
               Left = 914
               Bottom = 228
               Right = 1084
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 2175
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersSalers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersSalers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Invoice"
            Begin Extent = 
               Top = 0
               Left = 510
               Bottom = 193
               Right = 718
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Transation"
            Begin Extent = 
               Top = 113
               Left = 232
               Bottom = 311
               Right = 405
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ProductsBuyed"
            Begin Extent = 
               Top = 183
               Left = 650
               Bottom = 335
               Right = 852
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RetailSales"
            Begin Extent = 
               Top = 194
               Left = 936
               Bottom = 318
               Right = 1106
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1815
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersTransation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUsersTransation'
GO
USE [master]
GO
ALTER DATABASE [StoreDatabase] SET  READ_WRITE 
GO

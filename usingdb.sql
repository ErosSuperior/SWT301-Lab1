USE [master]
GO
/****** Object:  Database [MyStore]    Script Date: 10/26/2022 8:38:04 PM ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MyStore')
BEGIN
	ALTER DATABASE [MyStore] SET OFFLINE WITH ROLLBACK IMMEDIATE;
	ALTER DATABASE [MyStore] SET ONLINE;
	DROP DATABASE [MyStore];
END
GO
CREATE DATABASE [MyStore]
GO
ALTER DATABASE [MyStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyStore] SET RECOVERY FULL 
GO
ALTER DATABASE [MyStore] SET  MULTI_USER 
GO
ALTER DATABASE [MyStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyStore', N'ON'
GO
ALTER DATABASE [MyStore] SET QUERY_STORE = OFF
GO
USE [MyStore]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create the Account table
CREATE TABLE [dbo].[Account] (
  [AccountID] [INT] IDENTITY(1,1) NOT NULL,
  [UserName] [NVARCHAR](15) NOT NULL,
  [Password] [NVARCHAR](20) NOT NULL,
  [FullName] [NVARCHAR](50),
  [Address] [NVARCHAR](100),
  [Phone] [NVARCHAR](12),
  [Type] [BIT] NOT NULL,
CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED
    (
        [AccountID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the Orders table
CREATE TABLE [dbo].[Orders] (
  [OrderID] [INT] IDENTITY(1,1) NOT NULL,
  [CustomerID] [INT] NOT NULL,
  [OrderDate] DATE,
  [RequiredDate] DATE,
  [ShippedDate] DATE,
  [ShipAddress] NVARCHAR(255) NOT NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED
    (
	    [OrderID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the Products table
CREATE TABLE [dbo].[Products] (
  [ProductID] [INT] IDENTITY(1,1) NOT NULL,
  [ProductName] [NVARCHAR](50) NOT NULL,
  [SupplierID] [INT] NOT NULL,
  [CategoryID] [INT] NOT NULL,
  [Quantity] [INT] NOT NULL,
  [Price] [FLOAT] NOT NULL,
  [ProductImage] [NVARCHAR](800) NOT NULL,
  [Display] [BIT] NOT NULL,
  CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED
    (
	    [ProductID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the OrderDetails table
CREATE TABLE [dbo].[OrderDetails] (
  [OrderID] [INT] NOT NULL,
  [ProductID] [INT] NOT NULL,
  [Price] [FLOAT] NOT NULL,
  [Quantity] [INT] NOT NULL,
    CONSTRAINT [PK_orderdetails] PRIMARY KEY CLUSTERED
    (
      [OrderID] ASC,
	    [ProductID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the Categories table
CREATE TABLE [dbo].[Categories] (
  [CategoryID] [INT] IDENTITY(1,1) NOT NULL,
  [CategoryName] [NVARCHAR](50) NOT NULL,
  [Description] [NVARCHAR](255),
 CONSTRAINT [PK_categories] PRIMARY KEY CLUSTERED
    (
        [CategoryID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the Suppliers table
CREATE TABLE [dbo].[Suppliers] (
  [SupplierID] [INT] IDENTITY(1,1) NOT NULL,
  [CompanyName] [NVARCHAR](50) NOT NULL,
  [Address] [NVARCHAR](50) NOT NULL,
  [Phone] [NVARCHAR](12) NOT NULL,
CONSTRAINT [PK_suppliers] PRIMARY KEY CLUSTERED
    (
        [SupplierID] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Account] ([AccountID])
GO


ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO

ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO


ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO


ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO



-- Insert data into the Account table
INSERT INTO [dbo].[Account] ([UserName], [Password], [FullName], [Address], [Phone], [Type])
VALUES 
('baspdd', '123', 'duy Doe', '123 Main St', '1234567890', 1),
('duy', '123', 'John Doe', '123 Main St', '1234567890', 0),
('duong', '222', 'Jane Smith', '456 Elm St', '9876543210', 0),
('davidjohnson', 'pass789', 'David Johnson', '789 Oak St', '5555555555', 0),
('emilydavis', 'passabc', 'Emily Davis', '321 Pine St', '7777777777', 0),
('michaelwilson', 'passdef', 'Michael Wilson', '567 Maple Ave', '8888888888', 0),
('sarahbrown', 'passghi', 'Sarah Brown', '890 Walnut Blvd', '2222222222', 0),
('jessicataylor', 'passjkl', 'Jessica Taylor', '432 Cedar Ln', '3333333333', 0),
('andrewclark', 'passmno', 'Andrew Clark', '876 Birch Rd', '4444444444', 0),
('oliviaparker', 'passpqr', 'Olivia Parker', '654 Spruce Dr', '9999999999', 0),
('danieladams', 'passtu', 'Daniel Adams', '987 Pineapple Ave', '6666666666', 0),
('hehe', '123', 'John Doe', '123 Main St', '1234567890', 0);


-- Insert data into the Orders table
INSERT INTO [dbo].[Orders] ([CustomerID], [OrderDate], [RequiredDate], [ShippedDate], [ShipAddress])
VALUES 
(2, NULL, NULL, NULL, '123 Main St'),
(2, '2023-06-01', '2023-06-05', '2023-06-03', '123 Main St'),
(2, '2023-06-02', '2023-06-06', '2023-06-04', '456 Elm St'),
(3, '2023-06-03', '2023-06-07', '2023-06-05', '789 Oak St'),
(4, '2023-06-04', '2023-06-08', '2023-06-06', '321 Pine St'),
(5, '2023-06-05', '2023-06-09', '2023-06-07', '567 Maple Ave'),
(6, '2023-06-06', '2023-06-10', '2023-06-08', '890 Walnut Blvd'),
(7, '2023-06-07', '2023-06-11', '2023-06-09', '432 Cedar Ln'),
(8, '2023-06-08', '2023-06-12', '2023-06-10', '876 Birch Rd'),
(9, '2023-06-09', '2023-06-13', '2023-06-11', '654 Spruce Dr'),
(10, '2023-06-10', '2023-06-14', '2023-06-12', '987 Pineapple Ave');

-- Insert data into the Categories table
INSERT INTO [dbo].[Categories] ([CategoryName], [Description])
VALUES 
('Tech', 'Description 1'),
('Clothes', 'Description 2'),
('House', 'Description 3');

-- Insert data into the Suppliers table
INSERT INTO [dbo].[Suppliers] ([CompanyName], [Address], [Phone])
VALUES 
('Viettel', '123 Supplier St', '1111111111'),
('VNPT', '456 Supplier Ln', '2222222222'),
('FPT', '789 Supplier Rd', '3333333333');

-- Insert data into the Products table
INSERT INTO [dbo].[Products] ([ProductName], [SupplierID], [CategoryID], [Quantity], [Price], [ProductImage], [Display])
VALUES 
('Product1', 1, 1, 100, 9.99, 'https://down-vn.img.susercontent.com/file/ecaa8dcc91954323553a520339fa1a27', 1),
('Product2', 2, 2, 50, 19.99, 'https://down-vn.img.susercontent.com/file/vn-11134201-23030-r8yag7mgufovb0', 1),
('Product3', 3, 3, 200, 4.99, 'https://down-vn.img.susercontent.com/file/934bdb660d224820eae0e3d5a82ea4aa', 1),
('Product4', 1, 2, 75, 12.50, 'https://down-vn.img.susercontent.com/file/ecaa8dcc91954323553a520339fa1a27', 1),
('Product5', 2, 1, 150, 8.99, 'https://down-vn.img.susercontent.com/file/180018fcdf9e676d1b2ac12873c39478', 1),
('Product6', 3, 3, 100, 6.75, 'https://down-vn.img.susercontent.com/file/934bdb660d224820eae0e3d5a82ea4aa', 1),
('Product7', 1, 2, 120, 11.99, 'https://down-vn.img.susercontent.com/file/ecaa8dcc91954323553a520339fa1a27', 1),
('Product8', 2, 1, 80, 14.99, 'https://down-vn.img.susercontent.com/file/180018fcdf9e676d1b2ac12873c39478', 1),
('Product9', 3, 3, 90, 7.50, 'https://down-vn.img.susercontent.com/file/934bdb660d224820eae0e3d5a82ea4aa', 1),
('Product10', 1, 2, 110, 10.75, 'https://down-vn.img.susercontent.com/file/ecaa8dcc91954323553a520339fa1a27', 1);

-- Insert data into the OrderDetails table
INSERT INTO [dbo].[OrderDetails] ([OrderID], [ProductID], [Price], [Quantity])
VALUES 
(1, 1, 9.99, 2),
(1, 2, 19.99, 2),
(2, 1, 9.99, 1),
(2, 2, 19.99, 1),
(3, 3, 4.99, 5),
(4, 4, 12.50, 3),
(5, 5, 8.99, 4),
(6, 6, 6.75, 2),
(7, 7, 11.99, 1),
(8, 8, 14.99, 3),
(9, 9, 7.50, 2),
(10, 10, 10.75, 4);


USE [master]
GO
ALTER DATABASE [MyStore] SET  READ_WRITE 
GO

USE [RIGO_STORE]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductStatus] [nvarchar](20) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD CHECK  (([ProductStatus]='Inactive' OR [ProductStatus]='OutOfStock' OR [ProductStatus]='Active'))
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddProduct]
    @Name VARCHAR(100),
    @Price DECIMAL(18,2),
    @Quantity INT,
    @ProductStatus NVARCHAR(20)
AS
BEGIN
    INSERT INTO Products (Name, Price, Quantity, ProductStatus, CreationDate, UpdateDate)
    VALUES (@Name, @Price, @Quantity, @ProductStatus, GETDATE(), GETDATE());
    
    SELECT SCOPE_IDENTITY() AS NewProductId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProduct]
    @ProductId INT
AS
BEGIN
    DELETE FROM Products
    WHERE Id = @ProductId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllProducts]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllProducts]
AS
BEGIN
    SELECT Id, Name, Price, Quantity, ProductStatus, CreationDate, UpdateDate
    FROM Products;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductById]
    @ProductId INT
AS
BEGIN
    SELECT Id, Name, Price, Quantity, ProductStatus, CreationDate, UpdateDate
    FROM Products
    WHERE Id = @ProductId;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateProduct]
    @ProductId INT,
    @Name VARCHAR(100),
    @Price DECIMAL(18,2),
    @Quantity INT,
    @ProductStatus NVARCHAR(20)
AS
BEGIN
    
    UPDATE Products
    SET 
        Name = @Name,
        Price = @Price,
        Quantity = @Quantity,
        ProductStatus = @ProductStatus,
        UpdateDate = GETDATE()
    WHERE Id = @ProductId;
    
    
    SELECT 
        Id,
        Name,
        Price,
        Quantity,
        ProductStatus,
        CreationDate, 
        UpdateDate
    FROM Products
    WHERE Id = @ProductId;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductQuantity]    Script Date: 9/18/2024 7:25:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProductQuantity]
    @ProductId INT,
    @Quantity INT
AS
BEGIN
    UPDATE Products
    SET 
        Quantity = @Quantity,
        UpdateDate = GETDATE()
    WHERE Id = @ProductId;
END;
GO

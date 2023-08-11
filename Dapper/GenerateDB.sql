CREATE DATABASE DapperDatabase;
GO

USE DapperDatabase;
GO

CREATE TABLE Product (
    ID INT PRIMARY KEY IDENTITY,
    Description NVARCHAR(255) NOT NULL,
    Weight DECIMAL(18, 2) NOT NULL,
    Height DECIMAL(18, 2) NOT NULL,
    Width DECIMAL(18, 2) NOT NULL,
    Length DECIMAL(18, 2) NOT NULL
);
GO

CREATE TABLE [Order] (
    ID INT PRIMARY KEY IDENTITY,
    Status NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    UpdatedDate DATETIME NOT NULL,
    ProductID INT FOREIGN KEY REFERENCES Product(ID)
);
GO

INSERT INTO Product (Description, Weight, Height, Width, Length)
VALUES  ('TV', 10.5, 20.0, 30.0, 40.0),
        ('Sound System', 15.5, 25.0, 35.0, 45.0),
        ('PC', 20.5, 30.0, 40.0, 50.0);
GO

DECLARE @currentDate DATETIME = GETDATE();
INSERT INTO [Order] (Status, CreatedDate, UpdatedDate, ProductID)
VALUES  ('NotStarted', @currentDate, @currentDate, 1),
        ('InProgress', @currentDate, @currentDate, 2),
        ('Done', @currentDate, @currentDate, 3),
		('Cancelled', @currentDate, @currentDate, 2),
		('Cancelled', @currentDate, @currentDate, 1);
GO
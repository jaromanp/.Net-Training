﻿CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AddressId] INT NOT NULL, 
    [PersonId] INT NOT NULL, 
    [CompanyName] NVARCHAR(20) NOT NULL, 
    [Position] NVARCHAR(30) NULL, 
    [EmployeeName] NVARCHAR(100) NULL, 
    CONSTRAINT [FK_Employee_ToAddress] FOREIGN KEY (AddressId) REFERENCES dbo.Address(Id), 
    CONSTRAINT [FK_Employee_ToPerson] FOREIGN KEY (PersonID) REFERENCES dbo.Person(Id)
)

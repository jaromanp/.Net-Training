/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET NOCOUNT ON;

BEGIN TRANSACTION

    DECLARE @employee AS TABLE
    (
        [Id] INT, 
        [AddressId] INT, 
        [PersonId] INT, 
        [CompanyName] NVARCHAR(20), 
        [Position] NVARCHAR(30), 
        [EmployeeName] NVARCHAR(100) 
    )
    INSERT INTO @employee(Id, AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES
        (1, 2, 5, 'Google', 'Developer', 'Capitan America'),
        (2, 3, 4, 'Microsoft', 'QA Tester', 'Black Widow'),
        (3, 1, 2, 'Apple', 'UI Designer', 'Maya Perez'),
        (4, 5, 1, 'Google', 'Developer', NULL),
        (5, 4, 3, 'Tesla', 'Engineer', NULL)

    MERGE dbo.Employee AS target
    USING (SELECT Id, AddressId, PersonId, CompanyName, Position, EmployeeName FROM @employee) AS source (Id, AddressId, PersonId, CompanyName, Position, EmployeeName)
    ON (target.Id = source.Id)
    WHEN MATCHED THEN
        UPDATE
        SET
            Id = source.Id,
            AddressId = source.AddressId,
            PersonId = source.PersonId,
            CompanyName = source.CompanyName,
            Position = source.Position,
            EmployeeName = source.EmployeeName
        WHEN NOT MATCHED THEN
            INSERT (Id, AddressId, PersonId, CompanyName, Position, Employee)
            VALUES (source.Id, source.AddressId, source.PersonId, source.CompanyName, source.Position, source.EmployeeName);

COMMIT TRANSACTION;

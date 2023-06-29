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

    INSERT INTO dbo.Employee(AddressId, PersonId, CompanyName, Position, EmployeeName)
    SELECT AddressId, PersonId, CompanyName, Position, EmployeeName
    FROM (VALUES
        (5, 5, 'Google', 'Developer', 'Capitan America'),
        (6, 4, 'Microsoft', 'QA Tester', 'Black Widow'),
        (7, 2, 'Apple', 'UI Designer', 'Maya Perez'),
        (8, 1, 'Google', 'Developer', NULL),
        (9, 3, 'Tesla', 'Engineer', NULL)
    ) AS v(AddressId, PersonId, CompanyName, Position, EmployeeName)
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Employee e
        WHERE e.AddressId = v.AddressId AND e.PersonId = v.PersonId
    );  

COMMIT TRANSACTION;

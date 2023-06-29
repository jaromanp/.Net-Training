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

    DECLARE @company AS TABLE
    (
        [Id] INT, 
        [Name] NVARCHAR(20), 
        [AddressId] INT 
    )
    INSERT INTO @company(Id, Name, AddressId)
    VALUES
        (1,'Google', 2),
        (2,'Microsoft', 3),
        (3,'Apple', 1),
        (4,'Tesla', 5)
    MERGE dbo.Company AS target
    USING (SELECT Id, Name, AddressId FROM @company) AS source (Id, Name, AddressId)
    ON (target.Id = source.Id)
    WHEN MATCHED THEN
        UPDATE
        SET
            Id = source.Id,
            Name = source.Name,
            AddressId = source.AddressId
        WHEN NOT MATCHED THEN
            INSERT (Id, Name, AddressId)
            VALUES (source.Id, source.Name, source.AddressId);

COMMIT TRANSACTION;
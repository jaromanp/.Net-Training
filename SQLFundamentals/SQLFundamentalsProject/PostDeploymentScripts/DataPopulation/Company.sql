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
        [Name] NVARCHAR(20), 
        [AddressId] INT 
    )
    INSERT INTO @company(Name, AddressId)
    VALUES
        ('Google', 2),
        ('Microsoft', 3),
        ('Apple', 1),
        ('Tesla', 4)
    MERGE dbo.Company AS target
    USING (SELECT Name, AddressId FROM @company) AS source (Name, AddressId)
    ON (target.Name = source.Name)
    WHEN MATCHED THEN
        UPDATE
        SET            
            Name = source.Name,
            AddressId = source.AddressId
        WHEN NOT MATCHED THEN
            INSERT (Name, AddressId)
            VALUES (source.Name, source.AddressId);

COMMIT TRANSACTION;
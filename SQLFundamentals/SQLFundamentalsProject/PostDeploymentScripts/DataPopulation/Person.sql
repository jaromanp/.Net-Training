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

    DECLARE @person AS TABLE
    (
        [Id] INT, 
        [FirstName] NVARCHAR(50), 
        [LastName] NVARCHAR(50)
    )
    INSERT INTO @person(Id, FirstName, LastName)
    VALUES
        (1,'Alex', 'Ford'),
        (2,'Maya', 'Perez'),
        (3,'Jhon', 'Oswell'),
        (4,'Natasha', 'Romanof'),
        (5,'Steven', 'Rogers')
    MERGE dbo.Person AS target
    USING (SELECT Id, FirstName, LastName FROM @person) AS source (Id, FirstName, LastName)
    ON (target.Id = source.Id)
    WHEN MATCHED THEN
        UPDATE
        SET
            Id = source.Id,
            FirstName = source.FirstName,
            LastName = source.LastName
        WHEN NOT MATCHED THEN
            INSERT (Id, FirstName, LastName)
            VALUES (source.Id, source.FirstName, source.LastName);

COMMIT TRANSACTION;
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
        [FirstName] NVARCHAR(50), 
        [LastName] NVARCHAR(50)
    )
    INSERT INTO @person(FirstName, LastName)
    VALUES
        ('Alex', 'Ford'),
        ('Maya', 'Perez'),
        ('Jhon', 'Oswell'),
        ('Natasha', 'Romanof'),
        ('Steven', 'Rogers')
    MERGE dbo.Person AS target
    USING (SELECT FirstName, LastName FROM @person) AS source (FirstName, LastName)
    ON (target.FirstName = source.FirstName AND target.LastName = source.LastName)
    WHEN MATCHED THEN
        UPDATE
        SET            
            FirstName = source.FirstName,
            LastName = source.LastName
        WHEN NOT MATCHED THEN
            INSERT (FirstName, LastName)
            VALUES (source.FirstName, source.LastName);

COMMIT TRANSACTION;
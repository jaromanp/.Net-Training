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

    DECLARE @address AS TABLE
    (
        [Id] INT,
        [Street] NVARCHAR(50), 
        [City] NVARCHAR(20), 
        [State] NVARCHAR(50), 
        [ZipCode] NVARCHAR(50)
    )
    INSERT INTO @address(Id, Street, City, State, ZipCode)
    VALUES
        (1,'Bourbon Street', 'New Orleans', 'IL', '432562'),
        (2,'Wall Street', 'New York', 'NW', '543235'),
        (3,'Hollywood Boulevard', 'Los Angeles', 'CA', '352652'),
        (4,'Ocean Drive', 'Miamia', 'FL', '442562')

    MERGE dbo.Address AS target
    USING (SELECT Id, Street, City, State, ZipCode FROM @address) AS source (Id, Street, City, State, ZipCode)
    ON (target.Id = source.Id)
    WHEN MATCHED THEN
        UPDATE
        SET
            Id = source.Id,
            Street = source.Street,
            City = source.City,
            State = source.State,
            ZipCode = source.ZipCode
        WHEN NOT MATCHED THEN
            INSERT (Id, Street, City, State, ZipCode)
            VALUES (source.Id, source.Street, source.City, source.State, source.ZipCode);

COMMIT TRANSACTION;
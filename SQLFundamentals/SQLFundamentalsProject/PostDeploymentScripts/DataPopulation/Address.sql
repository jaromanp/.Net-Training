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
        [Street] NVARCHAR(50), 
        [City] NVARCHAR(20), 
        [State] NVARCHAR(50), 
        [ZipCode] NVARCHAR(50)
    )
    INSERT INTO @address(Street, City, State, ZipCode)
    VALUES
        ('Bourbon Street', 'New Orleans', 'IL', '432562'),
        ('Wall Street', 'New York', 'NW', '543235'),
        ('Hollywood Boulevard', 'Los Angeles', 'CA', '352652'),
        ('Ocean Drive', 'Miamia', 'FL', '442562')

    MERGE dbo.Address AS target
    USING (SELECT Street, City, State, ZipCode FROM @address) AS source (Street, City, State, ZipCode)
    ON (target.Street = source.Street)
    WHEN MATCHED THEN
        UPDATE
        SET            
            Street = source.Street,
            City = source.City,
            State = source.State,
            ZipCode = source.ZipCode
        WHEN NOT MATCHED THEN
            INSERT (Street, City, State, ZipCode)
            VALUES (source.Street, source.City, source.State, source.ZipCode);

COMMIT TRANSACTION;
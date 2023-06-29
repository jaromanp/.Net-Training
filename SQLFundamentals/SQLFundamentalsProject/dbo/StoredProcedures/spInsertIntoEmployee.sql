CREATE PROCEDURE [dbo].[spInsertEmployeeInfo]
	@EmployeeName NVARCHAR(100) = NULL,
	@FirstName NVARCHAR(100) = NULL,
	@LastName NVARCHAR(100) = NULL,
	@CompanyName NVARCHAR(100),
	@Position NVARCHAR(100) = NULL,
	@Street NVARCHAR(100),
	@City NVARCHAR(100) = NULL,
	@State NVARCHAR(100) = NULL,
	@ZipCode NVARCHAR(100) = NULL
AS
	BEGIN
		
		--First Check the conditions
		IF(@EmployeeName IS NULL AND @FirstName IS NULL AND @LastName IS NULL)
			BEGIN
				RAISERROR('Either EmployeeName  or FirstName or LastName should be not be NULL', 16, 1);
				RETURN
			END

		IF (@EmployeeName = '' OR @FirstName = '' OR @LastName = '')
			BEGIN
				RAISERROR('Either EmployeeName  or FirstName or LastName should be not be empty', 16, 1);
				RETURN
			END

		--Now insert the information

		IF(@FirstName IS NULL AND @LastName IS NULL)
			BEGIN
				SET @FirstName = 'No name provided'
				SET @LastName = 'No name provided'
			END

		INSERT INTO dbo.Person (FirstName, LastName)
		VALUES (@FirstName, @LastName)

		DECLARE @PersonID int;
		SET @PersonID = SCOPE_IDENTITY();

		INSERT INTO dbo.Address (Street, City, State, ZipCode)
		VALUES (@Street, @City, @State, @ZipCode)

		DECLARE @AddressID int;
		SET @AddressID = SCOPE_IDENTITY();

		INSERT INTO dbo.Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
		VALUES (@AddressID, @PersonID, LEFT(@CompanyName, 20), @Position, ISNULL(@EmployeeName, CONCAT(@FirstName, ' ', @LastName)))

	END


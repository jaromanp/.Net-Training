CREATE TRIGGER [CreateCompanyFromEmployee]
	ON dbo.Employee
AFTER INSERT
AS
SET NOCOUNT ON;
BEGIN
    INSERT INTO dbo.Company (Name, AddressId)
    SELECT CompanyName, AddressId
    FROM inserted
END


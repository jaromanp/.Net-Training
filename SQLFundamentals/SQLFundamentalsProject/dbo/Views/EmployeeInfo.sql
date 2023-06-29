CREATE VIEW [dbo].[EmployeeInfo]
	AS 
		SELECT e.Id AS 'EmployeeId',
		ISNULL(e.EmployeeName, CONCAT(p.FirstName,' ',  p.LastName)) AS 'EmployeeFullName',
		CONCAT(a.ZipCode,'_', a.State, ',', a.City,'-', a.Street) AS 'EmployeeFullAddress',
		CONCAT(e.CompanyName, '(', e.Position, ')') AS 'EmployeeCompanyInfo'
		FROM dbo.Employee AS e
		JOIN dbo.Person AS p ON p.Id = e.PersonId
		JOIN dbo.Address AS a ON a.Id = e.AddressId
		

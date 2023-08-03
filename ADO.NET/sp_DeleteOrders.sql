CREATE PROCEDURE dbo.sp_DeleteOrders
    @Month INT = NULL,
    @Year INT = NULL,
    @Status NVARCHAR(50) = NULL,
    @ProductID INT = NULL
AS
BEGIN
    DELETE FROM [Order]
    WHERE (@Month IS NULL OR MONTH(CreatedDate) = @Month)
    AND (@Year IS NULL OR YEAR(CreatedDate) = @Year)
    AND (@Status IS NULL OR Status = @Status)
    AND (@ProductID IS NULL OR ProductID = @ProductID)
END
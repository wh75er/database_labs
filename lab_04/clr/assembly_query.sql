sp_configure 'show advanced options', 1
GO
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO


DROP FUNCTION dbo.scal_func
DROP AGGREGATE dbo.PriceAvg
DROP FUNCTION dbo.getInterval
DROP ASSEMBLY funcs_assembly

CREATE ASSEMBLY funcs_assembly
FROM '/opt/mssql/data/libs/scalar_func.dll'
GO

CREATE FUNCTION dbo.scal_func(@Num int)
RETURNS INT
AS
        EXTERNAL NAME
        funcs_assembly.[scalar_func.Class1].scal_func
GO


SELECT dbo.scal_func(350) AS 'SUM'
GO

CREATE AGGREGATE PriceAvg(@value int, @price int)
RETURNS INT
        EXTERNAL NAME
        funcs_assembly.[scalar_func.PriceAvg]
GO

SELECT dbo.PriceAvg(BuildId, Price) FROM CameraBuild

CREATE FUNCTION getInterval(@from int, @to int)
RETURNS TABLE (ID int)
AS
        EXTERNAL NAME
        funcs_assembly.[scalar_func.TableFunction].GenerateInterval
GO

SELECT * FROM getInterval(10, 20)
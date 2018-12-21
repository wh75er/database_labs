sp_configure 'show advanced options', 1
GO
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO

IF (EXISTS(select * from sys.assemblies where name = 'func_assembly'))
BEGIN
	-- remove the reference 
	IF(EXISTS(select * from sys.objects where name = 'func'))
		DROP FUNCTION dbo.func
	DROP ASSEMBLY funcu_assembly
END

CREATE ASSEMBLY func_assembly
FROM '/opt/mssql/data/libs/clr.dll'
GO

CREATE FUNCTION dbo.func()
RETURNS TABLE
([Name] [NVARCHAR](50))
AS
        EXTERNAL NAME
        func_assembly.[table_func.Tabular].InitMethod;
GO
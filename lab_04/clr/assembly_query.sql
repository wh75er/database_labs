sp_configure 'show advanced options', 1
GO
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO

IF (EXISTS(select * from sys.assemblies where name = 'funcs_assembly'))
BEGIN
	-- remove the reference 
	IF(EXISTS(select * from sys.objects where name = 'scal_func'))
		DROP FUNCTION dbo.scal_func
        IF(EXISTS(select * from sys.objects where name = 'PriceAvg'))
		DROP AGGREGATE dbo.PriceAvg
        IF(EXISTS(select * from sys.objects where name = 'getTopHund'))
		DROP FUNCTION dbo.getTopHund
        IF(EXISTS(select * from sys.objects where name = 'getOldBuilds'))
		DROP PROCEDURE dbo.getOldBuilds
        IF(EXISTS(select * from sys.objects where name = 'triggerShowDelete'))
		DROP TRIGGER dbo.triggerShowDelete
        IF(EXISTS(select * from sys.objects where name = 'triggerShowInsert'))
		DROP TRIGGER dbo.triggerShowInsert
	DROP ASSEMBLY funcs_assembly
END

--DROP FUNCTION dbo.scal_func
--DROP AGGREGATE dbo.PriceAvg
--DROP FUNCTION dbo.getTopHund
--DROP ASSEMBLY funcs_assembly

CREATE ASSEMBLY funcs_assembly
FROM '/opt/mssql/data/libs/scalar_func.dll'
GO

CREATE FUNCTION dbo.scal_func(@Num int)
RETURNS INT
AS
        EXTERNAL NAME
        funcs_assembly.[scalar_func.Class1].scal_func
GO

CREATE FUNCTION getTopHund()
RETURNS TABLE
(BuildId int, Price int)
AS
        EXTERNAL NAME funcs_assembly.[scalar_func.TabularPriceLog].InitMethod;
GO

CREATE AGGREGATE PriceAvg(@value int, @price int)
RETURNS INT
        EXTERNAL NAME
        funcs_assembly.[scalar_func.PriceAvg]
GO

CREATE PROCEDURE getOldBuilds
AS
        EXTERNAL NAME funcs_assembly.[scalar_func.StoredProcedure].getOldBuilds
GO

CREATE TRIGGER triggerShowDelete
ON CameraBuild
FOR DELETE
AS EXTERNAL NAME funcs_assembly.[scalar_func.Trigger_UserEmailAudit].EmailAudit
GO

CREATE TRIGGER triggerShowInsert
ON CameraBuild
FOR INSERT
AS EXTERNAL NAME funcs_assembly.[scalar_func.Trigger_UserEmailAudit].EmailAudit
GO

SELECT dbo.scal_func(350) AS 'SUM'

SELECT dbo.PriceAvg(BuildId, Price) AS 'Avg price' FROM CameraBuild

SELECT * FROM getTopHund()

exec getOldBuilds

INSERT CameraBuild(CameraBodyId, LensId, FilterId, Price, [Year])
VALUES (200, 15, 319, 1337, '2008-06-29')
INSERT CameraBuild(CameraBodyId, LensId, FilterId, Price, [Year])
VALUES (222, 15, 319, 1337, '2008-06-29')

DELETE FROM CameraBuild WHERE BuildId > 1000
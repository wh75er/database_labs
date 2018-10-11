USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'CameraDB'
)
CREATE DATABASE [CameraDB]
GO

ALTER DATABASE [CameraDB] SET QUERY_STORE=ON
GO
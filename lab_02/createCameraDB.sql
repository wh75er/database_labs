-- Create a new table called 'CameraBody' in schema 'dbo'
-- Drop the table if it already exists
IF OBJECT_ID('dbo.CameraBuild', 'U') IS NOT NULL
DROP TABLE dbo.CameraBuild
GO
IF OBJECT_ID('dbo.CameraBody', 'U') IS NOT NULL
DROP TABLE dbo.CameraBody
GO
IF OBJECT_ID('dbo.Lens', 'U') IS NOT NULL
DROP TABLE dbo.Lens
GO
IF OBJECT_ID('dbo.Filter', 'U') IS NOT NULL
DROP TABLE dbo.Filter
GO
-- Create the table in the specified schema
CREATE TABLE dbo.CameraBody
(
    CameraId INT IDENTITY(1, 1) PRIMARY KEY,
    -- primary key column
    Brand [NVARCHAR](50) NOT NULL,
    Model [NVARCHAR](50) NOT NULL,
    Mount [NVARCHAR](50) NOT NULL,
    Megapixels [nvarchar](50) NOT NULL,
    [Color] [nvarchar](50) NOT NULL
);
GO


-- Create the table in the specified schema
CREATE TABLE dbo.Lens
(
    LensId INT IDENTITY(1, 1) PRIMARY KEY,
    -- primary key column
    Name [NVARCHAR](50) NOT NULL,
    MountType [NVARCHAR](50) NOT NULL,
    Purpose [NVARCHAR](50) NOT NULL,
    Diameter INT NOT NULL
);
GO
alter table Lens 
ADD CONSTRAINT
diameter_constr CHECK(Diameter > 0)


-- Create the table in the specified schema
CREATE TABLE dbo.Filter
(
    FilterId INT IDENTITY(1, 1) PRIMARY KEY,
    -- primary key column
    Name [NVARCHAR](50) NOT NULL,
    Purpose [NVARCHAR](50) NOT NULL,
    Diameter [NVARCHAR](50) NOT NULL
);
GO

-- Create the table in the specified schema
CREATE TABLE dbo.CameraBuild
(
    BuildId INT IDENTITY(1, 1) PRIMARY KEY,
    -- primary key column
    CameraBodyId INT NOT NULL FOREIGN KEY REFERENCES CameraBody(CameraId),
    LensId INT NOT NULL FOREIGN KEY REFERENCES Lens(LensId),
    FilterId INT NOT NULL FOREIGN KEY REFERENCES dbo.Filter(FilterId),
    Price INT,
    [Year]    DATE
);
GO
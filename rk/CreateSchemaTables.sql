CREATE SCHEMA rk3

IF OBJECT_ID('rk3.employees', 'U') IS NOT NULL
DROP TABLE rk3.employees
GO
IF OBJECT_ID('rk3.course', 'U') IS NOT NULL
DROP TABLE rk3.course
GO
CREATE TABLE rk3.course (
    id INT IDENTITY(1, 1) PRIMARY KEY,
    [Name] [NVARCHAR](50) NOT NULL,
    [Date] DATE NOT NULL,
    Spec [NVARCHAR](50) NOT NULL,
    [Time] TIME NOT NULL,
    People INT NOT NULL
)

CREATE TABLE rk3.employees (
    id INT IDENTITY(1, 1) PRIMARY KEY,
    Fio [NVARCHAR](50) NOT NULL,
    Birthday DATE NOT NULL,
    Spec [NVARCHAR](50) NOT NULL,
    LastDate DATE NOT NULL,
    courseId INT NOT NULL FOREIGN KEY REFERENCES rk3.course(id)
)
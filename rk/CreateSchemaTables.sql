CREATE SCHEMA rk3

IF OBJECT_ID('rk3.students', 'U') IS NOT NULL
DROP TABLE rk3.students
GO
IF OBJECT_ID('rk3.teachers', 'U') IS NOT NULL
DROP TABLE rk3.teachers
GO
CREATE TABLE rk3.teachers (
    id INT IDENTITY(1, 1) PRIMARY KEY,
    [Name] [NVARCHAR](50) NOT NULL,
    Spec [NVARCHAR](50) NOT NULL,
    People INT NOT NULL
)

CREATE TABLE rk3.students (
    id INT IDENTITY(1, 1) PRIMARY KEY,
    [Name] [NVARCHAR](50) NOT NULL,
    Birthday DATE NOT NULL,
    Spec [NVARCHAR](50) NOT NULL,
    courseTheme [NVARCHAR](50) NOT NULL,
    teacherId INT FOREIGN KEY REFERENCES rk3.teachers(id)
)
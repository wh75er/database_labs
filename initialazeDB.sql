GO
IF EXISTS (SELECT * FROM sys.databases WHERE name='extended')
BEGIN
	--ALTER DATABASE extended SET SINGLE_USER WITH ROLLBACK IMMEDIATE; 
	DROP DATABASE extended;
END

CREATE DATABASE extended;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE name='EmployeeVacation')
BEGIN
  drop table EmployeeVacation
END
IF EXISTS (SELECT * FROM sys.objects WHERE name='Vacation')
BEGIN
  drop table Vacation
END


CREATE TABLE Vacation
(
	VacationID INT IDENTITY(1,1) PRIMARY KEY,
	VacationType TEXT NOT NULL
);

CREATE TABLE EmployeeVacation
(
	EmployeeVacationID INT IDENTITY(1,1) PRIMARY KEY,
	EmployeeName nvarchar(15),
	VacationID INT FOREIGN KEY REFERENCES Vacation(VacationID),
	DateGAP DATE
);

INSERT Vacation(VacationType)
VALUES 
('Camping'),
('Road Trip'),
('Adventure travel'),
('Safari')


INSERT EmployeeVacation(EmployeeName, VacationID, DateGAP)
VALUES
('Ryazanova', 1,  '7/1/2018'),
('Ryazanova', 1,  '7/2/2018'),
('Ryazanova', 1,  '7/3/2018'),
('Ryazanova', 1,  '7/4/2018'),
('Ryazanova', 1,  '7/5/2018'),
('Kurov', 2,  '8/3/2018'),
('Kurov', 2,  '8/4/2018'),
('Kurov', 2,  '8/15/2018'),
('Kurov', 3,  '8/16/2018'),
('Kurov', 3,  '8/17/2018'),
('noname', 2,  '8/16/2018'),
('noname', 2,  '8/17/2018')
SELECT * FROM Vacation
SELECT * FROM EmployeeVacation
go



WITH
  dates AS (
    SELECT DISTINCT DateGAP, EmployeeName
    FROM EmployeeVacation
  ),
   
  groups AS (
    SELECT
      ROW_NUMBER() OVER (ORDER BY DateGAP) AS rn,
      dateadd(day, -ROW_NUMBER() OVER (PARTITION BY EmployeeName ORDER BY DateGAP), DateGAP) AS grp,
      DateGAP,
      EmployeeName
    FROM dates d
  )

SELECT
  MIN(g.EmployeeName) AS [Name],
  COUNT(*) AS vacationLenght,
  MIN(g.DateGAP) AS minDate,
  MAX(g.DateGAP) AS maxDate
FROM groups g
GROUP BY grp
ORDER BY 1 DESC, 2 DESC

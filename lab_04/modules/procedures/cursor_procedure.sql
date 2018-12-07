CREATE PROC CursorThingProc
AS

DECLARE @id INT
DECLARE @year DATE
DECLARE @Cursor CURSOR
SET @Cursor = CURSOR SCROLL
FOR
SELECT BuildId, [Year]
FROM CameraBuild

OPEN @Cursor
FETCH NEXT FROM @Cursor INTO @id, @year

CREATE TABLE #LocalTestTable
(
    MegaID INT PRIMARY KEY,
    [UltraYear] DATE,
)

WHILE @@FETCH_STATUS=0
BEGIN
    IF EXISTS(SELECT BuildId FROM CameraBuild WHERE BuildId = @id)
    BEGIN
        INSERT INTO #LocalTestTable (MegaID, UltraYear) VALUES(@id, @year)
    END
    FETCH NEXT FROM @Cursor INTO @id, @year
END
CLOSE @Cursor
SELECT * FROM #LocalTestTable
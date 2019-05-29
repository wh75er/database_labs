CREATE FUNCTION GetPrice(@year INT)
    RETURNS @ret TABLE (BuildingerId int primary key,
                        Price INT,
                        [Year] DATE)
    AS
    BEGIN
        INSERT @ret
        SELECT BuildId, Price, [Year]
        FROM CameraBuild
        WHERE YEAR([Year]) >= @year

        RETURN
    END
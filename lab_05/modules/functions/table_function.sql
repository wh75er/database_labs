CREATE FUNCTION AllBuildsAfterYear(@year INT)
    RETURNS TABLE
    AS RETURN (SELECT *
        FROM CameraBuild
        WHERE YEAR([Year]) >= @year)
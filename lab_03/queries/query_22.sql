WITH Modules_CTE (FilterId, LensId, [Year])
AS
(
    SELECT FilterId, LensId, [Year]
    FROM CameraBuild
)
SELECT COUNT(FilterId) AS 'Filters count'
FROM Modules_CTE
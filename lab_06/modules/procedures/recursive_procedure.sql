CREATE PROC RECURSIVE_THING
AS
BEGIN
WITH BuildCTE AS (
    SELECT BuildId, CameraBodyId, FilterId, 0 AS steps
    FROM CameraBuild
    WHERE BuildId = 7

    UNION ALL

    SELECT mgr.BuildId, mgr.CameraBodyId, mgr.FilterId, cmr.steps +1 AS steps
    FROM BuildCTE AS cmr
        INNER JOIN CameraBuild AS mgr
            ON mgr.BuildId = cmr.BuildId +1
    WHERE cmr.steps < 50
)SELECT * FROM BuildCTE AS u
END;

EXEC RECURSIVE_THING
INSERT CameraBuild(CameraBodyId, LensId, FilterId, Price, [Year])
SELECT (
        SELECT MAX(CameraId)
        FROM CameraBody
        WHERE Brand = 'Nikon'
), (
    SELECT TOP(1) LensId
    FROM Lens
), (
    SELECT TOP(1) FilterId
    FROM [Filter]
), Price, [Year]
FROM CameraBuild
WHERE BuildId=100

DELETE FROM CameraBuild
WHERE BuildId > 1000
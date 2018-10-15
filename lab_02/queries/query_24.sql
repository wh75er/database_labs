SELECT BuildId, CameraBodyId, FilterId, LensId, Price,
SUM(Price) OVER(PARTITION BY CameraBodyId) AS Total
FROM CameraBuild

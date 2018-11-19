SELECT BuildId, CameraBodyId, FilterId, LensId, Price,
SUM(Price) OVER(PARTITION BY CameraBodyId) AS Total,
COUNT(Price) OVER(PARTITION BY CameraBodyId) AS Amount
FROM CameraBuild
ORDER BY CameraBodyId
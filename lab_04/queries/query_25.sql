WITH CTE(CameraId, Brand, Model, Mount, Megapixels, Color)
AS(
SELECT TOP(5) * FROM CameraBody
UNION ALL
SELECT TOP(8) * FROM CameraBody
)
SELECT CameraId, Brand, Model, Mount, Megapixels, Color 
FROM (SELECT ROW_NUMBER() OVER(Partition By Model, CameraId ORDER BY CameraId) AS row#, * FROM CTE) AS b
WHERE b.row# = 1
ORDER BY CameraId
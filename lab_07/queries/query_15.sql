-- print list of kits which have sum less than 500 dollars
SELECT c1.BuildId, c2.Brand, c2.Model, c2.Color, SUM(c1.Price) AS 'Price'
FROM CameraBuild c1 JOIN CameraBody c2 ON c1.BuildId = c2.CameraId
GROUP BY BuildId, Brand, Model, Color
HAVING SUM(Price) < 500
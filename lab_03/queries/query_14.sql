-- return list of cameras, with average of megapixels
SELECT CameraId, Brand, Model, 
        Mount,
        AVG(Megapixels) AS 'AvgMegapixels'
FROM CameraBody
GROUP BY Brand, CameraId, Model, Mount
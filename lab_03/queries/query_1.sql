SELECT DISTINCT C1.Brand, C1.Model
FROM CameraBody C1
WHERE C1.CameraId >= 10 AND C1.CameraId <= 200 AND C1.Brand = 'AgfaPhoto'
ORDER BY C1.Brand, C1.Model
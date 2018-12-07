-- Получить список всех сборок, построенных на основе бренда Nikon, дороже 1к баксов
SELECT BuildId, CameraBodyId, Price
FROM CameraBuild
WHERE CameraBodyId IN
(
    SELECT CameraId
    FROM CameraBody
    WHERE Brand = 'Nikon'
) AND Price > 1000
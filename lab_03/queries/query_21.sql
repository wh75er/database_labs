DELETE FROM CameraBuild
WHERE BuildId IN 
(
    SELECT MAX(CameraId)
    FROM CameraBody
)
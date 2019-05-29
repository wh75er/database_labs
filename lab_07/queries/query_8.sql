-- Print list of release date with price and brand
SELECT [Year], Price,
(
    SELECT Brand
    FROM CameraBody
    WHERE CameraBody.CameraId = BuildId
) AS Brand
FROM CameraBuild
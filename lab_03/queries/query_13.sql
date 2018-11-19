SELECT Brand, Model
FROM CameraBody
WHERE CameraId = 
    (
    SELECT Yar
    FROM (  SELECT MAX(CameraBodyId) AS Yar
            FROM CameraBuild
            WHERE YEAR([Year]) = 
                (
                SELECT TOP(1) YEAR([Year])
                FROM CameraBuild
                WHERE Year([Year]) > 2000
                )
        ) AS MP
    )
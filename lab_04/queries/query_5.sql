-- Получить список сборок, в которых система гнезда для объективов использует линейку W...
SELECT BuildId, Price
FROM CameraBuild c1
WHERE EXISTS
(
    SELECT MountType, LensId
    FROM Lens
    WHERE MountType LIKE 'W%' AND c1.LensId = LensId
)
ORDER BY CameraBodyId

--SELECT c1.BuildId, c1.LensId, c2.MountType
--FROM CameraBuild c1 JOIN Lens c2 ON c1.LensId = c2.LensId
--WHERE c1.BuildId=115
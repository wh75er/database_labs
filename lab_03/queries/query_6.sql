-- Получить список китов, которые имеет цену дороже 150 кита
SELECT BuildId, Price, [Year]
FROM CameraBuild
WHERE Price > ALL
(
    SELECT Price
    FROM CameraBuild
    WHERE BuildId = 150
)
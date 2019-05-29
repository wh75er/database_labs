SELECT * FROM CameraBuild
WHERE Price < dbo.bestPrice(500)
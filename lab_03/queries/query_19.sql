UPDATE [Filter]
SET Diameter = 
(
    SELECT Diameter
    FROM Lens
    WHERE LensId = 10
)
WHERE FilterId = 1
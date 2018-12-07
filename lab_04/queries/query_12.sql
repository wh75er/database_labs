-- select Diameters of filters and lens for and model like yami
SELECT F.Diameter, L.Diameter, Model
FROM (Filter F JOIN Lens L ON F.FilterId = L.LensId) JOIN
(
    SELECT CameraId, Brand, Model, Megapixels
    FROM CameraBody
    WHERE Model LIKE '%Yami%'
) AS Test ON Test.CameraId = F.FilterId
WHERE L.Diameter > 50
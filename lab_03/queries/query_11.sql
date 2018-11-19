SELECT Brand, 
        SUM(Megapixels) AS SMG
INTO #BrandMegapixels
FROM CameraBody
GROUP BY Brand
SELECT DISTINCT c2.MountType, c1.CameraId, c1.Model, c2.LensId
FROM CameraBody c1 JOIN Lens c2 ON c1.Mount = c2.MountType
WHERE c1.Model LIKE 'Yami%'
ORDER BY c2.MountType
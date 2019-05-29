CREATE TRIGGER validate_changes_ins_cameraBody
On CameraBody
AFTER INSERT AS
BEGIN
    SELECT * FROM CameraBody
    WHERE CameraId = (SELECT MAX(CameraId) FROM CameraBody)
END;

INSERT CameraBody(Brand, Model, Mount, Megapixels, Color)
VALUES ('Nikon', 'Yami z16', 'ERP', 24, 'White')

DELETE FROM CameraBody
WHERE CameraId = (SELECT MAX(CameraId) FROM CameraBody) AND CameraId > 1000
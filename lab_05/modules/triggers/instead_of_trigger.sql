CREATE TRIGGER setInsteadOfDelete
ON CameraBody
INSTEAD OF DELETE
AS
UPDATE CameraBody
SET Megapixels = 9999
WHERE CameraId = (SELECT CameraId FROM deleted)

drop TRIGGER setInsteadOfDelete

INSERT CameraBody(Brand, Model, Mount, Megapixels, Color)
VALUES ('Nikon', 'Yami z16', 'ERP', 24, 'White')

DELETE FROM CameraBody
WHERE CameraId = (SELECT MAX(CameraId) FROM CameraBody) AND CameraId > 1000

SELECT *  FROM CameraBody WHERE CameraId = (SELECT MAX(CameraId) FROM CameraBody)
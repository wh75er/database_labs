SELECT * FROM dbo.CameraBody
FOR XML PATH;

SELECT * FROM dbo.CameraBody
FOR XML AUTO;

SELECT * FROM dbo.CameraBody
FOR XML RAW;

SELECT 1 AS Tag, NULL AS Parent,
BuildId AS [Build!1!id],
CameraBodyId AS [Build!1!BodyId!ELEMENT],
[Year] AS [Build!1!Year!ELEMENT]
FROM CameraBuild
FOR XML EXPLICIT, TYPE, ROOT('CameraBuild')
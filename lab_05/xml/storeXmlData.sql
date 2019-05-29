DECLARE @XML XML
DECLARE @hDoc INT
DECLARE @SQL NVARCHAR (MAX)

SELECT @XML = XMLData FROM XMLwithOpenXML


EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML

SELECT *
FROM OPENXML(@hDoc, 'ROOT/row')
WITH 
(
    CameraId INT 'CameraId',
    Brand [NVARCHAR](50) 'Brand',
    Model [NVARCHAR](50) 'Model',
    Mount [NVARCHAR](50) 'Mount',
    Megapixels INT 'Megapixels',
    [Color] [nvarchar](50) 'Color'
)


EXEC sp_xml_removedocument @hDoc

IF(EXISTS(select * from sys.objects where name = 'XMLwithOpenXML'))
    DROP TABLE XMLwithOpenXML

CREATE TABLE XMLwithOpenXML
(
Id INT IDENTITY PRIMARY KEY,
XMLData XML,
LoadedDateTime DATETIME
)

INSERT INTO XMLwithOpenXML(XMLData, LoadedDateTime)
SELECT CONVERT(XML, BulkColumn) AS BulkColumn, GETDATE() 
FROM OPENROWSET(BULK '/opt/mssql/data/cameraBodyData.xml', SINGLE_BLOB) AS x;

SELECT * FROM XMLwithOpenXML
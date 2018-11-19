BULK INSERT [cameraBody]
FROM '/opt/mssql/data/cameraBody_data'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = '|', ROWTERMINATOR = '0x0a');
GO

BULK INSERT [Filter]
FROM '/opt/mssql/data/filter_data'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = '|', ROWTERMINATOR = '0x0a');
GO

BULK INSERT [Lens]
FROM '/opt/mssql/data/lens_data'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = '|', ROWTERMINATOR = '0x0a');
GO

BULK INSERT [CameraBuild]
FROM '/opt/mssql/data/cameraBuild_data'
WITH (DATAFILETYPE = 'char', FIRSTROW = 1, FIELDTERMINATOR = '|', ROWTERMINATOR = '0x0a');
GO
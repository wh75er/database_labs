CREATE PROC WHAT_DB_IS_THAT 
    @TB_NAME nvarchar(128)
AS
exec('select index_id, ind.object_id, t.name from sys.indexes ind
        INNER JOIN sys.tables t ON ind.object_id = t.object_id
        where ind.object_id=OBJECT_ID(''' + @TB_NAME + ''')') 
GO


EXEC WHAT_DB_IS_THAT 'CameraBody';
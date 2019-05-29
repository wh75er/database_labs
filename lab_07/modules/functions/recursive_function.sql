CREATE FUNCTION getPriceRec (@id int)  
RETURNS @ret TABLE (BuildingerId int primary key,
  Price INT,
  [Year] DATE)
AS
BEGIN 
  IF EXISTS(SELECT BuildId, Price, [Year] FROM CameraBuild WHERE BuildId = @id)
  BEGIN
  INSERT INTO @ret
  SELECT BuildId, Price, [Year] FROM CameraBuild WHERE BuildId = @id
  INSERT INTO @ret
  SELECT BuildingerId, Price, [Year] FROM getPriceRec(@id+1)
  END;
  
  RETURN
END
go

select * from getPriceRec(998)
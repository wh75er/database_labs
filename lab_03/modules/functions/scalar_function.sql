-- Scalar function presented 

CREATE FUNCTION bestPrice (@topCheck INT = 5)
    RETURNS DECIMAL(16, 2)
    BEGIN
        DECLARE @lowestPrice DEC(14, 2)
        SELECT @lowestPrice = MIN(tbl.Price)
        FROM (
            SELECT TOP(@topCheck) *
            FROM CameraBuild
        ) AS tbl
        RETURN @lowestPrice
    END;
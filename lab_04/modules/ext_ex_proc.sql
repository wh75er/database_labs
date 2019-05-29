CREATE PROC removeDubles
AS
DELETE a
FROM
(
    SELECT row_num = ROW_NUMBER() OVER(PARTITION BY b.ID ORDER BY b.ID)
    FROM LocalTable b
) a
WHERE a.row_num > 1

SELECT * FROM LocalTable ORDER BY ID
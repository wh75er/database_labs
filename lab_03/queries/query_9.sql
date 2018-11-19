-- print list of product urgency
SELECT BuildId, Price,
CASE --YEAR([Year])
    WHEN YEAR([YEAR]) = YEAR(GETDATE())-1 THEN 'Last year'
    WHEN YEAR([Year]) < 2009 THEN 'LEGACY'
    ELSE 'More than one year'
END AS 'Urgency'
FROM CameraBuild
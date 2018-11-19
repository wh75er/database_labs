SELECT BuildId, [Year], Price,
CASE
    WHEN Price < 500 THEN 'Strange'
    WHEN Price < 900 THEN 'Sweety'
    WHEN Price < 1000 THEN 'Meh'
    ELSE '>.<'
END AS 'Price description'
FROM CameraBuild
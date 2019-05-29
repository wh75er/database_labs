--IF OBJECT_ID('#Table1', 'U') IS NOT NULL
DROP TABLE #Table1
--IF OBJECT_ID('#Table2', 'U') IS NOT NULL
DROP TABLE #Table2

CREATE TABLE #Table1
(
    id INT,
    var1 [nvarchar](5) NOT NULL,
    valid_from_dttm date NOT NULL,
    valid_to_dttm date NOT NULL
)
CREATE TABLE #Table2
(
    id INT,
    var2 [nvarchar](5) NOT NULL,
    valid_from_dttm date NOT NULL,
    valid_to_dttm date NOT NULL
)

INSERT INTO #Table1 VALUES(1, 'A', '2018-09-01', '2018-09-15')
INSERT INTO #Table1 VALUES(1, 'B', '2018-09-16', '5999-12-31')

INSERT INTO #Table2 VALUES(1, 'A', '2018-09-01', '2018-09-18')
INSERT INTO #Table2 VALUES(1, 'B', '2018-09-19', '5999-12-31')

SELECT * FROM #Table1
SELECT * FROM #Table2

SELECT id, var1, var2, valid_from_dttm, valid_to_dttm
FROM (SELECT id, var1, valid_from_dttm, valid_to_dttm, ROW_NUMBER() OVER(ORDER BY var1) AS row FROM #Table1) a
JOIN
(SELECT var2, ROW_NUMBER() OVER(ORDER BY var2) AS row FROM #Table2) b
ON a.row = b.row
WHERE a.row = 1

UNION ALL

SELECT id, var1, var2, valid_from_dttm, valid_to_dttm
FROM (SELECT id, var1, valid_from_dttm, ROW_NUMBER() OVER(ORDER BY var1) AS row FROM #Table1) a
JOIN
(SELECT var2, valid_to_dttm, ROW_NUMBER() OVER(ORDER BY var2) AS row FROM #Table2) b
ON a.row = b.row+1

UNION ALL

SELECT id, var1, var2, valid_from_dttm, valid_to_dttm
FROM (SELECT id, var1, ROW_NUMBER() OVER(ORDER BY var1) AS row FROM #Table1) a
JOIN
(SELECT var2, valid_from_dttm, valid_to_dttm, ROW_NUMBER() OVER(ORDER BY var2) AS row FROM #Table2) b
ON a.row = b.row
WHERE a.row = (SELECT COUNT(id) FROM #Table2)
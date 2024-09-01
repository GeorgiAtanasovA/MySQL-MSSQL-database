SELECT `name` AS 'Game',
CASE
     WHEN `duration` <= 3 THEN 'Smaller'
     ELSE 'Extra Long'
END AS 'Duration'
FROM `games`;
 
 
SELECT *, LEFT(RIGHT(`start`, 16), 9) AS 'Part of the day' FROM `games`;
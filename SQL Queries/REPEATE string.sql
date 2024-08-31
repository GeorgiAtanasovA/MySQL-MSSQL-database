CREATE TABLE t_text (
    c1 VARCHAR(255) NULL DEFAULT 'AAA',
    c2 TINYTEXT
);

INSERT INTO t_text VALUE(REPEAT('A', 50), REPEAT('B', 30));

SELECT 
   c1, 
   CHAR_LENGTH(c1), 
   LENGTH(c1), 
   c2, 
   CHAR_LENGTH(c2),
   LENGTH(c2)   
FROM 
   t_text;
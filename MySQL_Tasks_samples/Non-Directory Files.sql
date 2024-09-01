-- Some of the files are Directories, because they are a parent to some file. 
-- Find those, which aren’t.

USE `buhtig`;

SELECT d.id, d.name, concat(d.size, ' KB') AS 'size'
FROM files f
RIGHT JOIN files d ON f.parent_id = d.id
WHERE f.id IS NULL
ORDER BY d.id;
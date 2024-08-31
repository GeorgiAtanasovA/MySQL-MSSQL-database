-- There are some pretty big HTML files in the Buhtig database… Unnaturally big. 
-- Extract from the database all of the files, which have size, GREATER than 1000, and their name contains “html”.

USE `buhtig`;

SELECT `id`, `name`, `size`
FROM `files`
WHERE `size` > 1000 AND `name` LIKE '%html%'
ORDER BY `size` DESC;

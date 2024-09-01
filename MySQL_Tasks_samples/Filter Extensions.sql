#16. Filter Extensions
#Create a stored procedure udp_findbyextension which accepts the following parameters:
# extension
#And extracts all files that have the given extension. (like index.html for example)

USE `buhtig`;

DROP PROCEDURE IF EXISTS udp_findbyextension;
DELIMITER $$
CREATE PROCEDURE udp_findbyextension(`extension` VARCHAR(100))
BEGIN 
   SELECT `id`, `name`, CONCAT(`size`, 'KB') AS `size` 
   FROM `files` 
   WHERE name LIKE CONCAT('%.', `extension`)
   ORDER BY `id`;
END; $$
DELIMITER $$

CALL udp_findbyextension('html');

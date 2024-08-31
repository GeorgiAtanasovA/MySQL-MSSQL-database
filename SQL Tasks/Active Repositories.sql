-- Active Repositories
-- Extract from the database, the top 5 repositories, in terms of count of issues on them.

USE `buhtig`;

SELECT 
   `repositories`.`id`, 
   `repositories`.`name`, 
   COUNT(`issues`.`repository_id`) AS `issues`
FROM `issues` 
JOIN 
`repositories` ON `repositories`.`id` = `issues`.`repository_id`
GROUP BY `issues`.`repository_id`
ORDER BY `issues` DESC , `repositories`.`id` LIMIT 5;
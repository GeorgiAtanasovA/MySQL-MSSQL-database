UPDATE `employees` SET `salary` = `salary` * 1.12 -- 1.12 - отговаря на 12%
WHERE
`job_title` LIKE '%Engineering%' OR
`job_title` LIKE '%Tool Designer%' OR
`job_title` LIKE '%Marketing%'
ORDER BY `salary`;
-- Намира всички работници, които имат в длъжността си дума от изброените, и им сменя заплатата
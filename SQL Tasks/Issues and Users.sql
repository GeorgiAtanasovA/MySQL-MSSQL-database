-- Extract from the database, all of the issues, and the users that are assigned to them, so that they end up in the following format:

USE `buhtig`;

SELECT `issues`.`id`, CONCAT(`username`, ': ', `title`) AS 'issue_assignee'
FROM `issues`
JOIN `users` ON `users`.`id` = `issues`.`assignee_id`
ORDER BY `issues`.`id` DESC;
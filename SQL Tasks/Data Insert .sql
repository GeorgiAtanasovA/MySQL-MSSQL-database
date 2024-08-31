-- INSERT records of data into the issues table, based on the files table. 

USE `buhtig`;

INSERT INTO `issues` (`title`, `issue_status`, `repository_id`, `assignee_id`)
SELECT
     CONCAT('Critical Problem With ', `name`,'!') AS 'title', 
     'open' AS 'issue_status', 
     CEILING(files.`id` * 2 / 3) AS 'repository_id',
     contributor_id 
FROM `files` 
JOIN commits ON commits.id = files.commit_id
WHERE `files`.`id` BETWEEN 46 AND 50;
  
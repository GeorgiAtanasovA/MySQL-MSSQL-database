-- Extract from the database, for every user – the count of commits he has on issues that were assigned to him.

USE `buhtig`;
   
SELECT 
     users.id, 
     users.username, 
    (
       SELECT COUNT(commits.id) FROM commits
         JOIN issues 
           ON issues.id = commits.issue_id
        WHERE issues.assignee_id = users.id 
          AND commits.contributor_id = users.id
    ) AS `commits`
FROM users
ORDER BY `commits` DESC, users.id;

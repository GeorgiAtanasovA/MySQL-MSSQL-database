-- Data Deletion

USE `buhtig`;
-- option 1
DELETE repositories FROM repositories
LEFT JOIN issues ON issues.repository_id = repositories.id
WHERE issues.id IS NULL;

-- option 2
DELETE FROM repositories 
WHERE id NOT IN ( SELECT repository_id FROM issues ); 


SELECT id FROM repositories 
WHERE id NOT IN ( SELECT repository_id FROM issues ); 

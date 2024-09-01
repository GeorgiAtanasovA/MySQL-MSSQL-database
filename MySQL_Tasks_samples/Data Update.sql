-- UPDATE all contributors to repositories which have the same id (value) as the repository they contribute to.
-- SET them as a contributor to the repository with the lowest id (by value) which has no contributors.
-- If there aren’t any repositories with no contributors do nothing.

USE `buhtig`;

INSERT INTO repositories_contributors( contributor_id, repository_id)
SELECT * FROM 
   (SELECT contributor_id FROM repositories_contributors
   WHERE contributor_id = repository_id) AS t1
CROSS JOIN 
   (SELECT MIN(repositories.id) AS 'repository_id' FROM repositories
   LEFT JOIN repositories_contributors 
      ON repositories.id = repositories_contributors.repository_id
   WHERE repositories_contributors.repository_id IS NULL) AS t2
WHERE t2.repository_id IS NOT NULL;
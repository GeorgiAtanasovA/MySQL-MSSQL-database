DELETE FROM minionsT WHERE name = 'Bop';

ALTER TABLE data_types_examples DROP some_string_test;

DELETE repositories 
FROM   repositories
LEFT JOIN issues 
       ON issues.repository_id = repositories.id
WHERE issues.id IS NULL;
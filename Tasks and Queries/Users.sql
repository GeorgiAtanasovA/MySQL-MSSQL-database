-- Extract from the database, all of the users.
-- ORDER the results ascending by user id.

USE `buhtig`;

SELECT 
    `id`, `username`
FROM
    `users`
ORDER BY `id` ASC;
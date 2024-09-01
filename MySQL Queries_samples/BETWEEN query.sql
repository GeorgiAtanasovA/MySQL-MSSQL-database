SELECT `first_name`, `last_name`, `job_title`, `salary`
FROM employees
WHERE`salary` BETWEEN 20000 AND 30000
ORDER BY `salary`;
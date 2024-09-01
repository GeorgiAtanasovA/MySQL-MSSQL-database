SELECT CONCAT(`first_name`, ' . ',`last_name`, '@softuni.bg') 
AS 'Full email address'
FROM employees
ORDER BY first_name;
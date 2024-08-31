DELIMITER $$
CREATE PROCEDURE usp_some_procedure_test()
BEGIN
    SELECT `employee_id`, `first_name`, `last_name`
    FROM employees
    WHERE Salary > 10;
END; $$
DELIMITER $$
-- ----------------------------------------
CALL usp_some_procedure_test(); 
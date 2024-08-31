USE `soft_uni`;

DELIMITER $$
CREATE FUNCTION ufn_get_salary_level(`some_employee_salary` DECIMAL(19,4))
RETURNS VARCHAR(20)


BEGIN
   DECLARE `salary_level` VARCHAR(20);
   
   IF(`some_employee_salary` < 35000) THEN SET `salary_level` := 'low';
      ELSEIF (`some_employee_salary` >= 30000 AND `some_employee_salary` <= 50000 ) THEN SET `salary_level` := 'Average';
      ELSE SET `salary_level` := 'High';
   END IF;
   
      RETURN `salary_level`;
END; $$
DELIMITER $$
 

SELECT `salary`,
        UFN_GET_SALARY_LEVEL(`salary`) AS 'salary_level'
FROM `employees`
ORDER BY `first_name`, `last_name`;
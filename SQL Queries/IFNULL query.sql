
CREATE VIEW `v_employees_job_titles` AS
    SELECT 
        CONCAT(`first_name`, ' ', IF(`middle_name` IS NULL, '-', `middle_name`), ' ', `last_name`) AS 'Name',
        `job_title`
    FROM
        `employees`;
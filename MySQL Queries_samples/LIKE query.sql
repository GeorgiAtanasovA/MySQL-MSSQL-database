LIKE Operator Description

'a%'	Finds any values that start with "a"
'%a'	Finds any values that end with "a"
'%or%'	Finds any values that have "or" in any position
'_r%'	Finds any values that have "r" in the second position
'a_%'	Finds any values that start with "a" and are at least 2 characters in length
'a__%'	Finds any values that start with "a" and are at least 3 characters in length
'a%o'	Finds any values that start with "a" and ends with "o"

UPDATE `employees` SET `salary` = `salary` * 1.12 
WHERE
`job_title` LIKE '%Engineering%' OR
`job_title` LIKE '%Tool Designer%' OR
`job_title` LIKE '%Marketing%'
ORDER BY `salary`;
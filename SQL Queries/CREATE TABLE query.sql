CREATE TABLE minions
(
id INT NOT NULL,
name VARCHAR(50) NOT NULL,
age INT,
CONSTRAINT pk_minions PRIMARY KEY(id)
);


CREATE TABLE townsProbno
(
id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
`name` VARCHAR(50),
age INT
);

select * from townsProbno;
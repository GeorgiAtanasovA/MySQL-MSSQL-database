CREATE DATABASE datetime_db;
use datetime_db; 

USE `orders`;
SELECT `product_name`, `order_date`,
date_add(`order_date`, interval(3) day) as 'pay_due'
FROM `orders`;

CREATE TABLE sm_dates (
    t1 DATE,
    t2 TIME,
    t3 DATETIME,
    t4 TIMESTAMP
);
-- смяна на часовата зона
set time_zone = '+02:00';-- България
set time_zone = '+01:00';-- Швеция

INSERT INTO sm_dates VALUES('2020-11-18', '16:38:30', now(), now());
INSERT INTO sm_dates VALUES(now(), now(), now(), now());

SELECT t3,t4 FROM sm_dates;

DROP TABLE sm_dates;
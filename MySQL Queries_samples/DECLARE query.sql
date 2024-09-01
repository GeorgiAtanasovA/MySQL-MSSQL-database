DELIMITER $$
CREATE PROCEDURE usp_withdraw_money(account_id INT, money_amount DECIMAL(20, 4))
BEGIN
      DECLARE `error_message` VARCHAR(200);
      SET `error_message` = 
      CONCAT(
               'Error...Not enough money! ', 
               'Account balance: ', (SELECT `balance` FROM `accounts` WHERE `id` = account_id), ' лв. ', 
               'Money withdraw: ', money_amount
             );
-- ...write_some_code...
   END; $$
   DELIMITER $$
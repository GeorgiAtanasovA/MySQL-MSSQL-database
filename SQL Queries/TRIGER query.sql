
DELIMITER $$
CREATE PROCEDURE usp_withdraw_money(account_id INT, money_amount DECIMAL(20, 4))
BEGIN
START TRANSACTION;
   IF (money_amount < 10) THEN
      SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error...Withdraw bigger amount!!!';
      ROLLBACK;
   ELSEIF ((SELECT `balance` FROM `accounts` WHERE `id` = account_id) < 10) THEN
      SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error...Not enough money!!!';
      ROLLBACK;
   ELSEIF (((SELECT `balance` FROM `accounts` WHERE `id` = account_id) - money_amount) < 10) THEN
      SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Error...Too big sum!!!';
      ROLLBACK;
   ELSE 
      UPDATE `accounts` SET `balance` = `balance` - money_amount WHERE `id` = account_id;
      COMMIT;
      SELECT * FROM `accounts`;
   END IF;
END; $$
DELIMITER $$


CREATE TABLE `logs_table` ( 
    log_id INT PRIMARY KEY AUTO_INCREMENT,
    account_id INT,
    old_sum DECIMAL(20 , 4),
    new_sum DECIMAL(20 , 4)
);


DELIMITER $$
CREATE TRIGGER tr_log_accounts_trigger AFTER UPDATE ON `accounts` FOR EACH ROW
BEGIN
   INSERT INTO `logs_table`(account_id, old_sum, new_sum) VALUES(old.`id`, old.`balance`, new.`balance`);
END; $$
DELIMITER $$

CALL usp_withdraw_money(1, 10.0000); 
SELECT * FROM `logs_table`; 
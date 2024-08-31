-- 1. Задача - дооправяне на таблици и връзките

USE `buhtig`;
-- -----------
ALTER TABLE `users` CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`);
-- -----------
ALTER TABLE `repositories` CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`);
-- -----------
ALTER TABLE `repositories_contributors` ADD CONSTRAINT `fk_repositories_contributors_Repositories` FOREIGN KEY (`repository_id`) REFERENCES `repositories`(`id`);
ALTER TABLE `repositories_contributors` ADD CONSTRAINT `fk_repositories_contributors_Users` FOREIGN KEY (`contributor_id`) REFERENCES `users`(`id`);
-- -----------
ALTER TABLE `issues` CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`);
ALTER TABLE `issues` ADD CONSTRAINT `fk_issues_repositories` FOREIGN KEY (`repository_id`) REFERENCES `repositories` (`id`);
ALTER TABLE `issues` ADD CONSTRAINT `fk_issues_users` FOREIGN KEY (`assignee_id`) REFERENCES `users` (`id`);
-- -----------
ALTER TABLE `commits` CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`);
ALTER TABLE `commits` ADD CONSTRAINT `fk_commits_issues` FOREIGN KEY (`issue_id`) REFERENCES `issues` (`id`);
ALTER TABLE `commits` ADD CONSTRAINT `fk_commits_repositories` FOREIGN KEY (`repository_id`) REFERENCES `repositories` (`id`);
ALTER TABLE `commits` ADD CONSTRAINT `fk_commits_users` FOREIGN KEY (`contributor_id`) REFERENCES `users` (`id`);
-- -----------
ALTER TABLE `files` CHANGE COLUMN `id` `id` INT NOT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`);
ALTER TABLE `files` ADD CONSTRAINT `fk_files_files` FOREIGN KEY (`parent_id`) REFERENCES `files` (`id`);
ALTER TABLE `files` ADD CONSTRAINT `fk_files_commits` FOREIGN KEY (`commit_id`) REFERENCES `commits` (`id`);
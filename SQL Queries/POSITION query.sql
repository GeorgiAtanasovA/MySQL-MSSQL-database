SELECT 
   `email` AS 'test_col', 
    position('@' IN `email`) AS '@-ta e na pozicia'
FROM `users`;
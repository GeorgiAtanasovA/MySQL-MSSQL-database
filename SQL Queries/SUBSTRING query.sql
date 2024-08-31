SELECT                     -- `index`
     `email`, substring(`email`, 6, 11) AS 'some_substring' 
FROM `users`;

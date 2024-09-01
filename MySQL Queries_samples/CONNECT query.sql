-- connect tables
ALTER TABLE minions ADD towns_id INT; 

ALTER TABLE minions 
ADD CONSTRAINT fk_minions_towns FOREIGN KEY(towns_id)
REFERENCES towns(id);

select * from towns;
select * from minions;
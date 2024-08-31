CREATE TABLE towns_Probno
(
id INT NOT NULL AUTO_INCREMENT,
name varchar(50),
oblast varchar(50)
CONSTRAINT pk_towns_Probno PRIMARY KEY(id)
);

INSERT INTO towns_Probno(name,oblast) VALUES('Varna1','Burgas1');
INSERT INTO towns_Probno(name,oblast) VALUES('Varna2','Burgas2');
INSERT INTO towns_Probno(name,oblast) VALUES('Varna3','Burgas3');
INSERT INTO towns_Probno(name,oblast) VALUES('Varna4','Burgas4');
INSERT INTO towns_Probno(name,oblast) VALUES('Varna5','Burgas5');

SELECT * FROM towns_Probno;
INSERT INTO Countries(Name, Population, Surface,ContinentId) VALUES(
'Belgium', 23400, 44500, 3);
SELECT * FROM countries;
INSERT INTO Countries(Name, Population, Surface,ContinentId) VALUES(
'Portugal', 23409, 22000, 3);
SELECT * FROM countries;
INSERT INTO Countries(Name, Population, Surface,ContinentId) VALUES(
'Spain', 45678, 23456, 3);
SELECT * FROM countries;
UPDATE countries SET Population = 11111 WHERE id=3;
DELETE FROM countries WHERE id = 3;
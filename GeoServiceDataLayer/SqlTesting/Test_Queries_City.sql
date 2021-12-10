  INSERT INTO Cities(Name, Population, CountryId, Capital)
  VALUES('Olsene', 345, '5', 1);
 INSERT INTO Cities(Name, Population, CountryId, Capital)
  VALUES('Zingem', 1200, '5', 0);
  INSERT INTO Cities(Name, Population, CountryId, Capital)
  VALUES('Mater', 4567, '5', 1);
  SELECT * FROM Cities;
  UPDATE Cities SET Name='Gavere' WHERE id=3;
  DELETE FROM Cities WHERE id=1;
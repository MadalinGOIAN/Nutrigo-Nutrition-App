ALTER TABLE `bd_licenta`.`alimente` 
CHANGE COLUMN `denumire` `denumire` VARCHAR(50) NOT NULL ;

ALTER TABLE `bd_licenta`.`istoric` 
DROP FOREIGN KEY `fk_denumire_aliment`;
ALTER TABLE `bd_licenta`.`istoric` 
CHANGE COLUMN `denumire_aliment` `denumire_aliment` VARCHAR(50) NOT NULL ;
ALTER TABLE `bd_licenta`.`istoric` 
ADD CONSTRAINT `fk_denumire_aliment`
  FOREIGN KEY (`denumire_aliment`)
  REFERENCES `bd_licenta`.`alimente` (`denumire`);

ALTER TABLE `bd_licenta`.`utilizatori` 
CHANGE COLUMN `hash_parola` `hash_parola` VARCHAR(50) NOT NULL ;

ALTER TABLE `bd_licenta`.`utilizatori` 
CHANGE COLUMN `hash_parola` `hash_parola` VARCHAR(50) NOT NULL ;

CREATE SCHEMA `db_licenta`;

CREATE TABLE `db_licenta`.`utilizatori` (
  `nume_utilizator` VARCHAR(30) NOT NULL,
  `hash_parola` VARCHAR(45) NOT NULL,
  `prenume` VARCHAR(45) NOT NULL,
  `nume_familie` VARCHAR(45) NOT NULL,
  `sex` CHAR(1) NOT NULL,
  `varsta` INT UNSIGNED NOT NULL,
  `inaltime` INT UNSIGNED NOT NULL,
  `greutate` INT UNSIGNED NOT NULL,
  `nivel_activitate_fizica` INT UNSIGNED NOT NULL,
  `necesar_caloric` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`nume_utilizator`));

CREATE TABLE `db_licenta`.`utilizatori_conectati` (
  `nume_utilizator_conectat` VARCHAR(30) NOT NULL,
  `hash_parola` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`nume_utilizator_conectat`),
  CONSTRAINT `fk_nume_utilizator`
    FOREIGN KEY (`nume_utilizator_conectat`)
    REFERENCES `db_licenta`.`utilizatori` (`nume_utilizator`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE `db_licenta`.`alimente` (
  `denumire` VARCHAR(30) NOT NULL,
  `cod_bare` VARCHAR(13) NULL,
  `calorii` INT NOT NULL,
  `grasimi` FLOAT NOT NULL,
  `glucide` FLOAT NOT NULL,
  `proteine` FLOAT NOT NULL,
  PRIMARY KEY (`denumire`));

CREATE TABLE `db_licenta`.`istoric` (
  `istoric_id` INT NOT NULL AUTO_INCREMENT,
  `nume_utilizator` VARCHAR(30) NOT NULL,
  `denumire_aliment` VARCHAR(30) NOT NULL,
  `data` DATE NOT NULL,
  `cantitate_consumata` FLOAT NOT NULL,
  `calorii_consumate` INT NOT NULL,
  `grasimi_consumate` FLOAT NOT NULL,
  `glucide_consumate` FLOAT NOT NULL,
  `proteine_consumate` FLOAT NOT NULL,
  PRIMARY KEY (`istoric_id`),
  INDEX `fk_nume_utilizator_idx` (`nume_utilizator` ASC) VISIBLE,
  INDEX `fk_denumire_aliment_idx` (`denumire_aliment` ASC) VISIBLE,
  CONSTRAINT `fk_istoric_nume_utilizator`
    FOREIGN KEY (`nume_utilizator`)
    REFERENCES `db_licenta`.`utilizatori` (`nume_utilizator`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_denumire_aliment`
    FOREIGN KEY (`denumire_aliment`)
    REFERENCES `db_licenta`.`alimente` (`denumire`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE SCHEMA `bd_licenta` ;

CREATE TABLE `bd_licenta`.`utilizatori` (
  `nume_utilizator` varchar(30) NOT NULL,
  `hash_parola` varchar(45) NOT NULL,
  `prenume` varchar(45) NOT NULL,
  `nume_familie` varchar(45) NOT NULL,
  `sex` char(1) NOT NULL,
  `varsta` int unsigned NOT NULL,
  `inaltime` int unsigned NOT NULL,
  `greutate` int unsigned NOT NULL,
  `nivel_activitate_fizica` int unsigned NOT NULL,
  `necesar_caloric` int unsigned NOT NULL,
  PRIMARY KEY (`nume_utilizator`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `bd_licenta`.`utilizatori_conectati` (
  `nume_utilizator_conectat` varchar(30) NOT NULL,
  `hash_parola` varchar(45) NOT NULL,
  PRIMARY KEY (`nume_utilizator_conectat`),
  CONSTRAINT `fk_nume_utilizator` FOREIGN KEY (`nume_utilizator_conectat`) REFERENCES `bd_licenta`.`utilizatori` (`nume_utilizator`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `bd_licenta`.`alimente` (
  `denumire` varchar(30) NOT NULL,
  `cod_bare` varchar(13) DEFAULT NULL,
  `calorii` int NOT NULL,
  `grasimi` float NOT NULL,
  `glucide` float NOT NULL,
  `proteine` float NOT NULL,
  PRIMARY KEY (`denumire`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `bd_licenta`.`istoric` (
  `istoric_id` int NOT NULL AUTO_INCREMENT,
  `nume_utilizator` varchar(30) NOT NULL,
  `denumire_aliment` varchar(30) NOT NULL,
  `data` date NOT NULL,
  `cantitate_consumata` float NOT NULL,
  `calorii_consumate` int NOT NULL,
  `grasimi_consumate` float NOT NULL,
  `glucide_consumate` float NOT NULL,
  `proteine_consumate` float NOT NULL,
  PRIMARY KEY (`istoric_id`),
  KEY `fk_nume_utilizator_idx` (`nume_utilizator`),
  KEY `fk_denumire_aliment_idx` (`denumire_aliment`),
  CONSTRAINT `fk_denumire_aliment` FOREIGN KEY (`denumire_aliment`) REFERENCES `bd_licenta`.`alimente` (`denumire`),
  CONSTRAINT `fk_istoric_nume_utilizator` FOREIGN KEY (`nume_utilizator`) REFERENCES `bd_licenta`.`utilizatori` (`nume_utilizator`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

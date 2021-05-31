-- MySQL dump 10.15  Distrib 10.0.28-MariaDB, for debian-linux-gnueabihf (armv7l)
--
-- Host: cinemadb    Database: cinemadb
-- ------------------------------------------------------
-- Server version       10.0.28-MariaDB-2+b1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `acteur`
--

DROP TABLE IF EXISTS `acteur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `acteur` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(45) NOT NULL,
  `prenom` varchar(45) NOT NULL,
  `langue` varchar(45) NOT NULL,
  `date_naissance` date NOT NULL,
  `lieu_naissance` varchar(75) NOT NULL,
  `image` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `acteur`
--

LOCK TABLES `acteur` WRITE;
/*!40000 ALTER TABLE `acteur` DISABLE KEYS */;
INSERT INTO `acteur` VALUES (1,'Diesel','Vin','Anglais','1967-07-18','Comté d\'Alameda, Californie, États-Unis','https://i.imgur.com/p6KM36x.png'),(3,'Carrier','Tristan','Muet','1999-01-01','Beauport Beach','https://i.imgur.com/0n1xAd0.png'),(4,'Gaston','Lamouche','Indien','2020-05-04','Le pacific','https://i.imgur.com/9p2yIp8.png'),(6,'Depp','Jhonny','Anglais','1963-06-09','Owensboro, Kentucky','https://i.imgur.com/seqchTg.png'),(7,'Robert','Jackson','Anglais','2020-05-07','fredo','https://i.imgur.com/XBKPkGe.png'),(14,'CArrier','Tristan','Anglais','2020-05-01','Beauport','https://i.imgur.com/SzOdi2o.png'),(15,'hhhhhh','retert','eert','2020-05-19','hhhh','https://i.imgur.com/seqchTg.png');
/*!40000 ALTER TABLE `acteur` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cinema`
--

DROP TABLE IF EXISTS `cinema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cinema` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(100) NOT NULL,
  `adresse` varchar(150) NOT NULL,
  `telephone` varchar(30) NOT NULL,
  `id_responsable` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idResponsable` (`id_responsable`),
  CONSTRAINT `idResponsable` FOREIGN KEY (`id_responsable`) REFERENCES `responsable` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cinema`
--

LOCK TABLES `cinema` WRITE;
/*!40000 ALTER TABLE `cinema` DISABLE KEYS */;
INSERT INTO `cinema` VALUES (1,'Odéon','1666, Autoroute 540, Québec, G2G 2B5','(418) 871-1666',1),(5,'Lévis-Paul','66 Route du Président-Kennedy, Lévis, QC G6V 6C5','(418) 837-1111',2),(11,'La Notion','12 Avenue Limoilou','(581) 445-7789',4),(35,'Programmation','13 rue des fleurs','567-999-0879',28),(37,'lévis','405routedufleuve','418835-0975',30);
/*!40000 ALTER TABLE `cinema` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `directeur`
--

DROP TABLE IF EXISTS `directeur`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `directeur` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(45) NOT NULL,
  `prenom` varchar(45) NOT NULL,
  `langue` varchar(45) NOT NULL,
  `date_naissance` date NOT NULL,
  `lieu_naissance` varchar(75) NOT NULL,
  `image` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `directeur`
--

LOCK TABLES `directeur` WRITE;
/*!40000 ALTER TABLE `directeur` DISABLE KEYS */;
INSERT INTO `directeur` VALUES (1,'Cohen','Rob','Anglais','1949-03-12','Cornwall','https://i.imgur.com/9CiLCBZ.png'),(2,'Gaetan','LaBiche','Tokebak','2020-05-04','Kébak','https://i.imgur.com/54G6Kvq.png'),(5,'Lamotte','Fredo','Franglais','2020-05-04','fredo','https://i.imgur.com/okSrQEt.png'),(6,'Jean','Benoit','Français','1962-10-18','France, Paris','https://i.imgur.com/tVTVlzn.png'),(14,'Carrier','Tristan','Anglais','2020-05-01','Beauport','https://i.imgur.com/SzOdi2o.png');
/*!40000 ALTER TABLE `directeur` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `direction`
--

DROP TABLE IF EXISTS `direction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `direction` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_directeur` int(11) NOT NULL,
  `id_film` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idFilmDirection` (`id_film`),
  KEY `idDirecteurDirection` (`id_directeur`),
  CONSTRAINT `idDirecteurDirection` FOREIGN KEY (`id_directeur`) REFERENCES `directeur` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `idFilmDirection` FOREIGN KEY (`id_film`) REFERENCES `film` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `direction`
--

LOCK TABLES `direction` WRITE;
/*!40000 ALTER TABLE `direction` DISABLE KEYS */;
INSERT INTO `direction` VALUES (6,2,19),(23,2,1),(29,14,145),(30,5,144);
/*!40000 ALTER TABLE `direction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `film`
--

DROP TABLE IF EXISTS `film`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `film` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `titre` varchar(75) NOT NULL,
  `description` varchar(500) NOT NULL,
  `annee` int(11) NOT NULL,
  `duree` int(11) NOT NULL,
  `note` double DEFAULT NULL,
  `nbr_votes` int(11) DEFAULT NULL,
  `revenue` double DEFAULT NULL,
  `id_genre` int(11) NOT NULL,
  `image` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `idGenreFilm` (`id_genre`),
  CONSTRAINT `idGenreFilm` FOREIGN KEY (`id_genre`) REFERENCES `genre` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=147 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `film`
--

LOCK TABLES `film` WRITE;
/*!40000 ALTER TABLE `film` DISABLE KEYS */;
INSERT INTO `film` VALUES (1,'Rapide et dangereux 2','Après avoir laissé s\'échapper le fugitif Dominic Toretto, l\'ancien officier de police Brian O\'Conner est maintenant lui aussi en cavale et quitte Los Angeles pour Miami afin de commencer une nouvelle vie. Mais une nuit, il est pris par les agents des douanes. Le FBI lui offre une dernière chance : s\'il accepte de prendre part à une opération conjointe des douanes et du FBI, son passé criminel sera effacé.',2001,120,7,200,230000,2,'https://i.imgur.com/ePgCJAq.png'),(10,'Prometheus','Following clues to the origin of mankind/ a team finds a structure on a distant moon/ but they soon realize they are not alone.',2012,124,7,485820,126.46,1,NULL),(14,'Split','Three girls are kidnapped by a man with a diagnosed 23 distinct personalities. They must try to escape before the apparent mergence of a frightful new 24th.',2016,117,7.3,157606,138.12,1,NULL),(15,'Sing','In a city of humanoid animals/ a hustling theater impresario\'s attempt to save his theater with a singing competition becomes grander than he anticipates even as its finalists\' find that their lives will never be the same.',2016,108,7.2,60545,270.32,1,NULL),(16,'SuperFredo','Fred c\'est le pire dans le fond',2020,182,10,666074,700.13,2,NULL),(19,'Alice au pays des merveilles','lapin avec un chapeau',1982,7,10,96,2,6,NULL),(45,'Lion','\"A five-year-old Indian boy gets lost on the streets of Calcutta, thousands of kilometers from home. He survives many challenges before being adopted by a couple in Australia. 25 years later, he sets out to find his lost family.\"',2016,118,8.1,102061,51.69,12,NULL),(46,'Arrival','\"When twelve mysterious spacecraft appear around the world, linguistics professor Louise Banks is tasked with interpreting the language of the apparent alien visitors.\"',2016,116,8,340798,100.5,3,NULL),(47,'Gold','\"Kenny Wells, a prospector desperate for a lucky break, teams up with a similarly eager geologist and sets off on a journey to find gold in the uncharted jungle of Indonesia.\"',2016,120,6.7,19053,7.22,11,NULL),(48,'Manchester by the Sea','A depressed uncle is asked to take care of his teenage nephew after the boy\'s father dies.',2016,137,7.9,134213,47.7,3,NULL),(49,'Hounds of Love','\"A cold-blooded predatory couple while cruising the streets in search of their next victim, will stumble upon a 17-year-old high school girl, who will be sedated, abducted and chained in the strangers\' guest room.\"',2016,108,6.7,1115,72,6,NULL),(50,'Tinker Tailor Soldier Spy','\"In the bleak days of the Cold War, espionage veteran George Smiley is forced from semi-retirement to uncover a Soviet agent within MI6.\"',2011,122,7.1,157053,24.1,3,NULL),(51,'Resident Evil: Retribution','Alice fights alongside a resistance movement to regain her freedom from an Umbrella Corporation testing facility.',2012,96,5.4,114144,42.35,1,NULL),(52,'Dear Zindagi','\"Kaira is a budding cinematographer in search of a perfect life. Her encounter with Jug, an unconventional thinker, helps her gain a new perspective on life. She discovers that happiness is all about finding comfort in life\'s imperfections.\"',2016,151,7.8,23540,1.4,3,NULL),(53,'Genius','\"A chronicle of Max Perkins\'s time as the book editor at Scribner, where he oversaw works by Thomas Wolfe, Ernest Hemingway, F. Scott Fitzgerald and others.\"',2016,104,6.5,10708,1.36,12,NULL),(55,'Gravity','Donc le film se passe dans l\'espace et je sais pas trop pourquoi mais un moment donné y\'a une catastrophe faque les gens meurent pi là on suit une actrice qui essaie de survivre dans l\'espace et qui tente de revenir sur la Terre.',2013,91,7,30000000,723000000,7,'https://i.imgur.com/CTdUlaf.png'),(136,'Guardians of the Galaxy','A group of intergalactic criminals are forced to work together to stop a fanatical warrior from taking control of the universe.',2014,121,8.1,757074,333.13,1,NULL),(137,'Rules Don\'t Apply','\"The unconventional love story of an aspiring actress, her determined driver, and their boss, eccentric billionaire Howard Hughes.\"',2016,127,5.7,3731,3.65,2,NULL),(138,'Ouija: Origin of Evil','\"In 1967 Los Angeles, a widowed mother and her 2 daughters add a new stunt to bolster their seance scam business, inviting an evil presence into their home.\"',2016,99,6.1,30035,34.9,8,NULL),(139,'Percy Jackson: Sea of Monsters','\"In order to restore their dying safe haven, the son of Poseidon and his friends embark on a quest to the Sea of Monsters to find the mythical Golden Fleece while trying to stop an ancient evil from rising.\"',2013,106,5.9,91684,68.56,11,NULL),(140,'Fracture','\"An attorney, intent on climbing the career ladder toward success, finds an unlikely opponent in a manipulative criminal he is trying to prosecute.\"',2007,113,7.2,148943,39,6,NULL),(141,'Oculus','\"A woman tries to exonerate her brother, who was convicted of murder, by proving that the crime was committed by a supernatural phenomenon.\"',2013,104,6.5,92875,27.69,8,NULL),(142,'In Bruges','\"Guilt-stricken after a job gone wrong, hitman Ray and his partner await orders from their ruthless boss in Bruges, Belgium, the last place in the world Ray wants to be.\"',2008,107,7.9,322536,7.76,2,NULL),(143,'This Means War','Two top CIA operatives wage an epic battle against one another after they discover they are dating the same woman.',2012,103,6.3,154400,54.76,1,NULL),(144,'Lída Baarová','A film about the black-and-white era actress Lída Baarová and her doomed love affair.',2016,106,5,353,0,12,NULL),(145,'Test JBR','test\r\n',1990,122,10,19999,1e46,5,'C:\\Users\\julbr\\Dropbox\\cegep\\420-EAG-LI-PVM\\clap.jpg'),(146,'test JBR','csAscas',2000,1,10,4,NULL,1,NULL);
/*!40000 ALTER TABLE `film` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genre`
--

DROP TABLE IF EXISTS `genre`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `genre` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genre`
--

LOCK TABLES `genre` WRITE;
/*!40000 ALTER TABLE `genre` DISABLE KEYS */;
INSERT INTO `genre` VALUES (1,'action'),(2,'comédie'),(3,'drame'),(4,'romance amoureuse'),(5,'historique'),(6,'policier'),(7,'science-fiction'),(8,'horreur'),(10,'fantasy'),(11,'aventure'),(12,'biographie'),(14,'thriller'),(15,'mystère'),(16,'animation'),(17,'autre');
/*!40000 ALTER TABLE `genre` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `profil`
--

DROP TABLE IF EXISTS `profil`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `profil` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `courriel` varchar(75) NOT NULL,
  `type` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `profil`
--

LOCK TABLES `profil` WRITE;
/*!40000 ALTER TABLE `profil` DISABLE KEYS */;
INSERT INTO `profil` VALUES (1,'alex','alex123','alex@hotmaail.com','admin'),(2,'tristan','tristan123','tristan@hotmail.com','admin'),(3,'seb','seb123','sebastien@hotmail.com','admin'),(4,'fred','fred123','frédéric@hotmail.com','admin'),(5,'eric','eric123','ericmartel1@hotmail.com','utilisateur'),(6,'julien','jul123','julien.brunet@toto.com','admin');
/*!40000 ALTER TABLE `profil` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `responsable`
--

DROP TABLE IF EXISTS `responsable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `responsable` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(45) NOT NULL,
  `prenom` varchar(45) NOT NULL,
  `telephone` varchar(30) NOT NULL,
  `courriel` varchar(75) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `responsable`
--

LOCK TABLES `responsable` WRITE;
/*!40000 ALTER TABLE `responsable` DISABLE KEYS */;
INSERT INTO `responsable` VALUES (1,'Chaplin','Charlie','(418) 544-5444','CharlieChaplin@hotmail.com'),(2,'Lepage','Boulot','(418) 544-3333','responsable2@hotmail.com'),(4,'Couture','Martin','456-666-0000','martinC@hot.com'),(28,'Lepage','Fred','418-954-0390','johndoe@hot.com'),(30,'Doe','John','000-000-0000','johndoe@hot.com');
/*!40000 ALTER TABLE `responsable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_film` int(11) NOT NULL,
  `id_acteur` int(11) NOT NULL,
  `nom_personnage` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idFilmRole` (`id_film`),
  KEY `idActeurRole` (`id_acteur`),
  CONSTRAINT `idActeurRole` FOREIGN KEY (`id_acteur`) REFERENCES `acteur` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `idFilmRole` FOREIGN KEY (`id_film`) REFERENCES `film` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,1,1,'Dominic Toretto'),(13,16,3,'fredo'),(16,19,3,'le lapin a chapo'),(21,1,4,'Julien Brunet'),(36,1,6,'Bobby'),(39,55,1,'toto');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `salle`
--

DROP TABLE IF EXISTS `salle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `salle` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `numero` int(11) NOT NULL,
  `nbr_places` int(11) NOT NULL,
  `taille_ecran` varchar(45) NOT NULL,
  `exploitable` tinyint(1) NOT NULL,
  `id_cinema` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idCinemaSalle` (`id_cinema`),
  CONSTRAINT `idCinemaSalle` FOREIGN KEY (`id_cinema`) REFERENCES `cinema` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `salle`
--

LOCK TABLES `salle` WRITE;
/*!40000 ALTER TABLE `salle` DISABLE KEYS */;
INSERT INTO `salle` VALUES (58,1,300,'60',0,5),(66,1,150,'56',1,11),(73,2,45,'50',1,11),(74,3,80,'80',0,11),(75,1,125,'100',1,1),(76,2,350,'100',0,1),(77,3,55,'20',1,1),(83,2,250,'67',0,35);
/*!40000 ALTER TABLE `salle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seance`
--

DROP TABLE IF EXISTS `seance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `seance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `heure_debut` datetime NOT NULL,
  `heure_fin` datetime NOT NULL,
  `nom_seance` varchar(45) NOT NULL,
  `id_salle` int(11) NOT NULL,
  `id_film` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idSalleSeance` (`id_salle`),
  KEY `idFilmSeance` (`id_film`),
  CONSTRAINT `idFilmSeance` FOREIGN KEY (`id_film`) REFERENCES `film` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `idSalleSeance` FOREIGN KEY (`id_salle`) REFERENCES `salle` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=306 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seance`
--

LOCK TABLES `seance` WRITE;
/*!40000 ALTER TABLE `seance` DISABLE KEYS */;
INSERT INTO `seance` VALUES (224,'2020-05-18 20:00:00','2020-05-18 22:27:00','fredo',66,14),(225,'0001-01-01 00:00:00','0001-01-01 03:01:00','fredo3',73,52),(276,'0001-01-01 00:00:00','0001-01-01 02:30:00','lll',77,1),(302,'0001-01-01 00:00:00','0001-01-01 02:30:00','afsfsfafsafa',58,1),(303,'0001-01-02 00:00:00','0001-01-02 02:30:00','afsfsfafsafa',58,1),(304,'0001-01-04 00:00:00','0001-01-04 02:30:00','afsfsfafsafa',58,1),(305,'0001-01-06 00:00:00','0001-01-06 02:30:00','afsfsfafsafa',58,1);
/*!40000 ALTER TABLE `seance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `supplements`
--

DROP TABLE IF EXISTS `supplements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `supplements` (
  `id` int(11) NOT NULL,
  `3d` tinyint(1) NOT NULL,
  `ultra_avx` tinyint(1) NOT NULL,
  `dbox` tinyint(1) NOT NULL,
  `imax` tinyint(1) NOT NULL,
  `id_salle` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idSalleSupplements` (`id_salle`),
  CONSTRAINT `idSalleSupplements` FOREIGN KEY (`id_salle`) REFERENCES `salle` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `supplements`
--

LOCK TABLES `supplements` WRITE;
/*!40000 ALTER TABLE `supplements` DISABLE KEYS */;
INSERT INTO `supplements` VALUES (1,0,0,1,0,58),(2,1,0,0,0,66),(3,0,0,0,0,73),(4,0,0,0,0,74),(5,0,0,0,0,75),(6,0,0,0,0,76),(7,0,0,0,0,77),(9,0,0,0,0,83);
/*!40000 ALTER TABLE `supplements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tarifs`
--

DROP TABLE IF EXISTS `tarifs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tarifs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `prix_adulte` double NOT NULL,
  `prix_enfant` double NOT NULL,
  `prix_aine` double NOT NULL,
  `prix_3d` double NOT NULL,
  `prix_ultra_avx` double NOT NULL,
  `prix_dbox` double NOT NULL,
  `prix_imax` double NOT NULL,
  `id_cinema` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `idCinema` (`id_cinema`),
  CONSTRAINT `idCinema` FOREIGN KEY (`id_cinema`) REFERENCES `cinema` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarifs`
--

LOCK TABLES `tarifs` WRITE;
/*!40000 ALTER TABLE `tarifs` DISABLE KEYS */;
INSERT INTO `tarifs` VALUES (1,10,7,8,3,5,7,5,1);
/*!40000 ALTER TABLE `tarifs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-27 16:42:08
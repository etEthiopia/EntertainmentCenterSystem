CREATE DATABASE  IF NOT EXISTS `fdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `fdb`;
-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: localhost    Database: fdb
-- ------------------------------------------------------
-- Server version	8.0.13

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `dirs`
--

DROP TABLE IF EXISTS `dirs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `dirs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dir` mediumtext NOT NULL,
  `type` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dirs`
--

LOCK TABLES `dirs` WRITE;
/*!40000 ALTER TABLE `dirs` DISABLE KEYS */;
INSERT INTO `dirs` VALUES (1,'C:/Users/Dagi/Downloads',1),(2,'C:/Users/Dagi/Videos',1);
/*!40000 ALTER TABLE `dirs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drives`
--

DROP TABLE IF EXISTS `drives`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `drives` (
  `id` int(11) NOT NULL,
  `name` varchar(225) DEFAULT NULL,
  `port` varchar(45) DEFAULT NULL,
  `serial` varchar(225) DEFAULT NULL,
  `size` varchar(225) DEFAULT NULL,
  `status` varchar(45) NOT NULL,
  `entrytime` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drives`
--

LOCK TABLES `drives` WRITE;
/*!40000 ALTER TABLE `drives` DISABLE KEYS */;
/*!40000 ALTER TABLE `drives` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `itemtypes`
--

DROP TABLE IF EXISTS `itemtypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `itemtypes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(225) NOT NULL,
  `price` double NOT NULL,
  `filetype` varchar(225) NOT NULL,
  `reference` mediumtext,
  `initials` varchar(2) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `itemtypes`
--

LOCK TABLES `itemtypes` WRITE;
/*!40000 ALTER TABLE `itemtypes` DISABLE KEYS */;
INSERT INTO `itemtypes` VALUES (1,'Series',1,'Video','DagiEnt','S'),(2,'Movie',3,'Video','dagiEnt','M');
/*!40000 ALTER TABLE `itemtypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `videosells`
--

DROP TABLE IF EXISTS `videosells`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `videosells` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` mediumtext NOT NULL,
  `dir` mediumtext NOT NULL,
  `type` int(11) NOT NULL,
  `price` double NOT NULL,
  `size` varchar(10) NOT NULL,
  `datetime` datetime NOT NULL,
  `reciever` varchar(25) NOT NULL,
  `recieverserial` varchar(225) NOT NULL,
  `folderserial` int(11) DEFAULT NULL,
  `special` int(11) DEFAULT NULL,
  `videofolderserial` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `videosells`
--

LOCK TABLES `videosells` WRITE;
/*!40000 ALTER TABLE `videosells` DISABLE KEYS */;
INSERT INTO `videosells` VALUES (147,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:00:01','E:/','',NULL,NULL,0),(148,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:02','E:/','',NULL,NULL,0),(149,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:03','E:/','',NULL,NULL,0),(150,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:00:24','E:/','',NULL,NULL,0),(151,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:00:24','E:/','',NULL,NULL,0),(152,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:00:24','E:/','',NULL,NULL,0),(153,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:25','E:/','',NULL,NULL,0),(154,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:25','E:/','',NULL,NULL,0),(155,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:25','E:/','',NULL,NULL,0),(156,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:26','E:/','',NULL,NULL,0),(157,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:26','E:/','',NULL,NULL,0),(158,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:26','E:/','',NULL,NULL,0),(159,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:00:26','E:/','',NULL,NULL,0),(160,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:03:07','E:/','',NULL,NULL,0),(161,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:08','E:/','',NULL,NULL,0),(162,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:09','E:/','',NULL,NULL,0),(163,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:03:43','E:/','',NULL,NULL,0),(164,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:03:43','E:/','',NULL,NULL,0),(165,'1.mp4','C:/Users/Dagi/Videos/',1,1,'57 MB','2019-09-30 14:03:43','E:/','',NULL,NULL,0),(166,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:44','E:/','',NULL,NULL,0),(167,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:44','E:/','',NULL,NULL,0),(168,'4.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:45','E:/','',NULL,NULL,0),(169,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:45','E:/','',NULL,NULL,0),(170,'2.mp4','C:/Users/Dagi/Videos/',1,1,'10 MB','2019-09-30 14:03:46','E:/','',NULL,NULL,0);
/*!40000 ALTER TABLE `videosells` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-10-04  7:44:43

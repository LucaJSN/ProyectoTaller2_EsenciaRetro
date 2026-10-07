-- MySqlBackup.NET 2.7.1.0
-- Dump Time: 2026-09-23 11:37:31
-- --------------------------------------
-- Server version 12.2.2-MariaDB MariaDB Server


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
-- Definition of direccion
-- 

DROP TABLE IF EXISTS `direccion`;
CREATE TABLE IF NOT EXISTS `direccion` (
  `id_direccion` int(11) NOT NULL AUTO_INCREMENT,
  `provincia` varchar(255) NOT NULL,
  `ciudad` varchar(255) NOT NULL,
  `calle` varchar(255) NOT NULL,
  `altura` int(11) NOT NULL,
  PRIMARY KEY (`id_direccion`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- 
-- Dumping data for table direccion
-- 

/*!40000 ALTER TABLE `direccion` DISABLE KEYS */;
INSERT INTO `direccion`(`id_direccion`,`provincia`,`ciudad`,`calle`,`altura`) VALUES(1,'Corrientes','Posadas','Zabala',2731),(3,'Misiones','Posadas','Zabala',2730),(25,'a','asdas','',0),(26,'Cordoba','Villa Maria','Riachuelo',0);
/*!40000 ALTER TABLE `direccion` ENABLE KEYS */;

-- 
-- Definition of rol
-- 

DROP TABLE IF EXISTS `rol`;
CREATE TABLE IF NOT EXISTS `rol` (
  `id_rol` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` varchar(255) NOT NULL,
  PRIMARY KEY (`id_rol`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- 
-- Dumping data for table rol
-- 

/*!40000 ALTER TABLE `rol` DISABLE KEYS */;
INSERT INTO `rol`(`id_rol`,`tipo`) VALUES(1,'admin'),(2,'supervisor'),(3,'vendedor');
/*!40000 ALTER TABLE `rol` ENABLE KEYS */;

-- 
-- Definition of usuario
-- 

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE IF NOT EXISTS `usuario` (
  `id_usuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(255) NOT NULL,
  `apellido` varchar(255) NOT NULL,
  `correo` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `telefono` varchar(255) DEFAULT NULL,
  `Direccion_id` int(11) DEFAULT NULL,
  `Rol_id` int(11) NOT NULL,
  `fecha_baja` datetime DEFAULT NULL,
  `fecha_alta` datetime DEFAULT current_timestamp(),
  `fecha_modificacion` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `correo` (`correo`),
  KEY `fk_Usuario_Rol` (`Rol_id`),
  CONSTRAINT `fk_Usuario_Rol` FOREIGN KEY (`Rol_id`) REFERENCES `rol` (`id_rol`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;

-- 
-- Dumping data for table usuario
-- 

/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario`(`id_usuario`,`nombre`,`apellido`,`correo`,`password`,`telefono`,`Direccion_id`,`Rol_id`,`fecha_baja`,`fecha_alta`,`fecha_modificacion`) VALUES(1,'Luca','Nielsen','luca@gmail.com','luca123','3743510296',1,1,NULL,'2026-09-13 19:25:42','2026-09-15 01:52:41'),(11,'Pepesasdasd','Mariña','pepe@gmail.com','luca','46830564',3,3,'2026-09-17 16:14:07','2026-09-13 19:25:42','2026-09-17 16:14:07'),(20,'Joaquin','Saucedo','otro@gmail.com','123456','123',25,3,NULL,'2026-09-15 01:57:33','2026-09-17 16:14:00'),(21,'Pedro','Palacios','pedrito@gmail.com','123','123412',26,3,NULL,'2026-09-15 02:03:02','2026-09-17 15:26:29');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;


/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2026-09-23 11:37:31
-- Total time: 0:0:0:0:100 (d:h:m:s:ms)

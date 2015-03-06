CREATE DATABASE  IF NOT EXISTS `theme_park_db` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `theme_park_db`;
-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: siliconshores.net    Database: theme_park_db
-- ------------------------------------------------------
-- Server version	5.6.22-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table ` room_types`
--

DROP TABLE IF EXISTS ` room_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE ` room_types` (
  `room_type_id` int(11) NOT NULL AUTO_INCREMENT,
  `room_types_string` varchar(255) NOT NULL,
  PRIMARY KEY (`room_type_id`),
  UNIQUE KEY `room_types_string_UNIQUE` (`room_types_string`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `attractions`
--

DROP TABLE IF EXISTS `attractions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `attractions` (
  `attractions_id` int(11) NOT NULL AUTO_INCREMENT,
  `attraction_name` varchar(50) NOT NULL,
  `theme_area_id` int(11) NOT NULL,
  `attraction_description` varchar(5000) DEFAULT NULL,
  `picture_path` varchar(50) DEFAULT NULL,
  `is_working` tinyint(1) NOT NULL,
  `date_opened` date NOT NULL,
  PRIMARY KEY (`attractions_id`),
  KEY `theme_area_id_idx` (`theme_area_id`),
  CONSTRAINT `theme_area_id` FOREIGN KEY (`theme_area_id`) REFERENCES `theme_areas` (`theme_area_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `breakdowns`
--

DROP TABLE IF EXISTS `breakdowns`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `breakdowns` (
  `breakdown_id` int(11) NOT NULL AUTO_INCREMENT,
  `attraction_id` int(11) NOT NULL,
  `incidence_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `resolution_date` timestamp NULL DEFAULT NULL,
  `repair_cost` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`breakdown_id`),
  KEY `attraction_id_breakdowns` (`attraction_id`),
  CONSTRAINT `attraction_id_breakdowns` FOREIGN KEY (`attraction_id`) REFERENCES `attractions` (`attractions_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `daily_ride_report`
--

DROP TABLE IF EXISTS `daily_ride_report`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `daily_ride_report` (
  `ride_report_date` date NOT NULL,
  `attraction_id` int(11) NOT NULL,
  `total_riders` int(11) NOT NULL,
  PRIMARY KEY (`ride_report_date`,`attraction_id`),
  KEY `attraction_id_ride_reports` (`attraction_id`),
  CONSTRAINT `attraction_id_ride_reports` FOREIGN KEY (`attraction_id`) REFERENCES `attractions` (`attractions_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `daily_weather`
--

DROP TABLE IF EXISTS `daily_weather`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `daily_weather` (
  `weather_date` date NOT NULL,
  `theme_park_id` int(11) NOT NULL,
  `weather_conditions` enum('sunny','partly cloudy','mostly cloudy','overcast','rainy') NOT NULL,
  `high_temp` int(11) NOT NULL,
  `low_temp` int(11) NOT NULL,
  PRIMARY KEY (`weather_date`),
  KEY `theme_park_weather` (`theme_park_id`),
  CONSTRAINT `theme_park_weather` FOREIGN KEY (`theme_park_id`) REFERENCES `theme_park` (`theme_park_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employees` (
  `ssn` int(11) NOT NULL,
  `theme_park_id` int(11) NOT NULL,
  `first_name` varchar(45) NOT NULL,
  `last_name` varchar(45) NOT NULL,
  `middle_initial` varchar(1) DEFAULT NULL,
  `full_time` tinyint(1) NOT NULL,
  `payrate` decimal(10,0) NOT NULL,
  `hired_date` date NOT NULL,
  `date_left` date DEFAULT NULL,
  `rehireable` tinyint(1) DEFAULT NULL,
  `employee_id` varchar(128) NOT NULL,
  PRIMARY KEY (`employee_id`),
  KEY `theme_park_id_employees_idx` (`theme_park_id`),
  CONSTRAINT `theme_park_id_employees` FOREIGN KEY (`theme_park_id`) REFERENCES `theme_park` (`theme_park_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `food_categories`
--

DROP TABLE IF EXISTS `food_categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `food_categories` (
  `food_category_id` int(11) NOT NULL,
  `food_categories_name` varchar(45) NOT NULL,
  PRIMARY KEY (`food_category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `hotel_reservations`
--

DROP TABLE IF EXISTS `hotel_reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hotel_reservations` (
  `reservation_id` int(11) NOT NULL AUTO_INCREMENT,
  `hotel_id` int(11) NOT NULL,
  `room_number` int(11) NOT NULL,
  `reservation_checkin_date` date NOT NULL,
  `reservation_checkout_date` date NOT NULL,
  `total_reservation_cost` decimal(10,0) NOT NULL,
  `paid_in_full` tinyint(1) NOT NULL,
  PRIMARY KEY (`reservation_id`),
  KEY `hotel_id_reservations` (`hotel_id`,`room_number`),
  CONSTRAINT `room_id_reservation` FOREIGN KEY (`hotel_id`, `room_number`) REFERENCES `hotel_rooms` (`hotel_id`, `room_number`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `hotel_rooms`
--

DROP TABLE IF EXISTS `hotel_rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hotel_rooms` (
  `hotel_id` int(11) NOT NULL,
  `room_number` int(11) NOT NULL,
  `room_type_id` int(11) NOT NULL,
  `room_rate` decimal(10,0) NOT NULL,
  `occupied` tinyint(1) NOT NULL,
  PRIMARY KEY (`hotel_id`,`room_number`),
  KEY `room_id_rooms` (`hotel_id`),
  KEY `room_type_id_rooms` (`room_type_id`),
  CONSTRAINT `hotel_id_rooms` FOREIGN KEY (`hotel_id`) REFERENCES `hotels` (`hotel_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `room_type_id_rooms` FOREIGN KEY (`room_type_id`) REFERENCES `room_types` (`room_type_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `hotels`
--

DROP TABLE IF EXISTS `hotels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `hotels` (
  `hotel_id` int(11) NOT NULL AUTO_INCREMENT,
  `hotel_name` varchar(45) NOT NULL,
  `pets_allowed` tinyint(1) NOT NULL,
  `theme_area_id` int(11) NOT NULL,
  PRIMARY KEY (`hotel_id`),
  KEY `theme_area_id_hotels` (`theme_area_id`),
  CONSTRAINT `theme_area_id_hotels` FOREIGN KEY (`theme_area_id`) REFERENCES `theme_areas` (`theme_area_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `restaurant_daily_reports`
--

DROP TABLE IF EXISTS `restaurant_daily_reports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `restaurant_daily_reports` (
  `report_date` date NOT NULL,
  `restaurant_id` int(11) NOT NULL,
  `gross_income` decimal(10,0) NOT NULL,
  `patrons_served` int(11) NOT NULL,
  PRIMARY KEY (`report_date`,`restaurant_id`),
  KEY `restaurant_id_reports` (`restaurant_id`),
  CONSTRAINT `restaurant_id_reports` FOREIGN KEY (`restaurant_id`) REFERENCES `restaurants` (`restaurant_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `restaurants`
--

DROP TABLE IF EXISTS `restaurants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `restaurants` (
  `restaurant_id` int(11) NOT NULL,
  `restaurant_name` varchar(45) NOT NULL,
  `food_category_id` int(11) NOT NULL,
  `theme_area_id` int(11) NOT NULL,
  PRIMARY KEY (`restaurant_id`),
  KEY `theme_area_id_restaurants` (`theme_area_id`),
  KEY `restaurant_type_id_idx` (`food_category_id`),
  CONSTRAINT `food_category_id` FOREIGN KEY (`food_category_id`) REFERENCES `food_categories` (`food_category_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `theme_area_id_restaurants` FOREIGN KEY (`theme_area_id`) REFERENCES `theme_areas` (`theme_area_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `roles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `theme_areas`
--

DROP TABLE IF EXISTS `theme_areas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `theme_areas` (
  `theme_area_id` int(11) NOT NULL AUTO_INCREMENT,
  `theme_area_name` varchar(45) NOT NULL,
  `theme_area_description` varchar(5000) DEFAULT NULL,
  `theme_area_pictures` varchar(50) DEFAULT NULL,
  `theme_park_id` int(11) NOT NULL,
  PRIMARY KEY (`theme_area_id`),
  KEY `theme_park_areas` (`theme_park_id`),
  CONSTRAINT `theme_park_areas` FOREIGN KEY (`theme_park_id`) REFERENCES `theme_park` (`theme_park_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `theme_park`
--

DROP TABLE IF EXISTS `theme_park`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `theme_park` (
  `theme_park_id` int(11) NOT NULL,
  `theme_park_name` varchar(45) NOT NULL,
  `park_open` time NOT NULL,
  `park_close` time NOT NULL,
  `park_country` varchar(100) NOT NULL,
  `park_state_or_province` varchar(100) NOT NULL,
  `park_city` varchar(100) NOT NULL,
  `park_street_address` varchar(100) NOT NULL,
  PRIMARY KEY (`theme_park_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ticket_sales`
--

DROP TABLE IF EXISTS `ticket_sales`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ticket_sales` (
  `ticket_id` int(11) NOT NULL AUTO_INCREMENT,
  `ticket_type_id` int(11) NOT NULL,
  `sale_date` date NOT NULL,
  `redemption_date` date DEFAULT NULL,
  `theme_park_id` int(11) NOT NULL,
  `sale_location` enum('Box Office','Online') DEFAULT NULL,
  PRIMARY KEY (`ticket_id`),
  KEY `ticket_type_id_sales` (`ticket_type_id`),
  KEY `theme_park_id_sales` (`theme_park_id`),
  CONSTRAINT `theme_park_sales` FOREIGN KEY (`theme_park_id`) REFERENCES `theme_park` (`theme_park_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `ticket_type_sales` FOREIGN KEY (`ticket_type_id`) REFERENCES `ticket_types` (`ticket_type_id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `ticket_types`
--

DROP TABLE IF EXISTS `ticket_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ticket_types` (
  `ticket_type_id` int(11) NOT NULL AUTO_INCREMENT,
  `ticket_name` varchar(45) NOT NULL,
  `ticket_restrictions` varchar(200) NOT NULL,
  `ticket_price` float DEFAULT NULL,
  PRIMARY KEY (`ticket_type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `userclaims`
--

DROP TABLE IF EXISTS `userclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `userlogins`
--

DROP TABLE IF EXISTS `userlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`),
  CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `userroles`
--

DROP TABLE IF EXISTS `userroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `employee_id` FOREIGN KEY (`Id`) REFERENCES `employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'theme_park_db'
--

--
-- Dumping routines for database 'theme_park_db'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-03-05 16:56:37

-- MySQL Script generated by MySQL Workbench
-- 02/15/15 02:28:00
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema Theme_Park_DB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema Theme_Park_DB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Theme_Park_DB` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `Theme_Park_DB` ;

-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`theme_park`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`theme_park` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`theme_park` (
  `theme_park_id` INT NOT NULL,
  `theme_park_name` VARCHAR(45) NOT NULL,
  `park_open` TIME NOT NULL,
  `park_close` TIME NOT NULL,
  `park_country` VARCHAR(100) NOT NULL,
  `park_state_or_province` VARCHAR(100) NOT NULL,
  `park_city` VARCHAR(100) NOT NULL,
  `park_street_address` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`theme_park_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`theme_areas`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`theme_areas` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`theme_areas` (
  `theme_area_id` INT NOT NULL AUTO_INCREMENT,
  `theme_area_name` VARCHAR(45) NOT NULL,
  `theme_area_description` VARCHAR(200) NULL,
  `theme_area_pictures` VARCHAR(50) NULL,
  `theme_park_id` INT NOT NULL,
  PRIMARY KEY (`theme_area_id`),
  INDEX `theme_park_areas` (`theme_park_id` ASC),
  CONSTRAINT `theme_park_areas`
    FOREIGN KEY (`theme_park_id`)
    REFERENCES `Theme_Park_DB`.`theme_park` (`theme_park_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`attractions`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`attractions` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`attractions` (
  `attractions_id` INT NOT NULL AUTO_INCREMENT,
  `attraction_name` VARCHAR(50) NOT NULL,
  `theme_area_id` INT NOT NULL,
  `attraction_description` VARCHAR(200) NULL,
  `picture_path` VARCHAR(50) NULL,
  `is_working` TINYINT(1) NOT NULL,
  `date_opened` DATE NOT NULL,
  PRIMARY KEY (`attractions_id`),
  INDEX `theme_area_id_idx` (`theme_area_id` ASC),
  CONSTRAINT `theme_area_id`
    FOREIGN KEY (`theme_area_id`)
    REFERENCES `Theme_Park_DB`.`theme_areas` (`theme_area_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`breakdowns`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`breakdowns` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`breakdowns` (
  `breakdown_id` INT NOT NULL,
  `attraction_id` INT NOT NULL,
  `incidence_date` TIMESTAMP NOT NULL,
  `resolution_date` TIMESTAMP NULL,
  `repair_cost` DECIMAL(10,0) NULL,
  PRIMARY KEY (`breakdown_id`),
  INDEX `attraction_id_breakdowns` (`attraction_id` ASC),
  CONSTRAINT `attraction_id_breakdowns`
    FOREIGN KEY (`attraction_id`)
    REFERENCES `Theme_Park_DB`.`attractions` (`attractions_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`daily_weather`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`daily_weather` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`daily_weather` (
  `weather_date` DATE NOT NULL,
  `theme_park_id` INT NOT NULL,
  `weather_conditions` ENUM('sunny','partly cloudy','mostly cloudy','overcast','rainy') NOT NULL,
  `high_temp` INT NOT NULL,
  `low_temp` INT NOT NULL,
  PRIMARY KEY (`weather_date`),
  INDEX `theme_park_weather` (`theme_park_id` ASC),
  CONSTRAINT `theme_park_weather`
    FOREIGN KEY (`theme_park_id`)
    REFERENCES `Theme_Park_DB`.`theme_park` (`theme_park_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`ticket_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`ticket_types` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`ticket_types` (
  `ticket_type_id` INT NOT NULL AUTO_INCREMENT,
  `ticket_name` VARCHAR(45) NOT NULL,
  `ticket_restrictions` VARCHAR(200) NOT NULL,
  `ticket_price` FLOAT NULL,
  PRIMARY KEY (`ticket_type_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`ticket_sales`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`ticket_sales` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`ticket_sales` (
  `ticket_id` INT NOT NULL AUTO_INCREMENT,
  `ticket_type_id` INT NOT NULL,
  `sale_date` DATE NOT NULL,
  `redemption_date` DATE NULL,
  `theme_park_id` INT NOT NULL,
  `sale_location` ENUM('Box Office','Online') NULL,
  PRIMARY KEY (`ticket_id`),
  INDEX `ticket_type_id_sales` (`ticket_type_id` ASC),
  INDEX `theme_park_id_sales` (`theme_park_id` ASC),
  CONSTRAINT `ticket_type_sales`
    FOREIGN KEY (`ticket_type_id`)
    REFERENCES `Theme_Park_DB`.`ticket_types` (`ticket_type_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `theme_park_sales`
    FOREIGN KEY (`theme_park_id`)
    REFERENCES `Theme_Park_DB`.`theme_park` (`theme_park_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`hotels`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`hotels` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`hotels` (
  `hotel_id` INT NOT NULL AUTO_INCREMENT,
  `hotel_name` VARCHAR(45) NOT NULL,
  `pets_allowed` TINYINT(1) NOT NULL,
  `theme_area_id` INT NOT NULL,
  PRIMARY KEY (`hotel_id`),
  INDEX `theme_area_id_hotels` (`theme_area_id` ASC),
  CONSTRAINT `theme_area_id_hotels`
    FOREIGN KEY (`theme_area_id`)
    REFERENCES `Theme_Park_DB`.`theme_areas` (`theme_area_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`room_types`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`room_types` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`room_types` (
  `room_type_id` INT NOT NULL AUTO_INCREMENT,
  `room_types_string` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`room_type_id`),
  UNIQUE INDEX `room_types_string_UNIQUE` (`room_types_string` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`hotel_rooms`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`hotel_rooms` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`hotel_rooms` (
  `hotel_id` INT NOT NULL,
  `room_number` INT NOT NULL,
  `room_type_id` INT NOT NULL,
  `room_rate` DECIMAL(10,0) NOT NULL,
  `occupied` TINYINT(1) NOT NULL,
  INDEX `room_id_rooms` (`hotel_id` ASC),
  PRIMARY KEY (`hotel_id`, `room_number`),
  INDEX `room_type_id_rooms` (`room_type_id` ASC),
  CONSTRAINT `hotel_id_rooms`
    FOREIGN KEY (`hotel_id`)
    REFERENCES `Theme_Park_DB`.`hotels` (`hotel_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `room_type_id_rooms`
    FOREIGN KEY (`room_type_id`)
    REFERENCES `Theme_Park_DB`.`room_types` (`room_type_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`hotel_reservations`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`hotel_reservations` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`hotel_reservations` (
  `reservation_id` INT NOT NULL AUTO_INCREMENT,
  `hotel_id` INT NOT NULL,
  `room_number` INT NOT NULL,
  `reservation_checkin_date` DATE NOT NULL,
  `reservation_checkout_date` DATE NOT NULL,
  `total_reservation_cost` DECIMAL(10,0) NOT NULL,
  `paid_in_full` TINYINT(1) NOT NULL,
  PRIMARY KEY (`reservation_id`),
  INDEX `hotel_id_reservations` (`hotel_id` ASC, `room_number` ASC),
  CONSTRAINT `room_id_reservation`
    FOREIGN KEY (`hotel_id` , `room_number`)
    REFERENCES `Theme_Park_DB`.`hotel_rooms` (`hotel_id` , `room_number`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`restaurants`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`restaurants` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`restaurants` (
  `restaurant_id` INT NOT NULL,
  `restaurant_name` VARCHAR(45) NOT NULL,
  `food_category_id` INT NOT NULL,
  `theme_area_id` INT NOT NULL,
  PRIMARY KEY (`restaurant_id`),
  INDEX `theme_area_id_restaurants` (`theme_area_id` ASC),
  CONSTRAINT `theme_area_id_restaurants`
    FOREIGN KEY (`theme_area_id`)
    REFERENCES `Theme_Park_DB`.`theme_areas` (`theme_area_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`food_categories`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`food_categories` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`food_categories` (
  `food_category_id` INT NOT NULL,
  `food_categories_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`food_category_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`restaurant_daily_reports`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`restaurant_daily_reports` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`restaurant_daily_reports` (
  `report_date` DATE NOT NULL,
  `restaurant_id` INT NOT NULL,
  `gross_income` DECIMAL(10,0) NOT NULL,
  `patrons_served` INT NOT NULL,
  PRIMARY KEY (`report_date`),
  INDEX `restaurant_id_reports` (`restaurant_id` ASC),
  CONSTRAINT `restaurant_id_reports`
    FOREIGN KEY (`restaurant_id`)
    REFERENCES `Theme_Park_DB`.`restaurants` (`restaurant_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`daily_ride_report`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`daily_ride_report` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`daily_ride_report` (
  `ride_report_date` DATE NOT NULL,
  `attraction_id` INT NOT NULL,
  `total_riders` INT NOT NULL,
  PRIMARY KEY (`ride_report_date`),
  INDEX `attraction_id_ride_reports` (`attraction_id` ASC),
  CONSTRAINT `attraction_id_ride_reports`
    FOREIGN KEY (`attraction_id`)
    REFERENCES `Theme_Park_DB`.`attractions` (`attractions_id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`job_titles`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`job_titles` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`job_titles` (
  `job_title_id` INT NOT NULL AUTO_INCREMENT,
  `job_title` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`job_title_id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Theme_Park_DB`.`employees`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Theme_Park_DB`.`employees` ;

CREATE TABLE IF NOT EXISTS `Theme_Park_DB`.`employees` (
  `ssn` INT NOT NULL,
  `theme_park_id` INT NOT NULL,
  `first_name` VARCHAR(45) NOT NULL,
  `last_name` VARCHAR(45) NOT NULL,
  `middle_initial` VARCHAR(1) NOT NULL,
  `full_time` TINYINT(1) NOT NULL,
  `payrate` DECIMAL(10,0) NOT NULL,
  `hired_date` DATE NOT NULL,
  `job_title_id` INT NOT NULL,
  `date_left` DATE NULL,
  `rehireable` TINYINT(1) NULL,
  PRIMARY KEY (`ssn`),
  INDEX `theme_park_id_employees_idx` (`theme_park_id` ASC),
  INDEX `job_title_id_idx` (`job_title_id` ASC),
  CONSTRAINT `theme_park_id_employees`
    FOREIGN KEY (`theme_park_id`)
    REFERENCES `Theme_Park_DB`.`theme_park` (`theme_park_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `job_title_id`
    FOREIGN KEY (`job_title_id`)
    REFERENCES `Theme_Park_DB`.`job_titles` (`job_title_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

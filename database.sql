-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.7.22-0ubuntu0.16.04.1 - (Ubuntu)
-- Server OS:                    Linux
-- HeidiSQL Version:             9.5.0.5278
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for dotnetcore_library
CREATE DATABASE IF NOT EXISTS `dotnetcore_library` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `dotnetcore_library`;

-- Dumping structure for table dotnetcore_library.BranchHours
CREATE TABLE IF NOT EXISTS `BranchHours` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BranchId` int(11) DEFAULT NULL,
  `DayOfWeek` int(11) NOT NULL,
  `OpenTime` int(11) NOT NULL,
  `CloseTime` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_BranchHours_BranchId` (`BranchId`),
  CONSTRAINT `FK_BranchHours_LibraryBranches_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `LibraryBranches` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.BranchHours: ~0 rows (approximately)
DELETE FROM `BranchHours`;
/*!40000 ALTER TABLE `BranchHours` DISABLE KEYS */;
INSERT INTO `BranchHours` (`Id`, `BranchId`, `DayOfWeek`, `OpenTime`, `CloseTime`) VALUES
	(22, 1, 1, 7, 14),
	(23, 1, 2, 7, 18),
	(24, 1, 3, 7, 18),
	(25, 1, 4, 7, 18),
	(26, 1, 5, 7, 18),
	(27, 1, 6, 7, 18),
	(28, 1, 7, 7, 14),
	(29, 2, 1, 6, 20),
	(30, 2, 2, 6, 20),
	(31, 2, 3, 6, 20),
	(32, 2, 4, 6, 20),
	(33, 2, 5, 6, 20),
	(34, 2, 6, 6, 20),
	(35, 2, 7, 6, 20),
	(36, 3, 1, 5, 22),
	(37, 3, 2, 5, 18),
	(38, 3, 3, 5, 18),
	(39, 3, 4, 5, 18),
	(40, 3, 5, 5, 18),
	(41, 3, 6, 5, 22),
	(42, 3, 7, 5, 22);
/*!40000 ALTER TABLE `BranchHours` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.Checkout
CREATE TABLE IF NOT EXISTS `Checkout` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LibraryAssetId` int(11) NOT NULL,
  `LibraryCardId` int(11) DEFAULT NULL,
  `Since` datetime(6) NOT NULL,
  `Until` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Checkout_LibraryAssetId` (`LibraryAssetId`),
  KEY `IX_Checkout_LibraryCardId` (`LibraryCardId`),
  CONSTRAINT `FK_Checkout_LibraryAssets_LibraryAssetId` FOREIGN KEY (`LibraryAssetId`) REFERENCES `LibraryAssets` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Checkout_LibraryCards_LibraryCardId` FOREIGN KEY (`LibraryCardId`) REFERENCES `LibraryCards` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.Checkout: ~0 rows (approximately)
DELETE FROM `Checkout`;
/*!40000 ALTER TABLE `Checkout` DISABLE KEYS */;
/*!40000 ALTER TABLE `Checkout` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.CheckoutHistory
CREATE TABLE IF NOT EXISTS `CheckoutHistory` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `LibraryAssetId` int(11) NOT NULL,
  `LibraryCardId` int(11) NOT NULL,
  `CheckedOut` datetime(6) NOT NULL,
  `CheckedIn` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_CheckoutHistory_LibraryAssetId` (`LibraryAssetId`),
  KEY `IX_CheckoutHistory_LibraryCardId` (`LibraryCardId`),
  CONSTRAINT `FK_CheckoutHistory_LibraryAssets_LibraryAssetId` FOREIGN KEY (`LibraryAssetId`) REFERENCES `LibraryAssets` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_CheckoutHistory_LibraryCards_LibraryCardId` FOREIGN KEY (`LibraryCardId`) REFERENCES `LibraryCards` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.CheckoutHistory: ~0 rows (approximately)
DELETE FROM `CheckoutHistory`;
/*!40000 ALTER TABLE `CheckoutHistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `CheckoutHistory` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.Holds
CREATE TABLE IF NOT EXISTS `Holds` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LibraryAssetId` int(11) DEFAULT NULL,
  `LibraryCardId` int(11) DEFAULT NULL,
  `HoldedPlaced` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Holds_LibraryAssetId` (`LibraryAssetId`),
  KEY `IX_Holds_LibraryCardId` (`LibraryCardId`),
  CONSTRAINT `FK_Holds_LibraryAssets_LibraryAssetId` FOREIGN KEY (`LibraryAssetId`) REFERENCES `LibraryAssets` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_Holds_LibraryCards_LibraryCardId` FOREIGN KEY (`LibraryCardId`) REFERENCES `LibraryCards` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.Holds: ~0 rows (approximately)
DELETE FROM `Holds`;
/*!40000 ALTER TABLE `Holds` DISABLE KEYS */;
/*!40000 ALTER TABLE `Holds` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.LibraryAssets
CREATE TABLE IF NOT EXISTS `LibraryAssets` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` longtext NOT NULL,
  `Year` int(11) NOT NULL,
  `StatusId` int(11) NOT NULL,
  `Cost` decimal(65,30) NOT NULL,
  `ImageUrl` longtext,
  `NumberOfCopies` int(11) NOT NULL,
  `LocationId` int(11) DEFAULT NULL,
  `Discriminator` longtext NOT NULL,
  `ISBN` longtext,
  `Author` longtext,
  `DeweyIndex` longtext,
  `Director` longtext,
  PRIMARY KEY (`Id`),
  KEY `IX_LibraryAssets_LocationId` (`LocationId`),
  KEY `IX_LibraryAssets_StatusId` (`StatusId`),
  CONSTRAINT `FK_LibraryAssets_LibraryBranches_LocationId` FOREIGN KEY (`LocationId`) REFERENCES `LibraryBranches` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_LibraryAssets_Statuses_StatusId` FOREIGN KEY (`StatusId`) REFERENCES `Statuses` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.LibraryAssets: ~0 rows (approximately)
DELETE FROM `LibraryAssets`;
/*!40000 ALTER TABLE `LibraryAssets` DISABLE KEYS */;
INSERT INTO `LibraryAssets` (`Id`, `Title`, `Year`, `StatusId`, `Cost`, `ImageUrl`, `NumberOfCopies`, `LocationId`, `Discriminator`, `ISBN`, `Author`, `DeweyIndex`, `Director`) VALUES
	(22, 'Emma', 1815, 2, 18.000000000000000000000000000000, '/images/emma.png', 1, 2, 'Book', '9781519202987', 'Jane Austen', '823.123', NULL),
	(23, 'Jane Eyre', 1847, 2, 18.000000000000000000000000000000, '/images/janeeyre.png', 1, 3, 'Book', '9781519133977', 'Charlotte Bront', '822.133', NULL),
	(24, 'Moby Dick', 1851, 2, 12.990000000000000000000000000000, '/images/mobydick.png', 1, 2, 'Book', '9780746062760', 'Herman Melville', '821.153', NULL),
	(25, 'Ulysses', 1922, 2, 24.000000000000000000000000000000, '/images/ulysses.png', 3, 2, 'Book', '9788854139343', 'James Joyce', '822.556', NULL),
	(26, 'Republic', -380, 2, 11.000000000000000000000000000000, '/images/republic.png', 2, 3, 'Book', '9780758320209', 'Plato', '820.119', NULL),
	(27, 'Dracula', 1897, 2, 18.000000000000000000000000000000, '/images/dracula.png', 4, 3, 'Book', '9781623750282', 'Bram Stoker', '821.526', NULL),
	(28, 'White Noise', 1985, 2, 12.990000000000000000000000000000, '/images/default.png', 1, 2, 'Book', '9780670803736', 'Don Delillo', '822.436', NULL),
	(29, 'Another Country', 1962, 2, 16.000000000000000000000000000000, '/images/default.png', 2, 2, 'Book', '9780552084574', 'James Baldwin', '821.325', NULL),
	(30, 'The Waves', 1931, 2, 11.000000000000000000000000000000, '/images/thewaves.png', 1, 1, 'Book', '9781904919582', 'Virginia Woolf', '822.888', NULL),
	(31, 'The Color Purple', 1982, 2, 11.990000000000000000000000000000, '/images/default.png', 2, 1, 'Book', '9780151191543', 'Alice Walker', '820.298', NULL),
	(32, 'One Hundred Years of Solitude', 1967, 2, 12.500000000000000000000000000000, '/images/default.png', 1, 1, 'Book', '9789631420494', 'Gabriel García Márquez', '821.544', NULL),
	(33, 'Friend of My Youth', 1990, 2, 22.000000000000000000000000000000, '/images/default.png', 1, 1, 'Book', '9788702163452', 'Alice Monroe', '821.444', NULL),
	(34, 'To the Lighthouse', 1927, 2, 13.500000000000000000000000000000, '/images/tothelighthouse.png', 5, 1, 'Book', '9780795310522', 'Virginia Woolf', '820.111', NULL),
	(35, 'Mrs Dalloway', 1925, 2, 15.990000000000000000000000000000, '/images/mrsdalloway.png', 1, 3, 'Book', '9785457626126', 'Virginia Woolf', '821.254', NULL),
	(36, 'Blue Velvet', 1986, 2, 24.000000000000000000000000000000, '/images/default.png', 1, 1, 'Video', NULL, NULL, NULL, 'David Lynch'),
	(37, 'Trois Coleurs: Rouge', 1994, 2, 24.000000000000000000000000000000, '/images/default.png', 1, 1, 'Video', NULL, NULL, NULL, 'Krzysztof Kieslowski'),
	(38, 'Citizen Kane', 1941, 2, 30.000000000000000000000000000000, '/images/default.png', 1, 1, 'Video', NULL, NULL, NULL, 'Orson Welles'),
	(39, 'Spirited Away', 2002, 2, 28.000000000000000000000000000000, '/images/default.png', 1, 2, 'Video', NULL, NULL, NULL, 'Hayao Miyazaki'),
	(40, 'The Departed', 2006, 2, 23.000000000000000000000000000000, '/images/default.png', 1, 2, 'Video', NULL, NULL, NULL, 'Martin Scorsese'),
	(41, 'Taxi Driver', 1976, 2, 17.990000000000000000000000000000, '/images/default.png', 1, 2, 'Video', NULL, NULL, NULL, 'Martin Scorsese'),
	(42, 'Casablanca', 1943, 2, 18.000000000000000000000000000000, '/images/default.png', 1, 3, 'Video', NULL, NULL, NULL, 'Michael Curtiz');
/*!40000 ALTER TABLE `LibraryAssets` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.LibraryBranches
CREATE TABLE IF NOT EXISTS `LibraryBranches` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  `Address` longtext NOT NULL,
  `Telephone` longtext NOT NULL,
  `Description` longtext,
  `OpenDate` datetime(6) NOT NULL,
  `ImageUrl` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.LibraryBranches: ~0 rows (approximately)
DELETE FROM `LibraryBranches`;
/*!40000 ALTER TABLE `LibraryBranches` DISABLE KEYS */;
INSERT INTO `LibraryBranches` (`Id`, `Name`, `Address`, `Telephone`, `Description`, `OpenDate`, `ImageUrl`) VALUES
	(1, 'Lake Shore Branch', '88 Lakeshore Dr', '555-1234', 'The oldest library branch in Lakeview, the Lake Shore Branch was opened in 1975. Patrons of all ages enjoy the wide selection of literature available at Lake Shore library. The coffee shop is open during library hours of operation.', '1975-05-13 00:00:00.000000', '/images/branches/1.png'),
	(2, 'Mountain View Branch', '123 Skyline Dr', '555-1235', 'The Mountain View branch contains the largest collection of technical and language learning books in the region. This branch is within walking distance to the University campus', '1998-06-01 00:00:00.000000', '/images/branches/2.png'),
	(3, 'Pleasant Hill Branch', '540 Inventors Circle', '555-1236', 'The newest Lakeview Library System branch, Pleasant Hill has high-speed wireless access for all patrons and hosts Chess Club every Monday and Wednesday evening at 6 PM.', '2017-09-20 00:00:00.000000', '/images/branches/3.png');
/*!40000 ALTER TABLE `LibraryBranches` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.LibraryCards
CREATE TABLE IF NOT EXISTS `LibraryCards` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Fees` decimal(65,30) NOT NULL,
  `Created` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.LibraryCards: ~0 rows (approximately)
DELETE FROM `LibraryCards`;
/*!40000 ALTER TABLE `LibraryCards` DISABLE KEYS */;
INSERT INTO `LibraryCards` (`Id`, `Fees`, `Created`) VALUES
	(1, 12.000000000000000000000000000000, '2017-06-20 00:00:00.000000'),
	(2, 0.000000000000000000000000000000, '2017-06-20 00:00:00.000000'),
	(3, 0.500000000000000000000000000000, '2017-06-21 00:00:00.000000'),
	(4, 0.000000000000000000000000000000, '2017-06-21 00:00:00.000000'),
	(5, 3.500000000000000000000000000000, '2017-06-21 00:00:00.000000'),
	(6, 1.500000000000000000000000000000, '2017-06-23 00:00:00.000000'),
	(7, 0.000000000000000000000000000000, '2017-06-23 00:00:00.000000'),
	(8, 8.000000000000000000000000000000, '2017-06-23 00:00:00.000000');
/*!40000 ALTER TABLE `LibraryCards` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.Patrons
CREATE TABLE IF NOT EXISTS `Patrons` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` longtext,
  `LastName` longtext,
  `Address` longtext,
  `DateOfBirth` datetime(6) NOT NULL,
  `TelephoneNumber` longtext,
  `HomeLibraryBranchId` int(11) DEFAULT NULL,
  `LibraryCardId` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Patrons_HomeLibraryBranchId` (`HomeLibraryBranchId`),
  KEY `IX_Patrons_LibraryCardId` (`LibraryCardId`),
  CONSTRAINT `FK_Patrons_LibraryBranches_HomeLibraryBranchId` FOREIGN KEY (`HomeLibraryBranchId`) REFERENCES `LibraryBranches` (`Id`) ON DELETE NO ACTION,
  CONSTRAINT `FK_Patrons_LibraryCards_LibraryCardId` FOREIGN KEY (`LibraryCardId`) REFERENCES `LibraryCards` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.Patrons: ~0 rows (approximately)
DELETE FROM `Patrons`;
/*!40000 ALTER TABLE `Patrons` DISABLE KEYS */;
INSERT INTO `Patrons` (`Id`, `FirstName`, `LastName`, `Address`, `DateOfBirth`, `TelephoneNumber`, `HomeLibraryBranchId`, `LibraryCardId`) VALUES
	(1, 'Jane', 'Patterson', '165 Peace St', '1986-07-10 00:00:00.000000', '555-1234', 1, 1),
	(2, 'Margaret', 'Smith', '324 Shadow Ln', '1984-03-12 00:00:00.000000', '555-7785', 2, 2),
	(3, 'Tomas', 'Galloway', '18 Stone Cir', '1956-02-10 00:00:00.000000', '555-3467', 2, 3),
	(4, 'Mary', 'Li', '246 Jennifer St', '1997-01-17 00:00:00.000000', '555-1223', 3, 4),
	(5, 'Dan', 'Carter', '1465 Williamson St', '1952-09-16 00:00:00.000000', '555-8884', 3, 5),
	(6, 'Aruna', 'Adhiban', '785 Park Ave', '1994-03-24 00:00:00.000000', '555-9988', 3, 6),
	(7, 'Raj', 'Prasad', '5654 Main St', '2001-11-23 00:00:00.000000', '555-7894', 1, 7),
	(8, 'Tatyana', 'Ponomaryova', '1352 Bicycle Ct', '1981-10-16 00:00:00.000000', '555-4568', 3, 8);
/*!40000 ALTER TABLE `Patrons` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.Statuses
CREATE TABLE IF NOT EXISTS `Statuses` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Description` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.Statuses: ~0 rows (approximately)
DELETE FROM `Statuses`;
/*!40000 ALTER TABLE `Statuses` DISABLE KEYS */;
INSERT INTO `Statuses` (`Id`, `Name`, `Description`) VALUES
	(1, 'Checked Out', 'A library asset that has been checked out'),
	(2, 'Available', 'A library asset that is available for loan'),
	(3, 'Lost', 'A library asset that has been lost'),
	(4, 'On Hold', 'A library asset that has been placed On Hold for loan');
/*!40000 ALTER TABLE `Statuses` ENABLE KEYS */;

-- Dumping structure for table dotnetcore_library.__EFMigrationsHistory
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Dumping data for table dotnetcore_library.__EFMigrationsHistory: ~2 rows (approximately)
DELETE FROM `__EFMigrationsHistory`;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
	('20180614200412_Initial migration', '2.1.0-rtm-30799'),
	('20180614211332_Entity data models', '2.1.0-rtm-30799');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;


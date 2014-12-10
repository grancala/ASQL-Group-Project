-- #############################################
-- Create custom dataTypes
-- #############################################
CREATE TYPE yearType FROM smallint NOT NULL;
CREATE TYPE monthType FROM tinyint NOT NULL;
CREATE TYPE uInt FROM int NOT NULL;
CREATE TYPE uFloat FROM float NOT NULL;


-- #############################################
-- Create and populate tables with 
-- required values
-- #############################################

-- =============================================
-- RegionData table
-- =============================================

----If the table already exists delete it.--
IF OBJECT_ID('dbo.RegionData') IS NOT NULL
	DROP TABLE dbo.RegionData
GO

----Create the table--
CREATE TABLE dbo.RegionData
(
	stateCode int NOT NULL,
	stateName nVarChar(100) NOT NULL,
	stateAcronym nVarChar(4) NOT NULL,
	CONSTRAINT PK_RegionData PRIMARY KEY (stateCode)
);
GO


INSERT INTO dbo.RegionData VALUES
	(1,'Alabama','AL'),
(2,'Arizona','AZ'),
(3,'Arkansas','AR'),
(4,'California','CA'),
(5,'Colorado','CO'),
(6,'Connecticut','CT'),
(7,'Delaware','DE'),
(8,'Florida','FL'),
(9,'Georgia','GA'),
(10,'Idaho','ID'),
(11,'Illinois','IL'),
(12,'Indiana','IN'),
(13,'Iowa','IA'),
(14,'Kansas','KS'),
(15,'Kentucky','KY'),
(16,'Louisiana','LA'),
(17,'Maine','ME'),
(18,'Maryland','MD'),
(19,'Massachusetts','MA'),
(20,'Michigan','MI'),
(21,'Minnesota','MN'),
(22,'Mississippi','MS'),
(23,'Missouri','MO'),
(24,'Montana','MT'),
(25,'Nebraska','NE'),
(26,'Nevada','NV'),
(27,'New Hampshire','NH'),
(28,'New Jersey','NJ'),
(29,'New Mexico','NM'),
(30,'New York','NY'),
(31,'North Carolina','NC'),
(32,'North Dakota','ND'),
(33,'Ohio','OH'),
(34,'Oklahoma','OK'),
(35,'Oregon','OR'),
(36,'Pennsylvania','PA'),
(37,'Rhode Island','RI'),
(38,'South Carolina','SC'),
(39,'South Dakota','SD'),
(40,'Tennessee','TN'),
(41,'Texas','TX'),
(42,'Utah','UT'),
(43,'Vermont','VT'),
(44,'Virginia','VA'),
(45,'Washington','WA'),
(46,'West Virginia','WV'),
(47,'Wisconsin','WI'),
(48,'Wyoming','WY'),
(101,'Northeast Region','NER'),
(102,'East North Central Region','ENCR'),
(103,'Central Region','CR'),
(104,'Southeast Region','SER'),
(105,'West North Central Region','WNCR'),
(106,'South Region','SR'),
(107,'Southwest Region','SWR'),
(108,'Northwest Region','NWR'),
(109,'West Region','WR'),
(110,'National','US')


-- =============================================
-- Users table
-- =============================================

----If the table already exists delete it.--
IF OBJECT_ID('dbo.Users') IS NOT NULL
  DROP TABLE dbo.Users
GO

----Create the table--
CREATE TABLE dbo.Users
(
	userID int NOT NULL IDENTITY(1,1),
	userName nVarChar(50) NOT NULL UNIQUE,
	userPassword nVarChar(25),
	userDatabase nVarChar(50),
	CONSTRAINT PK_Users PRIMARY KEY (userID)
);
GO



-- =============================================
-- GraphTypes Table
-- =============================================

----If the table already exists delete it.--
IF OBJECT_ID('dbo.GraphTypes') IS NOT NULL
  DROP TABLE dbo.GraphTypes
GO

----Create the table--
CREATE TABLE dbo.GraphTypes
(
	typeID int NOT NULL IDENTITY(1,1),
	typeName nVarChar(50) NOT NULL UNIQUE,
	CONSTRAINT PK_GraphTypes PRIMARY KEY (typeID)
);
GO

----Populate the table--
INSERT INTO dbo.GraphTypes VALUES 
	('Precipitation'),
	('Cooling Days/Heating Days'),
	('Temperature (Min/Max/Avg)');
GO

 
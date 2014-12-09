-- #############################################
-- Creation of the database
-- #############################################
USE master
GO

-- Drop the database if it already exists
IF  EXISTS (
	SELECT name 
		FROM sys.databases 
		WHERE name = N'ASQLGroup'
)
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'ASQLGroup'
	ALTER DATABASE ASQLGroup SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE ASQLGroup
END
GO



CREATE DATABASE ASQLGroup
GO



USE ASQLGroup
GO
-- #############################################
-- Create custom dataTypes
-- #############################################
CREATE TYPE yearType FROM smallint NOT NULL;
CREATE TYPE monthType FROM tinyint NOT NULL;
CREATE TYPE uInt FROM int NOT NULL;
CREATE TYPE uFloat FROM float NOT NULL;
GO
CREATE RULE uIntRule as @value >= 0;
GO
CREATE RULE uFloatRule AS @value >= 0;
GO
CREATE RULE yearTypeRule as @value >= 0;
GO
CREATE RULE monthTypeRule as @value >= 1  AND @value <= 12;
GO

EXEC sp_bindrule 'uIntRule', 'uInt';
EXEC sp_bindrule 'uFloatRule', 'uFloat';
EXEC sp_bindrule 'yearTypeRule', 'yearType';
EXEC sp_bindrule 'monthTypeRule', 'monthType';
GO

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


----Populate the table--
--&Variables&--
DECLARE @regionsCsvPath VARCHAR(100) = 'C:\Users\Nick\Documents\GitHub\ASQL-Group-Project\ASQL4\ASQL4\Regions.csv'
DECLARE @csvDelimiter varchar(5) = ','
DECLARE @csvEndOfRow varchar(5) = '\n'

--Code--
DECLARE @sql_bulk VARCHAR(max) = 'BULK INSERT dbo.RegionData
FROM ''' + @regionsCsvPath + '''
WITH ( FIELDTERMINATOR = ''' + @csvDelimiter + ''', ROWTERMINATOR = ''' + @csvEndOfRow + ''')'
EXEC(@sql_bulk)
GO 


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
INSERT INTO dbo.GraphTypes VALUES ('Precipitation');
INSERT INTO dbo.GraphTypes VALUES ('Cooling Days/Heating Days');
INSERT INTO dbo.GraphTypes VALUES ('Temperature (Min/Max/Avg)');
GO

 
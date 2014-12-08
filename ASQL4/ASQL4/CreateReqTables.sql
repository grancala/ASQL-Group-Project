-- =========================================
-- Create table template
-- =========================================
USE ASQLGroup
GO

IF OBJECT_ID('dbo.RegionData') IS NOT NULL
  DROP TABLE dbo.RegionData
GO

CREATE TABLE dbo.RegionData
(
	stateCode int NOT NULL,
	stateName nVarChar(100) NOT NULL,
	stateAcronym nVarChar(4) NOT NULL,
	CONSTRAINT PK_RegionData PRIMARY KEY (stateCode)
);
GO

IF OBJECT_ID('dbo.Users') IS NOT NULL
  DROP TABLE dbo.Users
GO

CREATE TABLE dbo.Users
(
	userID int NOT NULL,
	userName nVarChar(50) NOT NULL UNIQUE,
	userPassword nVarChar(25),
	userDatabase nVarChar(50),
	CONSTRAINT PK_Users PRIMARY KEY (userID)
);
GO


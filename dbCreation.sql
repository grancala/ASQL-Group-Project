CREATE DATABASE ASQLGroup;
GO
USE ASQLGroup;
GO

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


CREATE TABLE RegionData
(
stateCode int NOT NULL PRIMARY KEY,
stateName nVarChar(50) NOT NULL,
stateAcronym nVarChar(3) NOT NULL
);


CREATE TABLE Users
(
userID int NOT NULL PRIMARY KEY,
userName nVarChar(50) NOT NULL UNIQUE,
userPassword nVarChar(25),
userDatabase nVarChar(50)
);
INSERT INTO Users VALUES ('1','Demo','Incorrect', 'Demo_12_21_2012');


CREATE TABLE Demo_12_21_2012
(
stateCode int 
	NOT NULL
	FOREIGN KEY REFERENCES RegionData(stateCode),
data_year yearType 
	NOT NULL,
data_month monthType
	NOT NULL,	 
	PCP money,
	CDD int,
	HDD int,
	TAVG money,
	TMIN money,
	TMAX money
	
	CONSTRAINT ID 
	PRIMARY KEY (stateCode,data_year,data_month)
	);




USE master
GO

if not exists (select * from master..syslogins where name = N'dbAccessor')
BEGIN
        exec sp_addlogin N'dbAccessor', N'wingding', N'ASQLGroup', @@language
END
GO

USE master
GO
USE ASQLGroup
    exec sp_grantdbaccess N'dbAccessor'
    exec sp_addrolemember 'db_owner','dbAccessor'
GO

USE master
GO
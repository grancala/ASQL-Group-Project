-- #############################################
-- Population of test data into the database
-- #############################################
USE ASQLGroup
GO


CREATE PROC #tempCreateTable
	@userTableName VARCHAR(50)
	AS
	DECLARE @tableCreate VARCHAR(max) = 
	'CREATE TABLE ' + @userTableName + 
	'(
		stateCode int NOT NULL,
		data_year yearType NOT NULL,
		data_month monthType NOT NULL,	 
		PCP decimal,
		CDD int,
		HDD int,
		TAVG money,
		TMIN money,
		TMAX money

		CONSTRAINT FK_' + @userTableName + '_stateCode FOREIGN KEY(stateCode)
			REFERENCES RegionData(stateCode),
		CONSTRAINT PK_'  + @userTableName + ' PRIMARY KEY
			(stateCode,data_year,data_month)
	)'
	EXEC(@tableCreate)
	GO

CREATE PROC #tempPopulateTable 
	@userTableName varchar(50),
	@userCsvPath varchar(100)

	AS
	DECLARE @sql_bulk VARCHAR(max) = 'BULK INSERT dbo.' + @userTableName +
	' FROM ''' + @userCsvPath + '''
	WITH ( FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'')'
	EXEC(@sql_bulk)
GO 

CREATE PROC #tempAddUser
	@userName nVarChar(50),
	@userPassword nVarChar(25),
	@userTableName nVarChar(50)
	AS
	INSERT INTO dbo.Users VALUES (@userName,@userPassword, @userTableName);
GO

CREATE PROCEDURE #tempAddPackage
	@userTableName varchar(50),
	@userCsvPath varchar(100),
	@userName nVarChar(50),
	@userPassword nVarChar(25)

	AS
	EXEC #tempCreateTable @userTableName=@userTableName;
	EXEC #tempPopulateTable @userTableName=@userTableName, @userCsvPath = @userCsvPath;
	EXEC #tempAddUser 
		@userName = @userName,
		@userPassword = @userPassword,
		@userTableName = @userTableName;
GO

EXEC #tempAddPackage
	@userTableName = 'Demo_2_12_2014',
	@userCsvPath = 'C:\Users\Jim\Documents\GitHub\ASQL-Group-Project\ASQL4\ASQL4\DemoDBValues.csv',
	--@userCsvPath = 'C:\Users\Nick\Documents\GitHub\ASQL-Group-Project\ASQL4\ASQL4\DemoDBValues.csv',
	@userName = 'Demo',
	@userPassword = 'Password';

GO 




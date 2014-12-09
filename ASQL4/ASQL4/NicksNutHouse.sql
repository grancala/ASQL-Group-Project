-- ===============================================
-- Create stored procedure with OUTPUT parameters
-- ===============================================
-- Drop stored procedure if it already exists
USE ASQLGroup


-- CREATE TABLE
IF OBJECT_ID('CreateTable', 'P') IS NOT NULL
    DROP PROCEDURE CreateTable;
GO

CREATE PROCEDURE CreateTable
	@userName nVarChar(50),
	@userPassword nVarChar(25),
	@overWrite bit = 0,
	@successful bit OUTPUT
	WITH EXECUTE AS Owner
AS
	-- table naming username_YYYYMMDD
	DECLARE @TableName nvarchar(50);

	SET @successful = 1

	-- check if user has table
	SELECT @TableName = [userDatabase]
		FROM [ASQLGroup].[dbo].[Users]
		WHERE [ASQLGroup].[dbo].[Users].[userName] = @userName AND
		[ASQLGroup].[dbo].[Users].[userPassword] = @userPassword;
	
	IF @TableName IS NOT NULL
	BEGIN
	-- table exists
		-- if overwrite
			-- drop table
		-- else
			-- select 0
		IF @overWrite = 1
		BEGIN
			DECLARE @SQL VARCHAR(max) = 
			'DROP TABLE ASQLGroup.dbo.' + @TableName
			EXEC(@SQL);
			SET @SQL = 'GRANT ALTER ON '+ @TableName + ' TO dbAccessor'
			EXEC(@SQL);
		END
		ELSE
		BEGIN
			set @successful = 0
		END
	END
	
	if @successful = 1
	BEGIN
		-- generate table name
		DECLARE @NewTableName nvarchar(50);
		SELECT @NewTableName = @userName + '_' + CONVERT(varchar,GETDATE(),112);
		
		-- generate table
		DECLARE @tableCreate VARCHAR(max) = 
			'CREATE TABLE ' + @NewTableName + 
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

			CONSTRAINT FK_' + @NewTableName + '_stateCode FOREIGN KEY(stateCode)
				REFERENCES RegionData(stateCode),
			CONSTRAINT PK_'  + @NewTableName + ' PRIMARY KEY
				(stateCode,data_year,data_month)
			)'
		EXEC(@tableCreate)

		-- assign table name
		UPDATE [ASQLGroup].[dbo].[Users]
			SET [ASQLGroup].[dbo].[Users].[userDatabase] = @NewTableName
			WHERE [ASQLGroup].[dbo].[Users].[userName] = @userName 
			AND [ASQLGroup].[dbo].[Users].[userPassword] = @userPassword
		SET @successful = 1
	END
GO


-- TABLE EXISTS
IF OBJECT_ID('TableExists', 'P') IS NOT NULL
    DROP PROCEDURE TableExists;
GO


CREATE PROCEDURE TableExists
	@userName nVarChar(50),
	@userPassword nVarChar(25),
	@dataCount int OUTPUT
AS
	DECLARE @TableName nvarchar(50);
	
	SET @dataCount = 0;

	-- check if user has table
	SELECT @TableName = [userDatabase]
		FROM [ASQLGroup].[dbo].[Users]
		WHERE [ASQLGroup].[dbo].[Users].[userName] = @userName AND
		[ASQLGroup].[dbo].[Users].[userPassword] = @userPassword
	
	IF @TableName IS NULL
	BEGIN
		SET @dataCount = -1
	END

	IF @dataCount = 0
	BEGIN
		DECLARE @Count int
		DECLARE @sqlCommand nvarchar(1000)
		DECLARE @params nvarchar(1000)
		
		SET @sqlCommand = 
		'SELECT @cnt = COUNT(*) FROM' + quotename(@TableName)
		SET @params =
		N'@cnt int OUTPUT'

		EXECUTE sp_executesql @sqlCommand, @params, @Count OUTPUT

		IF @Count = 0
		BEGIN
			SET @dataCount = 0
		END
		ELSE
		BEGIN
			SET @dataCount = @Count
		END
	END
GO


-- CREATE USER
IF OBJECT_ID('CreateUser', 'P') IS NOT NULL
    DROP PROCEDURE CreateUser;
GO

CREATE PROCEDURE CreateUser
	@userName nVarChar(50),
	@rowCount int output
AS
	INSERT INTO Users(userName,userPassword)
		VALUES (@userName, 'Incorrect')
	SET @rowCount = @@ROWCOUNT
GO


-- UPDATE USER
IF OBJECT_ID('UpdateUser', 'P') IS NOT NULL
    DROP PROCEDURE UpdateUser;
GO

CREATE PROCEDURE UpdateUser
	@userName nVarChar(50),
	@oldPass varchar(25),
	@newPass varchar(25),
	@rowCount int output
AS
	UPDATE Users
		SET userPassword = @newPass
		WHERE userName = @userName
		AND	userPassword = @oldPass
	SET @rowCount = @@ROWCOUNT
GO


-- DROP USER
IF OBJECT_ID('DropUser', 'P') IS NOT NULL
    DROP PROCEDURE DropUser;
GO

CREATE PROCEDURE DropUser
	@userName nVarChar(50),
	@userPass nVarChar(25),
	@rowCount int output
AS
	DECLARE @TableName varChar(50)

	SELECT @TableName = [userDatabase]
		FROM [ASQLGroup].[dbo].[Users]
		WHERE [ASQLGroup].[dbo].[Users].[userName] = @userName AND
		[ASQLGroup].[dbo].[Users].[userPassword] = @userPass
	
	IF @TableName IS NOT NULL
	BEGIN
		DECLARE @sql varchar(1000)
		SET @sql = 'DROP TABLE ' + @TableName
		EXEC(@sql)
	END

	DELETE FROM Users
		WHERE userName = @userName
		AND userPassword = @userPass
	SET @rowCount = @@ROWCOUNT
GO


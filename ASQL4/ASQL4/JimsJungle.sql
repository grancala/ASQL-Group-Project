Use ASQLGroup;
GO

IF OBJECT_ID('verifyUser', 'P') IS NOT NULL
    DROP PROCEDURE verifyUser;
GO

CREATE PROCEDURE verifyUser
    @userName nVarchar(50),
    @password nVarChar(50)
AS

    IF(SELECT COUNT(*) FROM Users
        WHERE userName = @userName
        AND userPassword = @password) = 0
        BEGIN
        RETURN(1)
    END
	RETURN(0)

GO

IF OBJECT_ID('getDBName', 'P') IS NOT NULL
    DROP PROCEDURE getDBName;
GO

CREATE PROCEDURE getDBName
    @userName nVarchar(50),
    @password nVarChar(50),
    @userTable nVarChar(100) output

AS

	DECLARE @retCode int;
	EXECUTE @retCode = verifyUser @username, @password;

    IF @retCode = 0
    BEGIN
        SELECT @userTable = userDatabase
        FROM Users
        WHERE userName = @userName;
        RETURN 0
    END
    ELSE
	BEGIN
		RETURN (1)
    END
    GO

IF OBJECT_ID('getMaxMin', 'P') IS NOT NULL
	DROP PROCEDURE getMaxMin;
GO

Create Procedure getMaxMin
	@userName nVarchar(50),
	@password nVarChar(50),
	@minDate date OUTPUT,
	@maxDate date OUTPUT
AS
	DECLARE	@retCode int
	
	Declare @tableName varChar(50)
	EXEC @retCode = dbo.getDBName
		@userName = @userName,
		@password = @password,
		@userTable = @tableName OUTPUT;
	
	    
	IF @retCode = 0
	BEGIN
		DECLARE @sql nvarchar(4000),@params nvarchar(4000)

		SELECT @sql =
			N'SELECT @minDate = MIN(DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1))), ' +
			'@maxDate = MAX(DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1))) '+
			'FROM ' + @tableName
		SELECT @params = 
			N'@minDate date OUTPUT, ' +
			N'@maxDate date OUTPUT'
		EXEC sp_executesql @sql, @params, 
			@minDate = @minDate OUTPUT, 
			@maxDate = @maxDate OUTPUT
		RETURN 0
	END
	ELSE
	BEGIN
		RETURN (1)
	END
GO
IF OBJECT_ID('getRegions', 'P') IS NOT NULL
	DROP PROCEDURE getRegions;
GO
CREATE PROCEDURE getRegions
	@userName nVarchar(50),
	@password nVarChar(50)
	AS
	DECLARE	@retCode int
	Declare @tableName varChar(50)
	EXEC @retCode = dbo.getDBName
		@userName = @userName,
		@password = @password,
		@userTable = @tableName OUTPUT;

	DECLARE @sql nVarChar(max) = 'SELECT DISTINCT stateCode ' +
                'FROM [ASQLGroup].[dbo].[' + @tableName + ']';

	EXEC @sql
	GO
Use ASQLGroup;
GO

IF OBJECT_ID('getDataBetweenTwoDates', 'P') IS NOT NULL
    DROP PROCEDURE getDataBetweenTwoDates;
GO

CREATE PROCEDURE getDataBetweenTwoDates
    @tableName nVarchar(50) = 'dbo.Demo_2_12_2014',
    @firstDate nVarChar(50) = '1951-1-1',
    @secondDate nVarChar(50) = '1952-1-2',
    @columns nVarChar(50) = 'HDD, PCP'
AS
	DECLARE @sqlQuery VARCHAR(max) = 'Select ' + @columns + ' FROM ' +  @tableName + 
		' WHERE CONVERT(DATE, CAST(data_month AS varchar(2)) + ''/01/'' + CAST(data_year AS varchar(4)) , 101) >= ''' + @firstDate + '''
		AND CONVERT(DATE, CAST(data_month AS varchar(2)) + ''/01/'' + CAST(data_year AS varchar(4)) , 101) <= ''' +  @secondDate + '''';
	EXEC (@sqlQuery);
GO


IF OBJECT_ID('SearchV2', 'P') IS NOT NULL
    DROP PROCEDURE SearchV2;
GO

CREATE PROCEDURE SearchV2(
    @stateCodeIn int,
    @dbName nVarChar(50),
    @TheStart Date,
    @TheEnd Date)
AS
	DECLARE @dateInfo date = DATEADD(YEAR, DATEPART(YEAR, @TheStart) - 1900, DATEADD(qq,  DATEPART(QUARTER, @TheStart) - 1, -1));
	DECLARE @dateInfoEnd date = DATEADD(YEAR, DATEPART(YEAR, @TheEnd) - 1900, DATEADD(qq,  DATEPART(QUARTER, @TheEnd), -1));

	DECLARE @bigSQL varChar(max) = 
		'SELECT data_year as [Year],
		CASE
			WHEN data_month IN (1, 2, 3) THEN 1
			WHEN data_month IN (4, 5, 6) THEN 2
			WHEN data_month IN (7, 8, 9) THEN 3
			ELSE 4
			END As [Quarter],
			AVG(PCP) AS PCP
			FROM ' + @dbName + ' 
			
			 WHERE stateCode = ' + CONVERT(varChar(10),@stateCodeIn)  + 
			' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) > ''' +CONVERT(varChar(10), @dateInfo) +
			''' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) < ''' + CONVERT(varChar(10), @dateInfoEnd) + 
			'''	GROUP BY
			data_year,
			CASE
			    WHEN data_month IN (1, 2, 3) THEN 1
			    WHEN data_month IN (4, 5, 6) THEN 2
			    WHEN data_month IN (7, 8, 9) THEN 3
			    ELSE 4
			END
		ORDER BY
		    [Year],
		    [Quarter]
		;'

	EXEC(@bigSQL)
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
		@userName = 'Demo',
		@password = N'Password',
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

CREATE PROCEDURE getRegions
	
	AS
	DECLARE	@retCode int
	Declare @tableName varChar(50)
	EXEC @retCode = dbo.getDBName
		@userName = 'Demo',
		@password = N'Password',
		@userTable = @tableName OUTPUT;

	DECLARE @sql nVarChar(max) = 'SELECT DISTINCT stateCode ' +
                'FROM [ASQLGroup].[dbo].[Demo_2_12_2014]';

	EXEC @sql
	GO
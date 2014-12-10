IF OBJECT_ID('GenericSearchYearly', 'P') IS NOT NULL
    DROP PROCEDURE GenericSearchYearly;
GO

CREATE PROCEDURE GenericSearchYearly(
    @stateCodeIn int,
    @dbName nVarChar(50),
    @TheStart Date,
    @TheEnd Date,
	@ReturnColumns nVarChar(100)
	)
AS
	DECLARE @dateInfo date = DATEADD(YEAR, DATEPART(YEAR, @TheStart) - 1900, -1);
	DECLARE @dateInfoEnd date = DATEADD(YEAR, DATEPART(YEAR, @TheEnd) - 1900, -1);

	DECLARE @bigSQL varChar(max) = 
		'SELECT data_year as [Year]'
			+ @returnColumns +
			
			'FROM ' + @dbName + ' 
			
			 WHERE stateCode = ' + CONVERT(varChar(10),@stateCodeIn)  + 
			' AND DATEADD(YEAR, data_year-1900, -1) >= ''' +CONVERT(varChar(10), @dateInfo) +
			''' AND DATEADD(YEAR, data_year-1900, -1) <= ''' + CONVERT(varChar(10), @dateInfoEnd) + 
			'''	GROUP BY
			data_year
		ORDER BY
		    [Year]

		;'

	EXEC(@bigSQL)
GO

IF OBJECT_ID('GenericSearchQuarterly', 'P') IS NOT NULL
    DROP PROCEDURE GenericSearchQuarterly;
GO

CREATE PROCEDURE GenericSearchQuarterly(
    @stateCodeIn int,
    @dbName nVarChar(50),
    @TheStart Date,
    @TheEnd Date,
	@ReturnColumns nVarChar(100)
	)
AS
	DECLARE @dateInfo date = DATEADD(YEAR, DATEPART(YEAR, @TheStart) - 1900, DATEADD(qq,  DATEPART(QUARTER, @TheStart), -1));
	DECLARE @dateInfoEnd date = DATEADD(YEAR, DATEPART(YEAR, @TheEnd) - 1900, DATEADD(qq,  DATEPART(QUARTER, @TheEnd), -1));

	DECLARE @bigSQL varChar(max) = 
		'SELECT data_year as [Year],
		CASE
			WHEN data_month IN (1, 2, 3) THEN 1
			WHEN data_month IN (4, 5, 6) THEN 2
			WHEN data_month IN (7, 8, 9) THEN 3
			ELSE 4
			END As [Quarter]'
			+ @returnColumns +
			
			'FROM ' + @dbName + ' 
			
			 WHERE stateCode = ' + CONVERT(varChar(10),@stateCodeIn)  + 
			' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) >= ''' +CONVERT(varChar(10), @dateInfo) +
			''' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) <= ''' + CONVERT(varChar(10), @dateInfoEnd) + 
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

IF OBJECT_ID('GenericSearchMonthly', 'P') IS NOT NULL
    DROP PROCEDURE GenericSearchMonthly;
GO

CREATE PROCEDURE GenericSearchMonthly(
    @stateCodeIn int,
    @dbName nVarChar(50),
    @TheStart Date,
    @TheEnd Date,
	@ReturnColumns nVarChar(100)
	)
AS
	DECLARE @dateInfo date = DATEADD(YEAR, DATEPART(YEAR, @TheStart) - 1900, DATEADD(MM,  DATEPART(MONTH, @TheStart), -1));
	DECLARE @dateInfoEnd date = DATEADD(YEAR, DATEPART(YEAR, @TheEnd) - 1900, DATEADD(MM,  DATEPART(MONTH, @TheEnd), -1));

	DECLARE @bigSQL varChar(max) = 
		'SELECT data_year as [Year], 
				data_month as [Month]'
				+ @returnColumns +
			'FROM ' + @dbName + ' 
			
			 WHERE stateCode = ' + CONVERT(varChar(10),@stateCodeIn)  + 
			' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) >= ''' +CONVERT(varChar(10), @dateInfo) +
			''' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) <= ''' + CONVERT(varChar(10), @dateInfoEnd) + 
			'''	GROUP BY
			data_year,
			data_month
		ORDER BY
		    [Year],
		    [Month]
		;'

	EXEC(@bigSQL)
GO

IF OBJECT_ID('GenericSearch', 'P') IS NOT NULL
    DROP PROCEDURE GenericSearch;
GO

CREATE PROCEDURE GenericSearch(
    @userName nVarChar(50),
	@password nVarChar(50),
	@stateCodeIn int,
    @searchType int,
	@timeIncrement int,
    @rangeStart Date,
    @rangeEnd Date
	
	--@searchType int
	)
AS
	--Declare @userName nVarChar(50) = N'Demo'
	--Declare @password nVarChar(50) = N'Password'
	--Declare @stateCodeIn int = 101
    
	--Declare @timeIncrement int = 2
   -- Declare @rangeStart Date = DATEADD(YEAR, 1953 - 1900, DATEADD(MM,  01, -1));
    --Declare @rangeEnd Date  = DATEADD(YEAR, 1960 - 1900, DATEADD(MM,  04, -1))
	
	
	DECLARE	@return_value int,
			@userTable nVarChar(50)


	EXEC @return_value = getDBName
		@userName = @userName,
		@password = @password,
		@userTable = @userTable OUTPUT

	IF @return_value = 0
		BEGIN	
		Declare @ReturnColumns nVarChar(100) = 
			CASE @searchType
				WHEN 2 THEN ',	AVG(CDD) AS CDD, AVG(HDD) AS HDD '
				WHEN 3 THEN ',	MAX(TMAX) AS TMAX, MIN(TMIN) AS TMIN, AVG(TAVG) AS TAVG '
				ELSE ', AVG(PCP) AS PCP '
			END
		IF @timeIncrement = 1
			BEGIN
			EXEC GenericSearchMonthly 
				@stateCodeIn = @stateCodeIn,
				@dbName = @userTable,
				@TheStart = @rangeStart,
				@TheEnd = @rangeEnd,
				@ReturnColumns = @ReturnColumns
			END
		ELSE
			BEGIN
			IF @timeIncrement = 2
				BEGIN
				EXEC GenericSearchQuarterly 
					@stateCodeIn = @stateCodeIn,
					@dbName = @userTable,
					@TheStart = @rangeStart,
					@TheEnd = @rangeEnd,
					@ReturnColumns = @ReturnColumns
				END
			ELSE
				BEGIN
				IF @timeIncrement = 3
					BEGIN
					EXEC GenericSearchYearly 
						@stateCodeIn = @stateCodeIn,
						@dbName = @userTable,
						@TheStart = @rangeStart,
						@TheEnd = @rangeEnd,
						@ReturnColumns = @ReturnColumns
					END
				ELSE
					BEGIN
					Return(1)
					END
				END
			END
		END
	ELSE
		BEGIN
		RETURN(1)
		END

	RETURN(0)
GO
IF OBJECT_ID('SearchQuarterlyPCP', 'P') IS NOT NULL
    DROP PROCEDURE SearchQuarterlyPCP;
GO

CREATE PROCEDURE SearchQuarterlyPCP(
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
			SUM(PCP) AS PCP
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



IF OBJECT_ID('SearchMonthlyPCP', 'P') IS NOT NULL
    DROP PROCEDURE SearchMonthlyPCP;
GO

CREATE PROCEDURE SearchMonthlyPCP(
    @stateCodeIn int,
    @dbName nVarChar(50),
    @TheStart Date,
    @TheEnd Date)
AS
	DECLARE @dateInfo date = DATEADD(YEAR, DATEPART(YEAR, @TheStart) - 1900, DATEADD(MM,  DATEPART(MONTH, @TheStart) - 1, -1));
	DECLARE @dateInfoEnd date = DATEADD(YEAR, DATEPART(YEAR, @TheEnd) - 1900, DATEADD(MM,  DATEPART(MONTH, @TheEnd)+1, -1));

	DECLARE @bigSQL varChar(max) = 
		'SELECT data_year as [Year], 
				data_month as [Month],
			SUM(PCP) AS PCP
			FROM ' + @dbName + ' 
			
			 WHERE stateCode = ' + CONVERT(varChar(10),@stateCodeIn)  + 
			' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) > ''' +CONVERT(varChar(10), @dateInfo) +
			''' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) < ''' + CONVERT(varChar(10), @dateInfoEnd) + 
			'''	GROUP BY
			data_year,
			data_month
		ORDER BY
		    [Year],
		    [Month]
		;'

	EXEC(@bigSQL)
GO


IF OBJECT_ID('SearchYearlyPCP', 'P') IS NOT NULL
    DROP PROCEDURE SearchYearlyPCP;
GO

CREATE PROCEDURE SearchYearlyPCP(
    @stateCodeIn int,
    @dbName nVarChar(50),
    @TheStart Date,
    @TheEnd Date)
AS
	DECLARE @dateInfo date = DATEADD(YEAR, DATEPART(YEAR, @TheStart) - 1900, DATEADD(MM,  DATEPART(MONTH, @TheStart) - 1, -1));
	DECLARE @dateInfoEnd date = DATEADD(YEAR, DATEPART(YEAR, @TheEnd) - 1900, DATEADD(MM,  DATEPART(MONTH, @TheEnd)+1, -1));

	DECLARE @bigSQL varChar(max) = 
		'SELECT data_year as [Year]
			SUM(PCP) AS PCP
			FROM ' + @dbName + ' 
			
			 WHERE stateCode = ' + CONVERT(varChar(10),@stateCodeIn)  + 
			' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) > ''' +CONVERT(varChar(10), @dateInfo) +
			''' AND DATEADD(YEAR, data_year-1900, DATEADD(MM, data_month, -1)) < ''' + CONVERT(varChar(10), @dateInfoEnd) + 
			'''	GROUP BY
			data_year,
			data_month
		ORDER BY
		    [Year]
		;'

	EXEC(@bigSQL)
GO


IF OBJECT_ID('SearchQuarterlyCDD_HDD', 'P') IS NOT NULL
    DROP PROCEDURE SearchQuarterlyCDD_HDD;
GO

CREATE PROCEDURE SearchQuarterlyCDD_HDD(
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
			SUM(CDD) AS CDD,
			SUM(HDD) AS HDD
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










IF OBJECT_ID('testMonthlyPCP', 'P') IS NOT NULL
    DROP PROCEDURE testMonthlyPCP;
GO
CREATE PROCEDURE testMonthlyPCP

AS
DECLARE @start date = DATEADD(YEAR, 1953 - 1900, DATEADD(MM,  01, -1));
	DECLARE @end date = DATEADD(YEAR, 1960 - 1900, DATEADD(MM,  04, -1))



DECLARE	@return_value int,
		@userTable nVarChar(50)


EXEC	@return_value = getDBName
		@userName = N'Demo',
		@password = N'Password',
		@userTable = @userTable OUTPUT


EXEC SearchQuarterlyCDD_HDD
	 @stateCodeIn  = 101,
    @dbName = @userTable,
    @TheStart = @start,
    @TheEnd =@end
	GO
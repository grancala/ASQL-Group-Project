USE [ASQLGroup]
GO

/*
DECLARE	@return_value int,
		@minDate date,
		@maxDate date

EXEC	@return_value = [dbo].[getMaxMin]
		@userName = N'Demo',
		@password = N'Password',
		@minDate = @minDate OUTPUT,
		@maxDate = @maxDate OUTPUT

SELECT	@minDate as N'minDate',
		@maxDate as N'maxDate'

SELECT	'Return Value' = @return_value

GO
*/
--Exec getDataBetweenTwoDates
--Go

Declare @TempStart Date = CONVERT(DATE, '20010101', 121)
Declare @TempEnd Date = CONVERT(DATE, '20130101', 121)


--EXEC SearchV2 @stateCodeIn = 101, @dbName = 'dbo.Demo_2_12_2014', @TheStart = @TempStart, @TheEnd = @TempEnd

--Declare @userName nVarChar(50) = N'Demo'
	--Declare @password nVarChar(50) = N'Password'
	--Declare @stateCodeIn int = 101
    
	--Declare @timeIncrement int = 2
   -- Declare @rangeStart Date = DATEADD(YEAR, 1953 - 1900, DATEADD(MM,  01, -1));
    --Declare @rangeEnd Date  = DATEADD(YEAR, 1960 - 1900, DATEADD(MM,  04, -1))
EXEC GenericSearch
@userName = N'Demo',
@password = N'Password',
@stateCodeIn = 101,
@searchType = 1,
@timeIncrement = 3,
@rangeStart = @TempStart,
@rangeEnd = @TempEnd
	
	
	GO
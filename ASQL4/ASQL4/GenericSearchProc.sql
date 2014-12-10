IF OBJECT_ID('GenericSearch', 'P') IS NOT NULL
    DROP PROCEDURE GenericSearch;
GO

CREATE PROCEDURE GenericSearch(
  /*  @userName nVarChar(50),
	@password nVarChar(50),
	@stateCodeIn int,
    @searchType int,
	@timeIncrement int,
    @rangeStart Date,
    @rangeEnd Date*/
	@searchType int
	)
AS
	Declare @userName nVarChar(50) = N'Demo'
	Declare @password nVarChar(50) = N'Password'
	Declare @stateCodeIn int = 101
    
	Declare @timeIncrement int = 2
    Declare @rangeStart Date = DATEADD(YEAR, 1953 - 1900, DATEADD(MM,  01, -1));
    Declare @rangeEnd Date  = DATEADD(YEAR, 1960 - 1900, DATEADD(MM,  04, -1))
	
	
DECLARE	@return_value int,
		@userTable nVarChar(50)


EXEC	@return_value = getDBName
		@userName = @userName,
		@password = @password,
		@userTable = @userTable OUTPUT

IF @searchType = 1
BEGIN
	EXEC SearchQuarterlyPCP
	    @stateCodeIn = @stateCodeIn,
	    @dbName = @userTable,
	    @TheStart = @rangeStart,
	    @TheEnd = @rangeEnd
		
END
ELSE
BEGIN
	IF @searchType = 2
	BEGIN
		EXEC SearchQuarterlyCDD_HDD
			@stateCodeIn = @stateCodeIn,
			@dbName = @userTable,
			@TheStart = @rangeStart,
			@TheEnd = @rangeEnd
			
	END
	ELSE
	BEGIN
		EXEC SearchQuarterlyTemperature
			@stateCodeIn = @stateCodeIn,
			@dbName = @userTable,
			@TheStart = @rangeStart,
			@TheEnd = @rangeEnd
			
	END
END
GO
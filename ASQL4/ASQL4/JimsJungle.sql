Use ASQLGroup;
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


/*
CREATE PROCEDURE getDateExtremes
	@tableName nVarChar(50)
	AS

	SELECT 
	*/
USE ASQLGroup
GO


--DECLARE @startDate date = '1951-1-1';

/*DECLARE @sqlQuery VARCHAR(max) = 'SELECT * FROM ' + @tableName 'WHERE  + ';';
	EXEC(@sqlQuery)
	GO 


	CONVERT(DATE, [month]+'/01/'+[year],101)
Select * From dbo.Demo_2_12_2014
	Where 
	*/
/*
DECLARE @tableName nVarchar(50) = 'dbo.Demo_2_12_2014';
DECLARE @firstDate nVarChar(50) = '1951-1-1';
DECLARE @secondDate nVarChar(50) = '1952-1-2';
DECLARE @sqlQuery VARCHAR(max) = 'Select * FROM ' + @tableName +
'WHERE CONVERT(DATE, CAST(data_month AS varchar(2)) + ''/01/'' + CAST(data_year AS varchar(4)) , 101) >= ''' + @firstDate + ''';'; 
--'AND CONVERT(DATE, CAST(data_month AS varchar(2)) + ''/01/'' + CAST(data_year AS varchar(4)) , 101) <= ''' +  @secondDate + ''':';
EXEC (@sqlQuery);
GO
*/
DECLARE @tableName nVarchar(50) = 'dbo.Demo_2_12_2014';
DECLARE @firstDate nVarChar(50) = '1951-1-1';
DECLARE @secondDate nVarChar(50) = '1952-1-2';
DECLARE @columns nVarChar(50) = 'HDD, PCP'

DECLARE @sqlQuery VARCHAR(max) = 'Select ' + @columns + ' FROM ' +  @tableName + 
' WHERE CONVERT(DATE, CAST(data_month AS varchar(2)) + ''/01/'' + CAST(data_year AS varchar(4)) , 101) >= ''' + @firstDate + '''
AND CONVERT(DATE, CAST(data_month AS varchar(2)) + ''/01/'' + CAST(data_year AS varchar(4)) , 101) <= ''' +  @secondDate + '''';
EXEC (@sqlQuery);
GO
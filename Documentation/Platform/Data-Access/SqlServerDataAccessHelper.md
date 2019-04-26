## SqlServerDataAccessHelper
To use the SqlServerDataAccessHelper you need to pass in a ILoggingService and IConfiguration

## Usage
Each call to the RunStoredProcedure method takes a new DatabaseConnectionInfo, this is because the connection objects are not cached by this component. 

If the same connection information is used between calls, the connection caching will be peformed by the .NET Framework

## Generating Cached Data.
There is a configuration option for this component to generate a cached DataSet that can be used in a Unit Test. 
The configuration option is **SQLHELPER_CACHEDATABASEOUTPUTS** which is a boolean option. If it is true, there is a second configuration option which is **SQLHELPER_TEMPFILEPATH**. This is the absolute path to the folder that the cached dataset files will be saved to. If this isn't provided then it will default to the folder that the HMS.Platform.DataAccess.SqlServer is being loaded from.
The default extension for the cached DataSets is _.datasets_

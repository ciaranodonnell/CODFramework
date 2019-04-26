## CachedDataSetDataAccessHelper
To unit test you application and its access to the database there is a specific implementation of the DataAccessHelper, the CachedDataSetDataAccessHelper.
This version only returns a DataReader over a DataSet object. These can be loaded from xml files or created in code.

The SqlDataAccessHelper is able to generate the XML files if needed by saving the output from Stored Procedure calls.


## Usage

To populat the CachedDataSetDataAccessHelper you can get it to load a director of \*.datasets files by calling the _LoadFilesToDataSets(string directory)_ function.
This scans a directory for files and deserializes them into the helper.

You can also use the DataSets property:
``` csharp
Dictionary<string, DataSet> DataSets 
```
to add DataSets created somewhere else in code. 


When the  CachedDataSetDataAccessHelper is asked to run a stored procedure it raises the event RunStoredProcedureCalled. 

```csharp
public event Action<object, RunStoredProcedureCalledEventArgs> RunStoredProcedureCalled;

public class RunStoredProcedureCalledEventArgs : EventArgs
{

    public IDatabaseConnectionInfo ConnectionInfo { get; internal set; }

    public string SpName { get; internal set; }

    public Tuple<string, object>[] Parameters { get; internal set; }

    /// <summary>
    /// Setting this string to a non-null value allows the test to (based on the parameters) choose the cached dataset explicitly by name
    /// </summary>
    public string OverrideSPNameToDifferentCacheLookupValue { get; set; }
}
```

This event lets the unit test know that the function was called. It also allows the Unit Test to chose which cached
DataSet should be returned by setting the _OverrideSPNameToDifferentCacheLookupValue_ property on the event args.
This will be the key used when the helper looks up the DataSet in the cache.

# HMS.Platform.Logging.NLog
This library is an implementation of the Logging platform library using NLog as the underlying framework.

## Example Creation

``` csharp
private static ILoggingService GetLoggingService()
{
    string path =
        Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "nlog.config");
    var logConfig = new XmlLoggingConfiguration(path);
    return new HMSNLogLoggingService(logConfig);
}

```

The above code is an example of how to create an instance of this component. 
This code should be in the Program.cs of a service host.

The code gets the path of the _nlog.config_ file in the same folder as the executable being run.
It calls the NLog configuration framework to parse that file into a configuration object. 
Then uses that to create an HMSNLogLoggingService object, which implements the ILoggingService interface.


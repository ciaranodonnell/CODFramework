# Platform Logging Library
The Logging Library is designed to wrap the logging needs for the services
to prevent tying any service directly to a logging framework. 
The API is simple and very similar to basic logging frameworks such as NLog and Log4Net.

With a distributed system such as the Marco Polo middleware the importance
of good logging and log collection can't be stressed enough.

The purpose of this framework is to make it simple to use NLog (or similar)
in development, but either migrate to an OpenTracing style framework 
in the future or stick with a basic framework but with more complicated
logging targets/appenders.

---

## ILoggingService
The ILoggingService is the primary entry point for the HMS.Platform.Logging library.
This is accepted through Dependency Injection into all services.
Use this interface to get an ILog instance for each class. Each class should ask
for its own logger as the underlying framework may support rules which 
direct log outputs from different classes to different destinations

## ILog Interface

The ILog Interface is the interface that allows code to send out log statements
It allows logging at different levels, namely:
- **Trace**: This is for very detailed and verbose output so you know exactly what the service is doing. Its not unusual for Trace statements to be written every couple of lines. This is not enabled by default in production
- **Debug**: This is for debugging information. Its less verbose that Trace and typically is for the output of decision logic, or dumping runtime values, e.g configuration. This is typically not enabled by default in production
- **Info**: This is for information relevant to understand whats happening in a production system. This is typically enabled in all environments.
- **Warn**: This is for things that are important to know. Unexpected behavior that could indicated an error condition.
- **Error**: This is for logging things that have gone wrong but are recoverable. 
- **Fatal**: This is for logging the reason for service failure. After a fatal error, the service will almost certainly die. This is critical information.

## Log Statement Overloads
There are a number of overloads that can be used when logging. They are 
primarily to reduce the burden of logging to levels that arent enabled.
They are all similar to a String.Format() call, merging the arguments 
into the string. 
The exception to this is the one which takes a Func\<string\>. This one 
is for exceptionally expensive log statements were even getting the parameters for the other
overloads would be expensive. The Func is only called if the log level is
enabled.
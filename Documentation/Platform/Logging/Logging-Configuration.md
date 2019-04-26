# Logging Configuration
There are not standard configuration entries for logging. 
This is for 2 reasons:
1. Logging Configurations are often very complicated, nested structures provided in xml or json files.
2. All other Platform libraries, including Configuration take the logging library through dependency injection, 
therefore it cant be used to instiate a logging framework.

Each service host will have to configure their own Logging. 

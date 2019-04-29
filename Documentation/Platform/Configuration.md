# COD Configuration Library
The Configuration library is kind of another attempt to simplify configuration for people getting started writing cloud applications. THe idea is that it provides two things:

## 1. A simple interface 
I am trying to provide a a simple interface to get access to configuration options. There are a number of other configuration libraries 


## 2. A way for configuration to be overriden 
I feel like there is normally a requirement for configuration to be overriden by more specific configuration. 
We provide configuration in an app.config or appSettings.js file while is part of the application package. This is like a base layer of configuration. 
However in the environment we deploy to we might have more specific configuration for that environment, perhaps domain name or database server ip address. 
Then for specific 
a by more specific configuration, e.g. Read a Config file, Overwrite that with Environment Variables, then overwrite that with command line arguments. 



---

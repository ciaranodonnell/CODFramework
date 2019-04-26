# Implementing a Logging Service
Implementing a logging service requires that you continue to support the same 
logging levels. 

Its also important that you implement the _Is\{Level\}Enabled_ properties. 
These are used to prevent unnecessary work to be done generating log 
messages that arent configured to be written. 


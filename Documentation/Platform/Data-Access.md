# HMS.Platform.DataAccess
The data access library is designed to make it simple to run stored procedures against a SQL database in a running service, while also making it easier to Mock that when writing unit tests.
The library is incredibly simply to use but is opinionated - it forces you to use Stored Procedures to load data. 


## ISqlRunner
The basic idea is that you issue a command against a database, which is the name of a Stored Procedure. 
This then returns an IDataReader that lets you iterate through each Result Set, accesing the data in order to populate your model objects.

## Using the Library

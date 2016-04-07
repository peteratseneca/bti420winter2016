### Week 12 code examples

Use the Visual Studio "Task List", and look for the "Attention" comment tokens.  

**LoadDataFromCSV**

Uses the CsvHelper tool (by Josh Close) to load data from a CSV file that's in the project's file system.  

Features:
- Matching CSV file content and "...Add" view model class

**LoadDataFromXLSX**

Uses the ExcelDataReader tool (by "Ian1971") to load data from an XLSX file that's in the project's file system.  

Features:
- Matching XLSX file content and "...Add" view model class

**WebServiceExample**

Delivers some data from the Chinook sample database.  
Load and run this BEFORE using the "LoadFromWebService" code example below.  

Features:
- Simple web service
- Supports read-only delivery of data, and does not support adds or changes
- Listens for requests for artist and employee data

**LoadDataFromWebService**

This web app calls into a web service, which delivers data on artists and employees.  
BEFORE loading and running this web app, make sure that the "WebServiceExample" app is running.  

Features:
- Implements simple get-all and get-one use cases for artist and employee
- Manager class has a "factory" to create web service request objects

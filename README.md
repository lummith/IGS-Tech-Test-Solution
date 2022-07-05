# IGS Tech Test Solution
## How to run

The 'Run' folder contains the ```QueryApp``` and ```RecipeAPI``` contains all files needed to run the application.  

1. Start the API by typing ```docker-compose -f docker-compose.yml up```  
2. To run the QueryApp type in a comand line ```start QueryApp.exe "InputFile.txt"```

Alternatively, to start the QueryApp open the IgsSoftwareTechTest.sln in visual studio 2022 and start the QueryApp project.  

## Thought process
I implemented a console application for my solution to provide a simple display of instruction. I structured the application around the single responsibility principle. The only dependency that the classes have is the DataObject class - this was to prevent code duplication.  The ```recipe``` in the ``RecipeAPI`` project can be removed and a reference to DataObjects used instead.

### Features

#### Storage
I used a text file to store the API's URL which allows it to be changed easily. In the case that the URL file is missing or empty, I have added a resource file to store the default URL for the API. 

#### Error Handling 
When an exception occurs, the app outputs an exception message to the console and pressing enter will exit the application. This provides a controlled exit from the application and shows information about the exception.   

## Assumptions
* For the watering phases, any consisting of a zero quantity of water are skipped to avoid unnecessary instructions being given to the tower. 
* The outputted times are in a more human-readable format but are stored in the UTC format - I left it like this to make it more human-readable.  

### Corrections 
While I was testing my solution, I noticed two typos with the given material provided:
* In the example input the start date for tray 2 is incorrectly formatted - months and days.  
* In the API there are two "phase 3" watering phases for strawberries.

## Improvments
* Rather than using text files to store the application’s configuration, (currently the API's URL), a database could be used to prevent tampering or potential loss of files. 
* To improve the debugging of the application, tracing and unit tests can be 
added. Unit tests are essential to meet the unit's intended function, especially in an ever-evolving environment.  
* The current error handling can be improved to provide more meaningful output to the console.
* In a test environment it is acceptable to have no authentication but in a production environment, this would be required to prevent any unauthorised access to the API.  
* To prevent repeated calls to the API whenever a new input is given, the recipe could be stored locally. A new endpoint could be added to indicate if the recipes have been updated which could be pooled daily/weekly/monthly.       

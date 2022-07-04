# IGS Tech Test Solution
## How to run

The 'Run' folder contains the ```QueryApp``` and ```RecipeAPI``` contains all files needed to run the application.  

1. Start the API by typing ```docker-compose -f docker-compose.yml up```  
2. To run the QueryApp type in a comand line ```start QueryApp.exe "InputFile.txt"```

Alternatively, to start the QueryApp open the IgsSoftwareTechTest.sln in visual studio 2022 and start the QueryApp project.  

## Thought process

I implemented a console application for my solution to give a simple display of what instructions would be given to structured. I structuced the application around the single responsibility principle. The only dependency the classes have is the DataObject class - I did this to prevent duplicate code. ```The recipe``` class in the ```RecipeAPI``` project can be removed and a reference to DataObjects can be used instead. 

### Features

#### Storage
I used a text file to store the API's URL allowing for it to be changed easily. In the case the URL file is missing or empty I have added a resource file to store the default URL for the API.

#### Error Handling 
When an exception occurs the app outputs the exception message in the console and pressing enter afterwards will exit the application. I did this to give a controlled exit out of the application and to show which issue had occurred.  

## Assumptions
* For watering phases I assumed phases containing 0 amount of water can be skipped to avoid unnecessary instructions given to the tower.  
* The outputted times are not in UTC format but are stored in this format. I left it like this to make it more human-readable.  

### Corrections 
While I was testing my solution I noticed two typos with the given material provided:
* In the example input the start date for tray 2 is incorrectly formated - months and days  
* In the API there are two "phase 3" watering phases for strawberries

## Improvments
* Rather than using text files to store the applications configuration (currently the API's URL) a database could be used instead to prevent tampering or potential loss of files 
* To improve debugging of the application tracing and unit tests can be added. Unit tests are essential meet the unit's intended function especially in an ever-evolving environment. 
* The current error handling can be improved to give more meaningful output to the console.
* In a test environment it is ok to have no authentication but in a production environment, this would be required to prevent any unauthorised access to the API.    
* To prevent repeated calls to the API whenever a new input is given the recipe can be stored locally. A new endpoint could be added to indicate if the recipes have been updated which can be pooled daily/weekly/monthly.        

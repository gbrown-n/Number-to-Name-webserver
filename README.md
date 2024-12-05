TechnologyOne Tech Test 

A program utilising a HTTP Webserver to translate a two-decimal double value to its string name form, and communicate this value
to the user in dollars and cents. 

Created by Gabriel Brown - 5/12/24

Usage: 

The main project, TechTest.csproj, can be built either by: 
    - Utilising the C# Dev Kit extension in VSCode, which includes build and debugging usage
    - Using a terminal, navigating to the directory containing the TechTest.csproj file (TechTest\TechTest) and using the dotnet build command

Running the main project can be done in a similar method, using the dotnet run command. 
If the program is executed successfully, a console message will be displayed indicating the webclient is active and listening for connections.
The default port used for these connections is 8000

The test harness, TestProject1.csproj, can be built and run in a similar method by: 
    - Utilising the C# Dev Kit extension in VSCode, which includes build and debugging usage
    - Using a terminal, navigating to the directory containing the TestProject1.csproj file (TechTest\TechTest\TestProject1) and using the dotnet run command

Known Issues: 
- The method NumToName uses an inefficient process of establishing the degree of the number (hundred, thousand, million, billion). This should be re-written to iterate through these denominations. 
- While the test harness is implemented, the unit tests created are far from conclusive, and many more tests could be made to consider a larger range of edge cases and invalid user input. 
- The HTML web layout is rudimentary and uses a large amount of inline css styling. A more responsive and professional webpage could be designed with a dedicated CSS document, and could be parsed through the HTTP webserver utilising a better webclient library. 
# NAGP-UnitTesting-eBroker

GitHub Link - https://github.com/Aayush-gupta10/NAGP-UnitTesting-eBroker

Clone the Git repository using command - git clone https://github.com/Aayush-gupta10/NAGP-UnitTesting-eBroker.git
Run the following commands to generate the test report:
 1. dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=.\TestResults\Coverage\
 2. reportgenerator -reports:".\**\TestResults\coverage\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html

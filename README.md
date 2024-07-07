<h1 align="center">API for NBP currencies</h1>

Link for swagger: https://currencyapi20240707133103.azurewebsites.net/index.html

## Technologies
CurrencyAPI is created with:
* .NET 8
* Entity Framework 
* SQL Server

## Setup
To run this app, install repository.

You need Visual Studio with .NET 8.

In appsetting.json change DefaultConnection:

* Server = your connection from SQL Server.

After these change choose Package Manager Console and run the command below:

$ update-database

After these steps, the application is ready to run. 

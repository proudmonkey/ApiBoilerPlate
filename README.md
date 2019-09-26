<img align="right" src="logo.PNG" />

# ApiBoilerPlate
A project template for building ASP.NET Core APIs using .NET Core 3.x (the latest/fastest version of .NET Core to date). The goal is to help you get started setting up the base structure of your app and it's dependencies so you can focus on implementing business specific requirements without you having to copy and paste the base structure of the project, and installing its dependencies all over again. This will speedup the development time when building new API project while enforcing standards for all your apps.

## Tools and Frameworks Used

* [.NET Core 3.0](https://dotnet.microsoft.com/download/dotnet-core)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.0) - For build RESTful APIs
* [Dapper](https://dapper-tutorial.net/dapper) - For data access.
* [AutoMapper](https://github.com/AutoMapper/AutoMapper) - For mapping entity models to DTOs.
* [AutoWrapper](https://github.com/proudmonkey/AutoWrapper) - For handling request exceptions and consistent HTTP response format.
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - For API documentation
* [FluentValidation.AspNetCore](https://fluentvalidation.net/aspnet) - For Model validations
* [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore) - For logging capabilties

## Install the extension from the Visual Studio Marketplace

1. Fire up Visual Studio 2019, on the `Extensions` menu, click `Manage Extensions`.
2. Click `Online` and then search for `ApiBoilerPlate`.
3. Click `Download`. The extension is then scheduled for install.

To complete the installation, close all instances of Visual Studio.

## Create a new Project from ApiBoilerPlate Extension

1. Open Visual Studio 2019 and then select `Create New Project` box
2. In the search bar, type `ApiBoilerPlate`.
3. Click the `ApiBoilerPlate` item and then click `Next`.
4. Name your project to whatever you like and then click `Create`.
5. Visual Studio should generate the files for you.

## Steps to run the template

**STEP 1:** Create a Test local Database:

1. Open Visual Studio 2019
2. Go to `View` > `SQL Server Object Explorer`
3. Drilldown to `SQL Server` > `(localdb)`
4. Right-click "`Database`" Folder
5. Click "`Add New Database`"
6. Name it as "`TestDB`" and click OK
7. Under "`TestDB`", Right-click "`Tables`" folder and select "`Add New Table`"

```sql
CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    	[FirstName] NVARCHAR(20) NOT NULL, 
    	[LastName] NVARCHAR(20) NOT NULL, 
    	[DateOfBirth] DATETIME NOT NULL
)
```

**STEP 2:** Update Database ConnectionString

Change the connectionString in `appsettings.json` that is pointing to the new created database. You can get the connectionString values in the properties window of the "`TestDB`" database in Visual Studio.

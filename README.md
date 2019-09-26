# ApiBoilerPlate
A template project for building ASP.NET Core APIs

## Install the extension from the Visual Studio Marketplace

1. Fire up Visual Studio 2019, on the `Extensions` menu, click `Manage Extensions`.
2. Click `Online` and then search for `ApiBoilerPlate`.
3. Click `Download`. The extension is then scheduled for install.

To complete the installation, close all instances of Visual Studio.

## Tools and Frameworks Used

* .NET Core 3.0
* ASP.NET Core
* Dapper
* AutoMapper
* AutoWrapper.Core
* Swashbuckle.AspNetCore
* FluentValidation.AspNetCore
* Serilog.AspNetCore

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

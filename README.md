<img align="right" src="logo.PNG" />

# ApiBoilerPlate.AspNetCore [![Visual Studio Marketplace Installs](https://img.shields.io/visual-studio-marketplace/i/vmsdurano.ApiProjVSExt?color=green)](https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt)

A simple yet organized [project template](https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt) for building ASP.NET Core APIs using .NET Core 3.x (the latest/fastest version of .NET Core to date). The goal is to help you get up to speed when setting up the core structure of your app and its dependencies. This enables you to focus on implementing business specific requirements without you having to copy and paste the core structure of your project, and installing its dependencies all over again. This will speed up your development time when building new API project while enforcing standard project structure with its dependencies and configurations for all your apps.

If you are looking for a project template for ASP.NET Core API that you can reuse across your team, or if you are new to ASP.NET Core and would like to get up to speed on how it works without having you to configure most of the basic features that an API will have, then this is for you.

## Tools and Frameworks Used

* [.NET Core 3.0](https://dotnet.microsoft.com/download/dotnet-core)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.0) - For building RESTful APIs
* [Dapper](https://dapper-tutorial.net/dapper) - For data access.
* [AutoMapper](https://github.com/AutoMapper/AutoMapper) - For mapping entity models to DTOs.
* [AutoWrapper](https://github.com/proudmonkey/AutoWrapper) - For handling request exceptions and consistent HTTP response format.
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - For API documentation
* [FluentValidation.AspNetCore](https://fluentvalidation.net/aspnet) - For Model validations
* [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore) - For logging capabilities

Keep in mind that you can always replace and choose whatever framework you want to use for your `API`. After all, the template is just a skeleton for your project structure with default preconfigured middlewares. For example, you can always replace `Dapper` with `Entity Framework Core`, `PetaPoco`, etc. and configure them yourself. You can also replace `Serilog` with whatever logging frameworks and providers you want that works with `ASP.NET Core` - the choice is yours.

## Install the extension from the Visual Studio Marketplace

1. Fire up Visual Studio 2019, click `Continue without code` link.
2. On the `Extensions` menu, click `Manage Extensions`.
3. Click `Online` and then search for `ApiBoilerPlate`.
4. Click `Download`. The extension is then scheduled for install.

To complete the installation, close all instances of Visual Studio.

Alternatively, you can `download` and `install` the VSIX Extension directly at the following link: https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt

## Create a new Project from ApiBoilerPlate Extension

1. Open Visual Studio 2019 and then select `Create New Project` box
2. The newly installed template should appear at the top. You can also type "`ApiBoilerPlate`" in the search bar.
3. Click the `ApiBoilerPlate` item and then click `Next`.
4. Name your project to whatever you like and then click `Create`.
5. Visual Studio should generate the files for you.

## Steps to run the template

**STEP 1:** Create a Test local Database:

1. Open Visual Studio 2019
2. Go to `View` > `SQL Server Object Explorer`
3. Drilldown to `SQL Server` > `(localdb)\MSSQLLocalDB`
4. Right-click "`Database`" Folder
5. Click "`Add New Database`"
6. Name it as "`TestDB`" and click OK
7. Right-click on the "`TestDB`" database and then select "`New Query`"
8. Run the script below to generate the "`Person`" table.

```sql
CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    	[FirstName] NVARCHAR(20) NOT NULL, 
    	[LastName] NVARCHAR(20) NOT NULL, 
    	[DateOfBirth] DATETIME NOT NULL
)
```

**STEP 2:** Update Database ConnectionString (Optional)

If you follow step 1, then you can skip this step and run the application right away.

If you have a different `database` and `table` name then you need to change the `connectionString` in `appsettings.json` that is pointing to the newly created database. You can get the `connectionString` values in the `properties` window of the "TestDB" database in Visual Studio.

## Walkthrough

[ApiBoilerPlate: A Project Template for Building ASP.NET Core APIs](http://vmsdurano.com/apiboilerplate-a-project-template-for-building-asp-net-core-apis/)

## Contributor

* **Vincent Maverick Durano** - [Blog](http://vmsdurano.com/)


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details

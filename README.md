<img align="right" src="logo.PNG" />

# ApiBoilerPlate.AspNetCore 
![Visual Studio Marketplace Version](https://img.shields.io/visual-studio-marketplace/v/vmsdurano.ApiProjVSExt?color=green&label=vsix)
[![Visual Studio Marketplace Installs](https://img.shields.io/visual-studio-marketplace/i/vmsdurano.ApiProjVSExt?color=green)](https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt) 
[![Nuget](https://img.shields.io/nuget/v/ApiBoilerPlate.AspNetCore?color=blue)](https://www.nuget.org/packages/ApiBoilerPlate.AspNetCore)
[![Nuget](https://img.shields.io/nuget/dt/ApiBoilerPlate.AspNetCore?color=blue)](https://www.nuget.org/packages/ApiBoilerPlate.AspNetCore)

A simple yet organized [project template](https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt) for building ASP.NET Core APIs using .NET Core 3.1 (the latest/fastest version of .NET Core to date) with preconfigured tools and frameworks. It features most of the functionalities that an API will have such as database CRUD operations, Token-based Authorization, Http Response format consistency, Global exception handling, Logging, Http Request rate limiting, HealthChecks and many more. The goal is to help you get up to speed when setting up the core structure of your app and its dependencies when spinning up a new ASP.NET Core API project. This enables you to focus on implementing business specific code requirements without you having to copy and paste the core structure of your project, common features, and installing its dependencies all over again. This will speed up your development time while enforcing standard project structure with its dependencies and configurations for all your apps.

If you are looking for a project template for ASP.NET Core API that you can reuse across your team, or if you are new to ASP.NET Core and would like to get up to speed on how it works without having you to configure most of the basic features that an API will have, then this is for you.

# How To Get It?
There are two ways to install the template:
* From Nuget with .NET CLI: [ApiBoilerPlate.AspNetCore](https://www.nuget.org/packages/ApiBoilerPlate.AspNetCore)
* From VSIX Market Place with Visual Studio: [ApiBoilerPlate.AspNetCore](https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt)

# Tools and Frameworks Used

* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core)
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) - For building RESTful APIs
* [Dapper](https://dapper-tutorial.net/dapper) - For data access.
* [AutoMapper](https://github.com/AutoMapper/AutoMapper) - For mapping entity models to DTOs.
* [AutoWrapper](https://github.com/proudmonkey/AutoWrapper) - For handling request `Exceptions` and consistent `HTTP` response format.
* [AutoWrapper.Server](https://github.com/proudmonkey/AutoWrapper.Server) - For unwrapping the `Result` attribute from AutoWrapper's `ApiResponse` output.
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - For securing API documentation.
* [FluentValidation.AspNetCore](https://fluentvalidation.net/aspnet) - For Model validations
* [Serilog.AspNetCore](https://github.com/serilog/serilog-aspnetcore) - For logging capabilities
* [IdentityServer4.AccessTokenValidation](https://github.com/IdentityServer/IdentityServer4.AccessTokenValidation) - For `JWT` Authentication handling
* [Microsoft.Extensions.Http.Polly](https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory) - For handling `HttpClient` Resilience and Transient fault-handling
* [AspNetCoreRateLimit](https://github.com/stefanprodan/AspNetCoreRateLimit) - For controlling the rate of requests that clients can make to an external `API` based on IP address or client ID.
* [AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) - For performing health checks
* [Microsoft.AspNetCore.Diagnostics.HealthChecks](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-3.0) - For getting the results of Health Checks in the application
* [AspNetCore.HealthChecks.UI](https://www.nuget.org/packages/AspNetCore.HealthChecks.UI/) - For Health Status visualization
* [xUnit](https://xunit.net/) and [Moq](https://github.com/Moq/moq4/wiki/Quickstart) - For unit testing.


Keep in mind that you can always replace and choose whatever framework you want to use for your `API`. After all, the template is just a skeleton for your project structure with default preconfigured middlewares. For example, you can always replace `Dapper` with `Entity Framework Core`, `PetaPoco`, etc. and configure them yourself. You can also replace `Serilog` with whatever logging frameworks and providers you want that works with `ASP.NET Core` - the choice is yours.

# Key Takeaways
Here's the list of the good stuff that you can get when using the template:

* Configured Sample Code for database CRUD operations.
* Configured Basic Data Access using `Dapper`.
* Configured Logging using `Serilog`.
* Configured `AutoMapper` for mapping entity models to DTOs.
* Configured `FluentValidation` for DTO validations.
* Configured `AutoWrapper` for handling request `Exceptions` and consistent `HTTP` response format.
* Configured `AutoWrapper.Server` for unwrapping the `Result` attribute from AutoWrapper's `ApiResponse` output.
* Configured `Swagger` API Documentation.
* Configured `CORS`.
* Configured `JWT` Authorization and Validation.
* Configured Sample Code for Requesting Client Credentials `Token`.
* Configured Swagger to secure `API` documentation with `Bearer Authorization`.
* Configured Sample Code for connecting Protected External APIs.
* Configured Sample Code for implementing custom `API` Pagination.
* Configured `HttpClient` Resilience and Transient fault-handling.
* Configured `Http` Request Rate Limiter.
* Configured `HealthChecks` and `HealthChecksUI`.
* Configured Unit Test Project with `xUnit`.
* [Deprecated] Configured Sample Code for Worker service. For handling extensive process in the background, you may want to look at the [Worker Template](https://github.com/judedaryl/pubsub-worker-starter) created by [Jude Daryl Clarino](https://judedaryl.github.io/). The template was also based on `ApiBoilerPlate`.

# Install the Template from .NET CLI
1. Install the latest [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).
2. Run `dotnet new -i apiboilerplate.aspnetcore`. This will install the template in your machine.
3. Run `dotnet new apiboilerplate --name "MyAPI" -o samples`. This will generate the project template named `MyAPI` within the `samples` directory.

Once installed, you should see the following console message:

> The template "ASP.NET Core API Template for .NET Core 3.x" was created successfully.  

# Install the Template from Visual Studio Marketplace

**Note**: If you are using the previous version of the template, make sure to uninstall it first before you install the latest version.

1. Fire up Visual Studio 2019, click `Continue without code` link.
2. On the `Extensions` menu, click `Manage Extensions`.
3. Click `Online` and then search for `ApiBoilerPlate`.
4. Click `Download`. The extension is then scheduled for install.

To complete the installation, close all instances of Visual Studio.

Alternatively, you can `download` and `install` the VSIX Extension directly at the following link: https://marketplace.visualstudio.com/items?itemName=vmsdurano.ApiProjVSExt

# Create a new Project from ApiBoilerPlate Extension

1. Open Visual Studio 2019 and then select `Create New Project` box
2. The newly installed template should appear at the top. You can also type "`ApiBoilerPlate`" in the search bar.
3. Click the `ApiBoilerPlate` item and then click `Next`.
4. Name your project to whatever you like and then click `Create`.
5. Visual Studio should generate the files for you.

# Steps to Run the Template

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

# Walkthrough

* [ApiBoilerPlate: A Project Template for Building ASP.NET Core APIs](http://vmsdurano.com/apiboilerplate-a-project-template-for-building-asp-net-core-apis/)
* [ApiBoilerPlate: New Features and Improvements for Building ASP.NET Core 3 APIs](http://vmsdurano.com/apiboilerplate-new-features-and-improvements-for-building-asp-net-core-3-apis/)
* [IdentityServer4: Building a Simple Token Server and Protecting Your ASP.NET Core APIs with JWT](http://vmsdurano.com/apiboilerplate-and-identityserver4-access-control-for-apis/)

# Give a Star! :star:
Feel free to request an issue on github if you find bugs or request a new feature. Your valuable feedback is much appreciated to better improve this project. If you find this useful, please give it a star to show your support for this project.

# Contributors

* **Vincent Maverick Durano** - [Blog](http://vmsdurano.com/)
* **Jude Daryl Clarino** - [Blog](https://judedaryl.github.io/)
* **Bruno Renato Feliciano** - [LinkedIn](https://www.linkedin.com/in/brunorfeliciano/)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.

# Donate
If you find this project useful — or just feeling generous, consider buying me a beer or a coffee. Cheers! :beers: :coffee:
|               |               |
| ------------- |:-------------:|
|   <a href="https://www.paypal.me/vmsdurano"><img src="https://github.com/proudmonkey/Resources/blob/master/donate_paypal.svg" height="40"></a>   | [![BMC](https://github.com/proudmonkey/Resources/blob/master/donate_coffee.png)](https://www.buymeacoffee.com/ImP9gONBW) |


Thank you!

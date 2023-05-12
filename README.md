# JobNexus-API

Job Publish and Seek API in `.NET 7`

| GitHub        | Codecov       |
|:-------------:|:-------------:|
| [![Build & Test](https://github.com/kimaneChen/JobNexus-Backend/actions/workflows/dotnetcore.yml/badge.svg?branch=master)](https://github.com/kimaneChen/JobNexus-Backend/actions/workflows/dotnetcore.yml) | [![codecov](https://codecov.io/gh/kimaneChen/JobNexus-Backend/branch/master/graph/badge.svg?token=1NJECZSLPB)](https://codecov.io/gh/kimaneChen/JobNexus-Backend) |

The Api provides the backend solutions for a Job publish and Job Seek platform.  This API is built based on [.Net Boilerplate](https://github.com/lkurzyniec/netcore-boilerplate).

## Source code contains

1. [Central Package Management (CPM)](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)
2. [Swagger](https://swagger.io/) + [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle)
3. [FeatureManagement](https://github.com/microsoft/FeatureManagement-Dotnet) (Feature Flags, Feature Toggles)
4. [HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)
5. [EF Core](https://docs.microsoft.com/ef/)
    * [MySQL provider from Pomelo Foundation](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
    * [MsSQL from Microsoft](https://github.com/aspnet/EntityFrameworkCore/)
6. Tests
    * Integration tests with InMemory database
        * [FluentAssertions]
        * [xUnit]
        * [Verify](https://github.com/VerifyTests/Verify/)
        * TestServer
    * Unit tests
        * [AutoFixture](https://github.com/AutoFixture/AutoFixture)
        * [FluentAssertions]
        * [Moq](https://github.com/moq/moq4)
        * [Moq.AutoMock](https://github.com/moq/Moq.AutoMocker)
        * [xUnit]
    * Architectural tests (conventional tests)
        * [NetArchTest](https://github.com/BenMorris/NetArchTest)
        * [xUnit]
7. Code quality
    * [EditorConfig](https://editorconfig.org/) ([.editorconfig](.editorconfig))
    * Analizers
        * [Microsoft.CodeAnalysis.Analyzers](https://github.com/dotnet/roslyn-analyzers)
        * [Microsoft.AspNetCore.Mvc.Api.Analyzers](https://github.com/aspnet/AspNetCore/tree/master/src/Analyzers)
        * [Microsoft.VisualStudio.Threading.Analyzers](https://github.com/microsoft/vs-threading)
    * CodeAnalysisRules [HappyCode.JobNexus.ruleset](HappyCode.JobNexus.ruleset)
    * Code coverage
        * [Coverlet](https://github.com/tonerdo/coverlet)
        * [Codecov](https://codecov.io/)
    * CI Code analysis with [CodeQL](https://codeql.github.com/)
8. Docker
    * [Dockerfile](dockerfile)
    * [Docker-compose](docker-compose.yml)
        * `mysql:8` with DB initialization
        * `mcr.microsoft.com/azure-sql-edge` with DB initialization
        * `netcore-boilerplate:local`
9. [Serilog](https://serilog.net/)
    * Sink: [Async](https://github.com/serilog/serilog-sinks-async)
10. [DbUp](http://dbup.github.io/) as a db migration tool
11. Continuous integration
    * [Travis CI](https://travis-ci.org/) ([travisci.yml](./.travis.yml)) Some things not very clear
    * [GitHub Actions](https://github.com/features/actions)
        * [dotnetcore.yml](.github/workflows/dotnetcore.yml)
        * [codeql-analysis.yml](.github/workflows/codeql-analysis.yml)
        * [docker.yml](.github/workflows/docker.yml)

## Architecture

### Api

[HappyCode.JobNexus.Api](src/HappyCode.JobNexus.Api)

* The entry point of the app - [Program.cs](src/HappyCode.JobNexus.Api/Program.cs)
* Simple Startup class - [Startup.cs](src/HappyCode.JobNexus.Api/Startup.cs)
  * MvcCore
  * DbContext (with MySQL)
  * DbContext (with MsSQL)
  * Swagger and SwaggerUI (Swashbuckle)
  * HostedService and HttpClient
  * FeatureManagement
  * HealthChecks
    * MySQL
    * MsSQL
* Filters
  * Simple `ApiKey` Authorization filter - [ApiKeyAuthorizationFilter.cs](src/HappyCode.JobNexus.Api/Infrastructure/Filters/ApiKeyAuthorizationFilter.cs)
  * Action filter to validate `ModelState` - [ValidateModelStateFilter.cs](src/HappyCode.JobNexus.Api/Infrastructure/Filters/ValidateModelStateFilter.cs)
  * Global exception filter - [HttpGlobalExceptionFilter.cs](src/HappyCode.JobNexus.Api/Infrastructure/Filters/HttpGlobalExceptionFilter.cs)
* Configurations
  * `Serilog` configuration place - [SerilogConfigurator.cs](src/HappyCode.JobNexus.Api/Infrastructure/Configurations/SerilogConfigurator.cs)
  * `Swagger` registration place - [SwaggerRegistration.cs](src/HappyCode.JobNexus.Api/Infrastructure/Registrations/SwaggerRegistration.cs)
* Simple exemplary API controllers - [EmployeesController.cs](src/HappyCode.JobNexus.Api/Controllers/EmployeesController.cs), [CarsController.cs](src/HappyCode.JobNexus.Api/Controllers/CarsController.cs)
* Example of BackgroundService - [PingWebsiteBackgroundService.cs](src/HappyCode.JobNexus.Api/BackgroundServices/PingWebsiteBackgroundService.cs)

### Core

[HappyCode.JobNexus.Core](src/HappyCode.JobNexus.Core)

* Models
  * Dto models
  * DB models
  * AppSettings models - [Settings](src/HappyCode.JobNexus.Core/Settings)
* DbContexts
  * MySQL DbContext - [EmployeesContext.cs](src/HappyCode.JobNexus.Core/EmployeesContext.cs)
  * MsSQL DbContext - [CarsContext.cs](src/HappyCode.JobNexus.Core/CarsContext.cs)
* Core registrations - [CoreRegistrations.cs](src/HappyCode.JobNexus.Core/Registrations/CoreRegistrations.cs)
* Exemplary MySQL repository - [EmployeeRepository.cs](src/HappyCode.JobNexus.Core/Repositories/EmployeeRepository.cs)
* Exemplary MsSQL service - [CarService.cs](src/HappyCode.JobNexus.Core/Services/CarService.cs)

## DB Migrations

[HappyCode.JobNexus.Db](src/HappyCode.JobNexus.Db)

* Console application as a simple db migration tool - [Program.cs](src/HappyCode.JobNexus.Db/Program.cs)
* Sample migration scripts, both `.sql` and `.cs` - [S001_AddCarTypesTable.sql](src/HappyCode.JobNexus.Db/Scripts/Sql/S001_AddCarTypesTable.sql), [S002_ModifySomeRows.cs](src/HappyCode.JobNexus.Db/Scripts/Code/S002_ModifySomeRows.cs)

## Tests

### Integration tests

[HappyCode.JobNexus.Api.IntegrationTests](test/HappyCode.JobNexus.Api.IntegrationTests)

* Infrastructure
  * Fixture with TestServer - [TestServerClientFixture.cs](test/HappyCode.JobNexus.Api.IntegrationTests/Infrastructure/TestServerClientFixture.cs)
  * TestStartup with InMemory databases - [TestStartup.cs](test/HappyCode.JobNexus.Api.IntegrationTests/Infrastructure/TestStartup.cs)
  * Simple data feeders - [EmployeeContextDataFeeder.cs](test/HappyCode.JobNexus.Api.IntegrationTests/Infrastructure/DataFeeders/EmployeesContextDataFeeder.cs), [CarsContextDataFeeder.cs](test/HappyCode.JobNexus.Api.IntegrationTests/Infrastructure/DataFeeders/CarsContextDataFeeder.cs)
* Exemplary tests - [EmployeesTests.cs](test/HappyCode.JobNexus.Api.IntegrationTests/EmployeesTests.cs), [CarsTests.cs](test/HappyCode.JobNexus.Api.IntegrationTests/CarsTests.cs)

### Unit tests

[HappyCode.JobNexus.Api.UnitTests](test/HappyCode.JobNexus.Api.UnitTests)

* Exemplary tests - [EmployeesControllerTests.cs](test/HappyCode.JobNexus.Api.UnitTests/Controllers/EmployeesControllerTests.cs)
* Unit tests of `ApiKeyAuthorizationFilter.cs` - [ApiKeyAuthorizationFilterTests.cs](test/HappyCode.JobNexus.Api.UnitTests/Infrastructure/Filters/ApiKeyAuthorizationFilterTests.cs)

[HappyCode.JobNexus.Core.UnitTests](test/HappyCode.JobNexus.Core.UnitTests)

* Extension methods to mock `DbSet` faster - [EnumerableExtensions.cs](test/HappyCode.JobNexus.Core.UnitTests/Extensions/EnumerableExtensions.cs)
* Exemplary tests - [EmployeeRepositoryTests.cs](test/HappyCode.JobNexus.Core.UnitTests/Repositories/EmployeeRepositoryTests.cs), [CarServiceTests.cs](test/HappyCode.JobNexus.Core.UnitTests/Services/CarServiceTests.cs)

### Architectural tests

[HappyCode.JobNexus.ArchitecturalTests](test/HappyCode.JobNexus.ArchitecturalTests)

* Exemplary tests - [ApiArchitecturalTests.cs](test/HappyCode.JobNexus.ArchitecturalTests/ApiArchitecturalTests.cs), [CoreArchitecturalTests.cs](test/HappyCode.JobNexus.ArchitecturalTests/CoreArchitecturalTests.cs)

## How to adapt to your project

Generally it is totally up to you! But in case you do not have any plan, You can follow below simple steps:

1. Download/clone/fork repository :arrow_heading_down:
1. Remove components and/or classes that you do not need to :fire:
1. Rename files (e.g. sln, csproj, ruleset), folders, namespaces etc :memo:
1. Appreciate the work :star:

## Build the solution

Just execute `dotnet build` in the root directory, it takes `HappyCode.JobNexus.sln` and build everything.

## Start the application

### Standalone

At first, you need to have up and running [MySQL](https://www.mysql.com/downloads/) and [MsSQL](https://www.microsoft.com/sql-server/sql-server-downloads) database servers on localhost with initialized
database by [mysql script](db/mysql/mysql-employees.sql) and [mssql script](db/mssql/mssql-cars.sql).

Then the application (API) can be started by `dotnet run` command executed in the `src/HappyCode.JobNexus.Api` directory.
By default it will be available under http://localhost:5000/, but keep in mind that documentation is available under
http://localhost:5000/swagger/.

### Docker (recommended)

Just run `docker-compose up` command in the root directory and after successful start of services visit http://localhost:5000/swagger/.
To check that API has connection to both MySQL and MsSQL databases visit http://localhost:5000/health/.

> When running on `Linux` (i.e. [WSL](https://learn.microsoft.com/en-us/windows/wsl/install)), make sure that all docker files
([dockerfile](dockerfile), [docker-compose](docker-compose.yml) and all [mssql files](db/mssql)) have line endings `LF`.

### Migrations

When the entire environment is up and running, you can additionally run a migration tool to add some new schema objects into MsSQL DB. To do that, go to `src/HappyCode.JobNexus.Db` directory and execute `dotnet run` command.

## Run unit tests

Run `dotnet test` command in the root directory, it will look for test projects in `HappyCode.JobNexus.sln` and run them.

## To Do

* any idea? Please create an issue.

# SAMPLE CRUD with customize API auth
Sample of CRUD with My Sql database, with EF Core and Dockerize (Optional).

## ASP NET.CORE
![App Screenshot](https://blog.rashik.com.np/wp-content/uploads/2020/06/efcore.jpg)

## Installation
To use this .NET CORE make sure you have :
- [DOTNET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Internet Information System 10](https://www.iis.net/downloads)
- [or Docker to replace IIS 10](https://www.docker.com/)

## Usage/Examples
- Make sure to install iis 10 or docker first.
- Add website and pointing to
```
localhost:5003/api
```
For better application run at [Microsoft Edge](https://www.microsoft.com/en-us/edge/download)

## Database
To run database migration
```bash
  dotnet ef migrations add InitialCreate
```
After creating initial database run this
```bash
  dotnet ef database update
```
Wait until all process done by ef core & dotnet.

## Features
- Using EF Core,
- Using Database table migration,
- Using JWT bearer token for authentication,
- Using Bogus for seeding data,
- Using Serilog to tracking error while using application.

## Lisence
.Net Core is a free and open-source, managed computer software framework for Windows, Linux, and macOS operating systems under [Microsoft](https://www.microsoft.com/en-us/licensing/default) & [MIT license](https://opensource.org/licenses/MIT).

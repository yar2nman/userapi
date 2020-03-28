
## Create the application
CLI
dotnet new webapi -au Windows --force

## Scaffold the databas
CLI
dotnet ef dbcontext scaffold "Server=server, port;Database=databasename;username=username;password=password;" Microsoft.EntityFrameworkCore.SqlServer -o Models

## Generate controller
dotnet aspnet-codegenerator controller -name RolesController -api -async -m usersapi.Models.AspNetRoles -dc UserApiContext -namespace usersapi.Controllers -outDir Controllers

## Setting the connection string in secrete file
cli
dotnet user-secrets init 
dotnet user-secrets set ConnStr "Put the full connection string here"

## Helper Class
To store to conn str after retreving it from the secrete store

## Password Hasher from
https://github.com/ruidfigueiredo/IdentityV3PasswordHasher/blob/master/PasswordHasher.cs
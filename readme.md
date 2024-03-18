# Introduction

This project is a simple todo web application applying some technologies below:
- [Angular 17](https://blog.angular.io/introducing-angular-v17-4d7033312e4b?gi=1de0b9bac012)
- [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview)
- [Terraform](https://www.terraform.io)
- [Auth0](https://auth0.com) for authentication
- [Azure pipelines](https://azure.microsoft.com/en-us/products/devops/pipelines) support CI/CD
- Azure resources
    - [Azure App Service](https://azure.microsoft.com/en-us/products/app-service)
    - [Azure SQL Database](https://azure.microsoft.com/en-us/products/azure-sql/database)
    - [Azure Key Vault](https://azure.microsoft.com/en-us/products/key-vault)


## Infrastructure

## CI/CD flows


# Setup infrastructure

Base on your requirements and project requirements, please follow this [link](https://developer.hashicorp.com/terraform/tutorials/azure-get-started) to setup and manage your infrastructure state.


# Todo Project - ASP.NET Core 8 - Angular 17

## 1. Project Structure

The project structure is designed to promote separation of concerns and modularity, making it easier to understand, test, and maintain the application.

```
├── Dekra.Todo.Api
│   ├── Business                # Contains business logic
│   ├── Infrastructure          # Contains configurations, middlewares, etc.
│   ├── API                     # Contains the api, including controllers, extensions, authentication, etc.
│   └── Controllers             # Contains REST API endpoints
├── Dekra.Todo.App
│   ├── helpers                 # Contains guards, interceptors ....
│   ├── components              # Contains application components, modules
│   ├── models                  # Contains data models
│   ├── store                   # Contains logic to manage app state, using rxjs/state & rxjs/effects
│   └── services                # Contains services
```
## 2. Database Migration
Note: Before apply change to your database, please update the connection string in `appsettings.json`

Create new migration:
```powershell
dotnet ef migrations add v.0.0.1 -o Data/Migrations
```

Remove latest migration:
```powershell
ef migrations remove
```

Apply migration to database:
```powershell
dotnet ef database update -- --environment local
```

## 3. Build Local

To run this project, follow some steps below:

1. Ensure the .NET 8 SDK and Node 16 are installed
4. With Dekra.Todo.Api, update project variables in `appsettings.json`
2. Open your terminal and move to src
3. Build the solution to restore NuGet packages and compile the code.
4. Configure the necessary database connection settings in the `appsettings.json` file of the API project.
5. Run .Net Api as start up project
6. restore npm packages in angular UI
7. Run Angular by command


## 4. FrontEnd
After done with backend, start angular project by below scripts
```shell
npm install

ng serve
```

## 5. KeyVaults
If use want to use azure key vaults add key vault configuration inside appsetting.json and add access policy for the application

![KeyVaults](./pictures/key_vault_access_policies.png?raw=true "KeyVaults")


## 6. Output
And here's how it looks like

Login Screen try user01/user01 or user02/user02

![LoginScreen](./pictures/Login_Screen.png?raw=true "LoginScreen")

Todo Screen
![TodoScreen](./pictures/Todo_Screen.png?raw=true "TodoScreen")

## 7. UnitTest
Currently, this project only write unit test for Backend

![UnitTest](./pictures//UnitTest.png?raw=true "UnitTest")
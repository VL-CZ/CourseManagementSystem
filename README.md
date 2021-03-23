## Prerequisites
- .NET Core 3.1 SDK
- Microsoft SQL Server

## Run
```shell
dotnet run --project CourseManagementSystem.API
```

## Add migration
```shell
dotnet ef migrations add {migration_name} --project CourseManagementSystem.Data --startup-project CourseManagementSystem.API
dotnet ef database update --project CourseManagementSystem.Data --startup-project CourseManagementSystem.API
```
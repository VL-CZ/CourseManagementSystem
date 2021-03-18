## Add migration
```shell
dotnet ef migrations add {migration_name} --project CourseManagementSystem.Data --startup-project CourseManagementSystem.API
dotnet ef database update --project CourseManagementSystem.Data --startup-project CourseManagementSystem.API
```
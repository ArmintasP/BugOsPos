# BugOsPos

## How to run

- Install .NET 7 SDK.
- Run the following command in solution folder: `dotnet run  --project .\BugOsPos.Api\BugOsPos.Api.csproj`
- In console logs there will be a mention of port application is listening to, for example `http://localhost:5095`.
- Add suffix `/swagger/index.html` to it, i.e. `http://localhost:5095/swagger/index.html`.
- Do not forget to use authorization tokens to access majority of endpoints (it can be done by pasting jwt token in `Authorize` section).

Some default values:
```
POST /customers/login
{
  "franchiseId": 11,
  "username": "sarah",
  "password": "Test12345"
}
```
```
POST /employees/login
{
  "franchiseId": 1,
  "employeeCode": "11",
  "password": "Test12345"
}
```

[Some differences between implementation and Lab1 & Lab2 documentation can be found here.](diff.txt)


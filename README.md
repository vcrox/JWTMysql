# Summary
ASP NET core 5 with EF Core 5 + Mysql + Swagger

## Install dotnet ef tool
```bash
dornet restore
dotnet tool install --global dotnet-ef
```

## Edit your connection string
```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;user=root;password=root;database=netcoreauthjwtmysql"
  },
  "AppSettings": {
    "Token": "c0e525b3-af69-493a-a381-991e0668746a"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## update database
```bash
cd JWTMysql
dotnet ef migrations add InitialCreate
dotnet ef database update
```


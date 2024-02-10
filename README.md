# ASP.NETCoreMVCTraining
The repository using for training ASP.NET Core MVC

Database migration by tool

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update

# Visual Studio

add-migration InitialCreate
database-update
```

How to run application
## 1. Visual Studio

- Select https option
- Click run application

## 2. Visual Studio Code

- Root folder - run command
```powershell
cd ASP.NETCoreMVCTraining
dotnet run
```

- Click address present in command line.

## 3. Docker Container
Setup https certificate before run follow [link](https://learn.microsoft.com/vi-vn/aspnet/core/security/docker-compose-https?view=aspnetcore-8.0)

- Root folder - run command
```powershell
docker-compose up -d trainingapp
```

## 4. Tye
### TBC
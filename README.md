# Tasks Manager Application

## Introduction

**This application is a work in progress.**

**This was built with the assistance of AI.** 

- Creates a new ASP.NET Core MVC app named TasksManager inside your workspace   
```dotnet new mvc -n TasksManager```  
```cd TasksManager```

-  Add the required packages  
```dotnet add package Microsoft.EntityFrameworkCore.Sqlite```  
```dotnet add package Microsoft.EntityFrameworkCore.Tools```  
```dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore```  

- Create the solution file   
```dotnet new sln```   
```dotnet sln add .\TasksManager.csproj```   

- Run the Application   
```dotnet run```   


- Setting up identity and database.   
    - Add EF Core design package   
    ```dotnet add package Microsoft.EntityFrameworkCore.Design```  
    ```dotnet add package Microsoft.AspNetCore.Identity.UI```   
    ```dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore```  

    Run dotnet restore so the project can actually use the packages added.   
    ```dotnet restore```   
    ```dotnet ef migrations add InitialCreate```   

    - Install tool ef  
    ```dotnet tool install --global dotnet-ef```      
    - Create the initial migration   
    ```dotnet ef migrations add InitialCreate```
    Done. To undo this action, use 'ef migrations remove'
    -  Apply the migration  
    ```dotnet ef database update```  
    - Scaffold the Identity pages:  
    ```dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design```   
    ```dotnet tool install --global dotnet-aspnet-codegenerator```   
    You can invoke the tool using the following command: dotnet-aspnet-codegenerator
    
    ```dotnet aspnet-codegenerator identity -dc TasksManager.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout"```   

- Create a Migragtion
```dotnet ef migrations add AddTaskModel```   
```dotnet ef database update```   

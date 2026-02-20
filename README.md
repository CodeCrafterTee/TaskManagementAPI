# Task Management API

## Tech Stack
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing

## Features
- User registration & login
- JWT authentication
- Create, update, delete tasks
- Assign priority levels
- Mark tasks as completed
- Filter tasks by status

## How to Run

1. Clone repository
2. Update connection string in appsettings.json
3. Run:
   dotnet ef database update
   dotnet run

4. Open Swagger at:
   https://localhost:5001/swagger
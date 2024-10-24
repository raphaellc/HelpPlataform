# HelpPlatform
 A Platform for helping each other in calamity situation

Comandos usados no migrations do Auth DB Context:
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef  
dotnet ef migrations add InitialCreate --context ApplicationDbContext    
dotnet ef database update --context ApplicationDbContext  
# H6.Fitness.App
<!-- GETTING STARTED -->
## Få Det startet

Dette er et eksempel på hvordan man får kørt programmet.

## Prerequisites

Her er et eksempel på hvordan man henter de programmer der er nødvendige for at kunne lave database migreringer.
* Visual Studio 2022
  ```sh
  Add-Migration foo
  Update-Database
  ```
* Jetbrains Rider
  ```sh
  dotnet tool install --global dotnet-ef
  dotnet ef migrations add "foo"
  dotnet ef database update
  ```
## Installation

1. Clone the repo
   ```sh
   git clone https://github.com/FrederikHKoch/H6.Fitness.Project.git
   ```
### Tilføj appsettings.json fil
1. Højre klik på konsol applikationen "Fitbod"
2. Add new -> appsettings.json
3. Kopier nedenfor ind under.
   ```sh
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "ConnectionStrings": {
        "FitbodContext": "Server=(DIN SERVER HER);Database=(DATABASENAVN);Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=true",
       } 
     }
   ```
## Tilføj brugere
1. log ind på "FitbodSuperuser@hotmail.com" med password "Test123#"
2. Opret Admin bruger (for at se alle knapper).
3. Log ud og opret almindelig bruger ved at sige registrer.
<p align="right">(<a href="#readme-top">back to top</a>)</p>

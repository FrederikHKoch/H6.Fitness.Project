# H6.Fitness.App
<!-- GETTING STARTED -->
## Få Det startet

Dette er et eksempel på hvordan man får kørt programmet.

### Prerequisites

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
### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/FrederikHKoch/H6.Fitness.Project.git
   ```
2. Tilføj appsettings.json fil
   2.1 Højre klik på konsol applikationen "Fitbod"
   2.2 Add new -> appsettings.json
   2.3 Kopier nedenfor ind under.
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
<p align="right">(<a href="#readme-top">back to top</a>)</p>

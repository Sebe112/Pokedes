# Pokedex

Et konsolprogram til håndtering af Pokémon og brugere. Programmet tillader oprettelse, visning, opdatering og sletning af Pokémon, samt oprettelse og login af brugere. Data gemmes i CSV-filer, og adgangskoder er krypteret for sikkerhed.

---

## 📋 Funktioner

### Brugerhåndtering
- **Opret bruger**: Tilføj en ny bruger med brugernavn og adgangskode.
- **Login**: Log ind med et eksisterende brugernavn og adgangskode.
- **Sikkerhed**: Adgangskoder gemmes som SHA-256 hashes.

### Pokémon-håndtering
- **Tilføj Pokémon**: Loggede brugere kan tilføje Pokémon til databasen.
- **Vis Pokémon**: Alle brugere kan se listen over Pokémon med pagination.
- **Søg efter Pokémon**: Søg efter Pokémon baseret på navn.
- **Opdater Pokémon**: Loggede brugere kan ændre typen og styrkeniveauet for eksisterende Pokémon.
- **Slet Pokémon**: Loggede brugere kan fjerne Pokémon fra databasen.

---

## ⚙️ Installation og Brug

### Forudsætninger
- .NET SDK (6.0 eller nyere) skal være installeret.
- En terminal eller kommandoprompt.

### Sådan Kører Du Programmet
1. Klon repository:
   ```cmd
   git clone https://github.com/Sebe112/Pokedes
   cd pokedex
   dotnet build
   dotnet run
   ```

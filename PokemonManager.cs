namespace Pokedex {
    /// <summary>
    /// Håndterer administration af Pokémon, herunder oprettelse, opdatering, sletning, og søgning.
    /// Pokémon gemmes og indlæses fra en CSV-fil.
    /// </summary>
    public class PokemonManager {
        /// <summary>
        /// Stien til CSV-filen, hvor Pokémon-data gemmes.
        /// </summary>
        private readonly string filePath = "PokeData.csv";

        /// <summary>
        /// Listen over Pokémon indlæst fra CSV-filen.
        /// </summary>
        private List<Pokemon> pokemons = new();

        /// <summary>
        /// Initialiserer PokemonManager og indlæser data fra CSV-filen.
        /// Hvis filen ikke findes, starter listen som tom.
        /// </summary>
        public PokemonManager() {
            LoadFromCsv();
        }

        /// <summary>
        /// Indlæser Pokémon-data fra CSV-filen og gemmer dem i listen.
        /// </summary>
        private void LoadFromCsv() {
            if (!File.Exists(filePath)) {
                Console.WriteLine("CSV fil ikke fundet. Starter uden data.");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            pokemons = lines.Select(Pokemon.FromCsv).ToList();
        }

        /// <summary>
        /// Gemmer Pokémon-data fra listen til CSV-filen.
        /// </summary>
        private void SaveToCsv() {
            var lines = pokemons.Select(p => p.ToCsv());
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// Tilføjer en ny Pokémon til systemet.
        /// </summary>
        /// <param name="navn">Navnet på Pokémon.</param>
        /// <param name="type">Typen af Pokémon.</param>
        /// <param name="styrkeniveau">Styrkeniveauet af Pokémon.</param>
        public void AddPokemon(PokemonNavne navn, Typer type, int styrkeniveau) {
            string formattedName = char.ToUpper(navn.ToString()[0]) + navn.ToString().Substring(1).ToLower();
            string formattedType = char.ToUpper(type.ToString()[0]) + type.ToString().Substring(1).ToLower();

            if (pokemons.Any(p => p.Navn.ToString() == formattedName)) {
                Console.WriteLine($"En Pokemon med navnet {formattedName} findes allerede.");
                return;
            }

            if (!Enum.TryParse<PokemonNavne>(formattedName, out var formattedNavn) ||
                !Enum.TryParse<Typer>(formattedType, out var formattedTypeEnum)) {
                Console.WriteLine($"Ugyldige værdier: Navn = {formattedName}, Type = {formattedType}. Tilføjelsen blev afbrudt.");
                return;
            }

            var newPokemon = new Pokemon(formattedNavn, formattedTypeEnum, styrkeniveau);
            pokemons.Add(newPokemon);
            SaveToCsv();
            Console.WriteLine($"Ny Pokemon {formattedName} ({formattedType}) tilføjet!");
        }

        /// <summary>
        /// Viser alle Pokémon med pagination.
        /// </summary>
        /// <param name="page">Den aktuelle side, der skal vises.</param>
        /// <param name="pageSize">Antal Pokémon pr. side.</param>
        public void ShowAllPokemons(int page = 1, int pageSize = 10) {
            if (pokemons.Count == 0) {
                Console.WriteLine("Ingen Pokemon fundet.");
                return;
            }

            var sortedPokemons = pokemons.OrderBy(p => p.ID).ToList();
            int totalPages = (int)Math.Ceiling((double)sortedPokemons.Count / pageSize);

            if (page < 1 || page > totalPages) {
                Console.WriteLine($"Forkert side nummer. vælg et tal mellem 1 og {totalPages}.");
                return;
            }

            var paginatedPokemons = sortedPokemons
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Console.WriteLine($"Viser side {page}/{totalPages}:");
            foreach (var pokemon in paginatedPokemons) {
                Console.WriteLine(pokemon.VisInfo());
            }
        }

        /// <summary>
        /// Opdaterer en eksisterende Pokémon baseret på ID.
        /// </summary>
        /// <param name="id">ID for Pokémon, der skal opdateres.</param>
        /// <param name="type">Den nye type for Pokémon.</param>
        /// <param name="styrkeniveau">Det nye styrkeniveau for Pokémon.</param>
        public void UpdatePokemon(int id, Typer type, int styrkeniveau) {
            string formattedType = char.ToUpper(type.ToString()[0]) + type.ToString().Substring(1).ToLower();
            var pokemon = pokemons.FirstOrDefault(p => p.ID == id);
            if (pokemon == null) {
                Console.WriteLine($"Ingen Pokemon fundet med det ID {id}.");
                return;
            }
            if (!Enum.TryParse<Typer>(formattedType, out var formattedTypeEnum)) {
                Console.WriteLine($"Ugyldig type: {formattedType}. Opdateringen blev afbrudt.");
                return;
            }

            pokemon.UpdatePokemon(formattedTypeEnum, styrkeniveau);
            SaveToCsv();
            Console.WriteLine("Pokemon opdateret!");
        }

        /// <summary>
        /// Sletter en Pokémon baseret på ID.
        /// </summary>
        /// <param name="id">ID for Pokémon, der skal slettes.</param>
        public void DeletePokemon(int id) {
            var pokemon = pokemons.FirstOrDefault(p => p.ID == id);
            if (pokemon == null) {
                Console.WriteLine($"Ingen Pokemon fundet med det ID {id}.");
                return;
            }

            pokemons.Remove(pokemon);
            SaveToCsv();
            Console.WriteLine("Pokemon fjernet!");
        }

        /// <summary>
        /// Søger efter Pokémon baseret på navn.
        /// </summary>
        /// <param name="name">Navnet eller del af navnet på Pokémon, der skal søges efter.</param>
        public void SearchPokemonByName(string name) {
            name = name.Trim().ToLower();
            var results = pokemons.Where(p => p.Navn.ToString().ToLower().Contains(name)).ToList();

            if (!results.Any()) {
                Console.WriteLine($"Ingen Pokemon fundet, der matcher navnet '{name}'.");
                return;
            }

            Console.WriteLine($"Søgeresultater for '{name}':");
            foreach (var pokemon in results) {
                Console.WriteLine(pokemon.VisInfo());
            }
        }
    }
}
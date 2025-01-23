namespace Pokedex {
    public class PokemonManager {
        private readonly string filePath = "PokeData.csv";
        private List<Pokemon> pokemons = new();

        public PokemonManager() {
            LoadFromCsv();
        }

        private void LoadFromCsv() {
            if (!File.Exists(filePath)) {
                Console.WriteLine("CSV fil ikke fundet. Starter uden data.");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            pokemons = lines.Select(Pokemon.FromCsv).ToList();
        }

        private void SaveToCsv() {
            var lines = pokemons.Select(p => p.ToCsv());
            File.WriteAllLines(filePath, lines);
        }

        // CREATE: Tilføj en ny Pokemon
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


        // READ: Vis Pokemon
        public void ShowAllPokemons(int page = 1, int pageSize = 10) {
            if (pokemons.Count == 0) {
                Console.WriteLine("Ingen Pokemon fundet.");
                return;
            }

            int totalPages = (int)Math.Ceiling((double)pokemons.Count / pageSize);

            if (page < 1 || page > totalPages) {
                Console.WriteLine($"Forkert side nummer. vælg et tal mellem 1 og {totalPages}.");
                return;
            }

            var paginatedPokemons = pokemons
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            Console.WriteLine($"Viser side {page}/{totalPages}:");
            foreach (var pokemon in paginatedPokemons) {
                Console.WriteLine(pokemon.VisInfo());
            }
        }

        // UPDATE: Opdater en eksisterende Pokemon
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

        // DELETE: Fjern en Pokemon
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
    }
}
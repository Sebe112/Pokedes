namespace Pokedex {
    public class Menu {
        private static PokemonManager manager = new PokemonManager();

        public static void MainMenu() {
            while (true) {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("        Pokémon Database          ");
                Console.WriteLine("==================================");
                Console.WriteLine("Klik (1): Tilføj en Pokémon");
                Console.WriteLine("Klik (2): Vis alle Pokémon i databasen");
                Console.WriteLine("Klik (3): Opdater en Pokémon");
                Console.WriteLine("Klik (4): Slet en Pokémon");
                Console.WriteLine("Klik (5): Afslut");
                Console.WriteLine("==================================");

                char input = Console.ReadKey().KeyChar;

                switch (input) {
                    case '1':
                        Console.Clear();
                        AddPokemon();
                        break;
                    case '2':
                        Console.Clear();
                        ShowAllPokemons();
                        break;
                    case '3':
                        Console.Clear();
                        UpdatePokemon();
                        break;
                    case '4':
                        Console.Clear();
                        DeletePokemon();
                        break;
                    case '5':
                        Console.WriteLine("\nFarvel!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nUgyldigt valg! Tryk på en vilkårlig tast for at prøve igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void AddPokemon() {
            Console.Write("Indtast Pokémon-navn (f.eks. Pikachu): ");
            string userInputName = Console.ReadLine().Trim();
            if (!Enum.TryParse<PokemonNavne>(char.ToUpper(userInputName[0]) + userInputName.Substring(1).ToLower(), out var navn)) {
                Console.WriteLine("Ugyldigt Pokémon-navn. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            Console.Write("Indtast type (f.eks. Electric, Fire): ");
            string userInputType = Console.ReadLine().Trim();
            if (!Enum.TryParse<Typer>(char.ToUpper(userInputType[0]) + userInputType.Substring(1).ToLower(), out var type)) {
                Console.WriteLine("Ugyldig type. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            Console.Write("Indtast styrkeniveau (f.eks. 50): ");
            if (!int.TryParse(Console.ReadLine(), out var styrkeniveau)) {
                Console.WriteLine("Ugyldigt styrkeniveau. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            manager.AddPokemon(navn, type, styrkeniveau);
            Console.WriteLine("Pokémon tilføjet! Tryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }

        private static void ShowAllPokemons() {
            Console.WriteLine("=== Liste over alle Pokémon ===");
            Console.Write("Indtast sidetal (standard er 1): ");
            int page = int.TryParse(Console.ReadLine(), out var pageNumber) ? pageNumber : 1;

            manager.ShowAllPokemons(page);
            Console.WriteLine("\nTryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }

        private static void UpdatePokemon() {
            Console.WriteLine("=== Opdater en Pokémon ===");
            Console.Write("Indtast ID for Pokémon, der skal opdateres: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) {
                Console.WriteLine("Ugyldigt ID. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            Console.Write("Indtast type (f.eks. Electric, Fire): ");
            string userInputType = Console.ReadLine().Trim();
            if (!Enum.TryParse<Typer>(char.ToUpper(userInputType[0]) + userInputType.Substring(1).ToLower(), out var type)) {
                Console.WriteLine("Ugyldig type. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            Console.Write("Indtast nyt styrkeniveau (f.eks. 50): ");
            if (!int.TryParse(Console.ReadLine(), out var styrkeniveau)) {
                Console.WriteLine("Ugyldigt styrkeniveau. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            manager.UpdatePokemon(id, type, styrkeniveau);
            Console.WriteLine("Pokémon opdateret! Tryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }

        private static void DeletePokemon() {
            Console.WriteLine("=== Slet en Pokémon ===");
            Console.Write("Indtast ID for Pokémon, der skal slettes: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) {
                Console.WriteLine("Ugyldigt ID. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            manager.DeletePokemon(id);
            Console.WriteLine("Pokémon slettet! Tryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }
    }
}
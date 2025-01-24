namespace Pokedex {
    /// <summary>
    /// Håndterer hovedmenuen for Pokémon-databasen.
    /// Tilbyder funktionalitet til at oprette brugere, logge ind og administrere Pokémon.
    /// </summary>
    public class Menu {
        private static UserManager userManager = new UserManager();
        private static PokemonManager pokemonManager = new PokemonManager();
        private static bool isLoggedIn = false;
        private static string loggedInUser = "";

        /// <summary>
        /// Starter hovedmenuen for programmet.
        /// Tilpasser menuvalgmuligheder baseret på loginstatus.
        /// </summary>
        public static void MainMenu() {
            while (true) {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("        Pokemon Database          ");
                Console.WriteLine("==================================");

                if (!isLoggedIn) {
                    Console.WriteLine("Klik (1): Opret en bruger");
                    Console.WriteLine("Klik (2): Log ind");
                    Console.WriteLine("Klik (3): Søg efter en Pokemon");
                    Console.WriteLine("Klik (4): Vis alle Pokemon i databasen");
                    Console.WriteLine("Klik (5): Afslut");
                }
                else {
                    Console.WriteLine($"Velkommen, {loggedInUser}!");
                    Console.WriteLine("Klik (1): Tilføj en Pokemon");
                    Console.WriteLine("Klik (2): Vis alle Pokemon i databasen");
                    Console.WriteLine("Klik (3): Opdater en Pokemon");
                    Console.WriteLine("Klik (4): Slet en Pokemon");
                    Console.WriteLine("Klik (5): Søg efter en Pokemon");
                    Console.WriteLine("Klik (6): Afslut");
                }

                Console.WriteLine("==================================");
                Console.Write("Vælg en mulighed: ");
                char input = Console.ReadKey().KeyChar;
                Console.Clear();

                if (!isLoggedIn) {
                    HandleGuestInput(input);
                }
                else {
                    HandleLoggedInInput(input);
                }
            }
        }

        /// <summary>
        /// Håndterer menuvalg for gæster (ikke-loggede brugere).
        /// </summary>
        /// <param name="input">Brugerens menuvalg.</param>
        private static void HandleGuestInput(char input) {
            switch (input) {
                case '1':
                    CreateUser();
                    break;
                case '2':
                    LoginUser();
                    break;
                case '3':
                    SearchPokemonByName();
                    break;
                case '4':
                    ShowAllPokemons();
                    break;
                case '5':
                    Console.WriteLine("Farvel!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk på en tast for at prøve igen.");
                    Console.ReadKey();
                    break;
            }
        }

        /// <summary>
        /// Håndterer menuvalg for loggede brugere.
        /// </summary>
        /// <param name="input">Brugerens menuvalg.</param>
        private static void HandleLoggedInInput(char input) {
            switch (input) {
                case '1':
                    AddPokemon();
                    break;
                case '2':
                    ShowAllPokemons();
                    break;
                case '3':
                    UpdatePokemon();
                    break;
                case '4':
                    DeletePokemon();
                    break;
                case '5':
                    SearchPokemonByName();
                    break;
                case '6':
                    Console.WriteLine("Farvel!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ugyldigt valg. Tryk på en tast for at prøve igen.");
                    Console.ReadKey();
                    break;
            }
        }

        /// <summary>
        /// Opretter en ny bruger.
        /// </summary>
        private static void CreateUser() {
            Console.WriteLine("=== Opret en ny bruger ===");
            Console.Write("Indtast brugernavn: ");
            string brugerNavn = Console.ReadLine();
            Console.Write("Indtast adgangskode: ");
            string adgangskode = Console.ReadLine();

            userManager.CreateUser(brugerNavn, adgangskode);
            Console.WriteLine("\nBruger oprettet! Tryk på en tast for at fortsætte.");
            Console.ReadKey();
        }

        /// <summary>
        /// Logger en eksisterende bruger ind.
        /// </summary>
        private static void LoginUser() {
            Console.WriteLine("=== Log ind ===");
            Console.Write("Indtast brugernavn: ");
            string brugerNavn = Console.ReadLine();
            Console.Write("Indtast adgangskode: ");
            string adgangskode = Console.ReadLine();

            if (userManager.LoginUser(brugerNavn, adgangskode)) {
                isLoggedIn = true;
                loggedInUser = brugerNavn;
            }

            Console.WriteLine("\nTryk på en tast for at fortsætte.");
            Console.ReadKey();
        }

        /// <summary>
        /// Tilføjer en ny Pokémon til databasen.
        /// Kun tilgængelig for loggede brugere.
        /// </summary>
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

            pokemonManager.AddPokemon(navn, type, styrkeniveau);
            Console.WriteLine("Pokémon tilføjet! Tryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }

        /// <summary>
        /// Viser alle Pokémon i databasen med pagination.
        /// </summary>
        private static void ShowAllPokemons() {
            Console.WriteLine("=== Vis alle Pokemon ===");
            Console.Write("Indtast sidetal (standard er 1): ");
            int page = int.TryParse(Console.ReadLine(), out var pageNumber) ? pageNumber : 1;

            pokemonManager.ShowAllPokemons(page);
            Console.WriteLine("\nTryk på en tast for at fortsætte.");
            Console.ReadKey();
        }

        /// <summary>
        /// Opdaterer en eksisterende Pokémon.
        /// Kun tilgængelig for loggede brugere.
        /// </summary>
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

            pokemonManager.UpdatePokemon(id, type, styrkeniveau);
            Console.WriteLine("Pokémon opdateret! Tryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }

        /// <summary>
        /// Sletter en eksisterende Pokémon fra databasen.
        /// Kun tilgængelig for loggede brugere.
        /// </summary>
        private static void DeletePokemon() {
            Console.WriteLine("=== Slet en Pokémon ===");
            Console.Write("Indtast ID for Pokémon, der skal slettes: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) {
                Console.WriteLine("Ugyldigt ID. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            pokemonManager.DeletePokemon(id);
            Console.WriteLine("Pokémon slettet! Tryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }

        /// <summary>
        /// Søger efter en Pokémon baseret på navn.
        /// </summary>
        private static void SearchPokemonByName() {
            Console.WriteLine("=== Søg efter en Pokémon ===");
            Console.Write("Indtast navn eller del af navn for at søge: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name)) {
                Console.WriteLine("Søgeterm kan ikke være tom. Tryk på en tast for at gå tilbage.");
                Console.ReadKey();
                return;
            }

            pokemonManager.SearchPokemonByName(name);
            Console.WriteLine("\nTryk på en tast for at gå tilbage.");
            Console.ReadKey();
        }
    }
}
namespace Pokedex {
    /// <summary>
    /// Håndterer oprettelse, login og lagring af brugere i en CSV-fil.
    /// </summary>
    class UserManager {
        /// <summary>
        /// Stien til CSV-filen, hvor brugerdata gemmes.
        /// </summary>
        private readonly string filePath = "users.csv";

        /// <summary>
        /// Listen over brugere indlæst fra CSV-filen.
        /// </summary>
        private List<User> users = new();

        /// <summary>
        /// Initialiserer UserManager og indlæser brugere fra CSV-filen.
        /// Hvis filen ikke findes, oprettes en tom liste.
        /// </summary>
        public UserManager() {
            LoadUsersFromCsv();
        }

        /// <summary>
        /// Indlæser brugere fra CSV-filen til `users`-listen.
        /// </summary>
        private void LoadUsersFromCsv() {
            if (!File.Exists(filePath)) {
                Console.WriteLine("Brugerfil ikke fundet. Starter med tom liste.");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            users = lines.Select(User.FromCsv).ToList();
        }

        /// <summary>
        /// Gemmer alle brugere fra `users`-listen til CSV-filen.
        /// </summary>
        private void SaveUsersToCsv() {
            var lines = users.Select(u => u.ToCsv());
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// Opretter en ny bruger og tilføjer den til systemet.
        /// Brugernavnet skal være unikt.
        /// </summary>
        /// <param name="brugerNavn">Brugernavnet for den nye bruger.</param>
        /// <param name="adgangskode">Adgangskoden for den nye bruger.</param>
        public void CreateUser(string brugerNavn, string adgangskode) {
            if (users.Any(u => u.BrugerNavn == brugerNavn)) {
                Console.WriteLine("En bruger med dette navn findes allerede.");
                return;
            }

            var newUser = new User(brugerNavn, adgangskode);
            users.Add(newUser);
            SaveUsersToCsv();
            Console.WriteLine("Ny bruger oprettet!");
        }

        /// <summary>
        /// Logger en bruger ind ved at verificere brugernavn og adgangskode.
        /// </summary>
        /// <param name="brugerNavn">Brugernavnet for brugeren.</param>
        /// <param name="adgangskode">Adgangskoden for brugeren.</param>
        /// <returns>Returnerer true, hvis login lykkes, ellers false.</returns>
        public bool LoginUser(string brugerNavn, string adgangskode) {
            var user = users.FirstOrDefault(u => u.BrugerNavn == brugerNavn);
            if (user == null) {
                Console.WriteLine("Brugernavn ikke fundet.");
                return false;
            }

            if (user.VerifyPassword(adgangskode)) {
                Console.WriteLine("Logget ind succesfuldt!");
                return true;
            }
            else {
                Console.WriteLine("Forkert adgangskode.");
                return false;
            }
        }
    }
}

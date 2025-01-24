using System.Security.Cryptography;
using System.Text;

namespace Pokedex {
    /// <summary>
    /// Repræsenterer en bruger med et brugernavn og en adgangskode.
    /// Adgangskoden gemmes som en hash.
    /// </summary>
    class User {
        /// <summary>
        /// Brugernavnet for brugeren.
        /// </summary>
        public string BrugerNavn { get; private set; }

        /// <summary>
        /// Hashet adgangskode for brugeren.
        /// </summary>
        public string Adgangskode { get; private set; }

        /// <summary>
        /// Initialiserer en ny instans af User-klassen.
        /// Adgangskoden kan hashe automatisk eller gemmes direkte baseret på hashPassword-parameteren.
        /// </summary>
        /// <param name="brugerNavn">Brugernavnet for brugeren.</param>
        /// <param name="adgangskode">Adgangskoden for brugeren. Hasher automatisk, medmindre hashPassword er false.</param>
        /// <param name="hashPassword">Angiver, om adgangskoden skal hashes.</param>
        public User(string brugerNavn, string adgangskode, bool hashPassword = true) {
            BrugerNavn = brugerNavn;
            Adgangskode = hashPassword ? HashPassword(adgangskode) : adgangskode;
        }

        /// <summary>
        /// Konverterer brugeren til en CSV-streng.
        /// </summary>
        /// <returns>Returnerer en CSV-repræsentation af brugeren.</returns>
        public string ToCsv() {
            return $"{BrugerNavn},{Adgangskode}";
        }

        /// <summary>
        /// Laver en ny bruger fra en CSV-linje.
        /// </summary>
        /// <param name="csvLine">En linje fra CSV-filen med formatet "BrugerNavn,Adgangskode".</param>
        /// <returns>Returnerer en User-instans baseret på CSV-data.</returns>
        public static User FromCsv(string csvLine) {
            var data = csvLine.Split(',');
            return new User(data[0], data[1], false);
        }

        /// <summary>
        /// Hasher en adgangskode ved hjælp af SHA256.
        /// </summary>
        /// <param name="password">Adgangskoden, der skal hashes.</param>
        /// <returns>Returnerer en hexadecimalt repræsenteret hash af adgangskoden.</returns>
        private static string HashPassword(string password) {
            using (var sha256 = SHA256.Create()) {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes) {
                    builder.Append(b.ToString("x2")); // Konverter til hex string
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Bekræfter, om en indtastet adgangskode matcher den gemte hash.
        /// </summary>
        /// <param name="inputPassword">Den adgangskode, der skal verificeres.</param>
        /// <returns>Returnerer true, hvis adgangskoden matcher, ellers false.</returns>
        public bool VerifyPassword(string inputPassword) {
            string inputHash = HashPassword(inputPassword);
            return inputHash == Adgangskode;
        }
    }
}
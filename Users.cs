namespace Pokedex {
    class User {
        public string BrugerNavn { get; private set; }
        public string Adgangskode { get; private set; }
        public User(string brugerNavn, string adgangskode) {
            BrugerNavn = brugerNavn;
            Adgangskode = adgangskode;
        }
    }
}
namespace Pokedex {
    /// <summary>
    /// Repræsenterer en Pokémon med ID, navn, type og styrkeniveau.
    /// </summary>
    public class Pokemon {
        /// <summary>
        /// Unikt ID for Pokémon, baseret på dens navn.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Navnet på Pokémon.
        /// </summary>
        public PokemonNavne Navn { get; private set; }

        /// <summary>
        /// Typen af Pokémon (f.eks. Electric, Fire, Water).
        /// </summary>
        public Typer Type { get; private set; }

        /// <summary>
        /// Styrkeniveauet for Pokémon.
        /// </summary>
        public int Styrkeniveau { get; private set; }

        /// <summary>
        /// Initialiserer en ny instans af Pokemon-klassen.
        /// </summary>
        /// <param name="navn">Navnet på Pokémon.</param>
        /// <param name="type">Typen af Pokémon.</param>
        /// <param name="styrkeniveau">Styrkeniveauet af Pokémon.</param>
        public Pokemon(PokemonNavne navn, Typer type, int styrkeniveau) {
            ID = (int)navn;
            Navn = navn;
            Type = type;
            Styrkeniveau = styrkeniveau;
        }

        /// <summary>
        /// Returnerer en streng med information om Pokémon.
        /// </summary>
        /// <returns>En streng med ID, navn, type og styrkeniveau for Pokémon.</returns>
        public string VisInfo() {
            return $"Pokemon ID: {ID}, Navn: {Navn}, Type: {Type}, Styrkeniveau: {Styrkeniveau}";
        }

        /// <summary>
        /// Opdaterer typen og styrkeniveauet for Pokémon.
        /// </summary>
        /// <param name="type">Den nye type for Pokémon.</param>
        /// <param name="styrkeniveau">Det nye styrkeniveau for Pokémon.</param>
        public void UpdatePokemon(Typer type, int styrkeniveau) {
            Type = type;
            Styrkeniveau = styrkeniveau;
        }

        /// <summary>
        /// Konverterer Pokémon til en CSV-streng.
        /// </summary>
        /// <returns>En streng i CSV-format, der repræsenterer Pokémon.</returns>
        public string ToCsv() {
            return $"{ID},{Navn},{Type},{Styrkeniveau}";
        }

        /// <summary>
        /// Opretter en ny instans af Pokémon baseret på data fra en CSV-linje.
        /// </summary>
        /// <param name="csvLine">En linje fra CSV-filen med formatet "ID,Navn,Type,Styrkeniveau".</param>
        /// <returns>En Pokémon baseret på CSV-data.</returns>
        public static Pokemon FromCsv(string csvLine) {
            var data = csvLine.Split(',');
            return new Pokemon(
                Enum.Parse<PokemonNavne>(data[1]),
                Enum.Parse<Typer>(data[2]),
                int.Parse(data[3])
            );
        }
    }
}
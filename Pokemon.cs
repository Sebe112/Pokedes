namespace Pokedex {
    public class Pokemon {
        public int ID { get; private set; }
        public PokemonNavne Navn { get; private set; }
        public Typer Type { get; private set; }
        public int Styrkeniveau { get; private set; }

        public Pokemon(PokemonNavne navn, Typer type, int styrkeniveau) {
            ID = (int)navn;
            Navn = navn;
            Type = type;
            Styrkeniveau = styrkeniveau;
        }

        public string VisInfo() {
            return $"Pokemon ID: {ID}, Navn: {Navn}, Type: {Type}, Styrkeniveau: {Styrkeniveau}";
        }

        public void UpdatePokemon(Typer type, int styrkeniveau) {
            Type = type;
            Styrkeniveau = styrkeniveau;
        }

        public string ToCsv() {
            return $"{ID},{Navn},{Type},{Styrkeniveau}";
        }

        public static Pokemon FromCsv(string csvLine)
        {
            var data = csvLine.Split(',');
            return new Pokemon(
                Enum.Parse<PokemonNavne>(data[1]),
                Enum.Parse<Typer>(data[2]),
                int.Parse(data[3])
            );
        }
    }
}
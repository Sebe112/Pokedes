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
    }
}
namespace Pokedex {
    public class Pokemon {
        public int ID { get; private set; }
        public PokemonNavne Navn { get; private set; }
        public Typer Type { get; private set; }
        public int Styrkeniveau { get; private set; }

        public Pokemon(int id, PokemonNavne navn, Typer type, int styrkeniveau)
        {
            ID = id;
            Navn = navn;
            Type = type;
            Styrkeniveau = styrkeniveau;
        }
    }
}
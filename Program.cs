namespace Pokedex {
    class Program {
        static void Main(string[] args) {
            Pokemon poke1 = new Pokemon(PokemonNavne.Caterpie, Typer.Bug, 200);

            Console.WriteLine(poke1.VisInfo());
        }
    }
}
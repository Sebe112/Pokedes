namespace Pokedex {
    public class Menu{
        public static void MainMenu() {
            Console.Clear();
            Console.WriteLine("Press a key (a, b, c, d, or e):");

            // Read a single character without waiting for Enter
            char input = Console.ReadKey().KeyChar;

            switch (input)
            {
                case 'a':
                    Console.WriteLine("You pressed 'a'.");
                    break;
                case 'b':
                    Console.WriteLine("You pressed 'b'.");
                    break;
                case 'c':
                    Console.WriteLine("You pressed 'c'.");
                    break;
                case 'd':
                    Console.WriteLine("You pressed 'd'.");
                    break;
                case 'e':
                    Console.WriteLine("You pressed 'e'.");
                    break;
                default:
                    Console.WriteLine($"Invalid key '{input}'. Please press a, b, c, d, or e.");
                    break;
            }
        }
    }
}
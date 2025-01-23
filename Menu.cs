namespace Pokedex {
    public class Menu{
        public static void MainMenu() {
            Console.Clear();
            Console.WriteLine("Press a key (1, 2, 3, 4, or 5):");

            char input = Console.ReadKey().KeyChar;

            while (true)
            {
                switch (input)
                {
                case '1':
                    Console.Clear();
                    Console.WriteLine("You pressed '1'.");
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("You pressed '2'.");
                    break;
                case '3':
                    Console.Clear();
                    Console.WriteLine("You pressed '3'.");
                    break;
                case '4':
                    Console.Clear();
                    Console.WriteLine("You pressed '4'.");
                    break;
                case '5':
                    Console.Clear();
                    Console.WriteLine("You pressed '5'.");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine($"Invalid key '{input}'. Please press 1, 2, 3, 4, or 5.");
                    return;
                }
            } 
        }
    }
}
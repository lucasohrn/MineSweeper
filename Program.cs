using System;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Kodar output av UTF-8 så den kan synas i terminalen
            Board board = new Board(args); // Deklarerar en ny variabel board av typen struct. Genererar nytt bräde, kan ta in argument från konsolen
            MineSweeper game = new MineSweeper(); // Deklarerar en ny variabel game av typen struct. Genererar ett nytt spel.

            game.Run(board); // Anropar metoden game.Run() som startar spelet med ett nytt bräde som in argument.
        }
    }
}
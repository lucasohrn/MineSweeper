using System;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Board board = new Board(args);
            MineSweeper game = new MineSweeper();

            game.Run(board);
        }
    }
}
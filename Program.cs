using System;

namespace MineSweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(args);
            MineSweeper game = new MineSweeper();

            game.Run(board);
        }
    }
}
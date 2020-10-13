using System;

namespace MineSweeper
{
    public class Helper
    {
        const int TOTAL_NUMBER_MINES = 10;
        static Random random;
        static bool[,] boobyTraps;

        public static void Initialize(string[] args)
        {
            if (args.Length > 0)
            {
                string arg0 = args[0];
                int seed;
                bool success = int.TryParse(arg0, out seed);

                if (success)
                {
                    random = new Random(seed);
                }
            }
            else
            {
                random = new Random();
            }

            boobyTraps = new bool[10, 10];

            int nrPlacedMines = 0;

            while (nrPlacedMines < 10)
            {
                int row = random.Next(0, 10);
                int column = random.Next(0, 10);

                if (!boobyTraps[row, column])
                {
                    boobyTraps[row, column] = true;
                    nrPlacedMines++;
                }
            }
        }
        public static bool BoobyTrapped(int row, int column)
        {
            if (row >= 0 && row < boobyTraps.GetLength(1) && column >= 0 && column < boobyTraps.GetLength(0))
            {
                return boobyTraps[row, column];
            }

            return false;
        }
    }
}
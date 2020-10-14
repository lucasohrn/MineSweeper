using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{

    struct Board
    {
        public Square[,] board;
        private int flagCount, sweepedCount;

        // Konstruktor som initaliserar ny spelplan
        public Board(string[] args)
        {
            Helper.Initialize(args);
            board = new Square[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    board[row, col] = new Square(Helper.BoobyTrapped(row, col));
                }
            }

            // UTRÄKNING AV NÄRLIGGANDE MINOR
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    // Med hjälp av nästlade for-loopar så hittar jag totalt 10 board[row, col] som är en mina
                    if (board[row, col].BoobyTrapped)
                    {
                        int tempRow = row - 1;  // Tar värdet på raden där minan är och minskar med 1

                        for (int i = 0; i < 3; i++)  // For-lopp som loppar igenom raden
                        {
                            int tempCol = col;
                            tempCol -= 1;

                            for (int j = 0; j < 3; j++)
                            {
                                if (tempRow < 0 || tempCol < 0 || tempRow > 9 || tempCol > 9)
                                {
                                }

                                else if (!board[tempRow, tempCol].BoobyTrapped)
                                {
                                    board[tempRow, tempCol].IncrementCloseMineCount();
                                }
                                tempCol++;
                            }
                            tempRow += 1;
                        }
                    }
                }
            }

            flagCount = 0;
            sweepedCount = 0;
        }

        // skriver ut om spelaren har vunnit
        public bool PlayerWon()
        {
            Print();
            Console.WriteLine("\nWELL DONE!\n");
            return true;
        }

        // skriver ut om spelaren förlorar
        public bool GameOver()
        {
            Console.WriteLine("\n    A B C D E F G H I J\n  +--------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + " |");
                for (int col = 0; col < 10; col++)
                {
                    board[col, row].PrintGameOverBoard();
                    ChangeColor(col, row);
                    Console.Write(" " + board[col, row].Symbol);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nGAME OVER\n");
            return true;
        }

        // Försökt röja en ruta. Returnerar false om ogiltigt drag, annars true
        public bool TrySweep(int row, int col)
        {
            if (board[row, col].TrySweep())
            {
                sweepedCount++;
                
                if (sweepedCount == 90)
                {
                    PlayerWon();
                    return false;
                }
                
                else if (board[row, col].Symbol == (char)Square.GameSymbol.SweepedZeroCloseMine)
                {
                    int tempRow = row - 1;  // Tar värdet på raden där minan är och minskar med 1

                    for (int i = 0; i < 3; i++)  // For-lopp som loppar igenom raden
                    {
                        int tempCol = col;
                        tempCol -= 1;

                        for (int j = 0; j < 3; j++)
                        {
                            if (tempRow < 0 || tempCol < 0 || tempRow > 9 || tempCol > 9)
                            {
                            }

                            else
                            {
                                if (board[tempRow, tempCol].Symbol == (char)Square.GameSymbol.NotSweeped)
                                {
                                    TrySweep(tempRow, tempCol);
                                }
                            }
                            tempCol++;
                        }
                        tempRow += 1;
                    }
                }
                return true;
            }

            else
            {
                GameOver();
                return false;
            }
        }

        // försök flaga, returnera false om ogiltigt drag, annars true
        public bool TryFlag(int row, int col)
        {
            if (flagCount < 25)
            {
                if (board[row, col].TryFlag())
                {
                    flagCount++;
                    return true;
                }
                else
                {
                    flagCount--;
                }
                return true;
            }

            else if (flagCount == 25)
            {
                if (board[row, col].Symbol == (char)Square.GameSymbol.Flagged)
                {
                    board[row, col].TryFlag();
                    flagCount--;
                }
                else
                {
                    throw new Exception("not allowed");
                }
                return true;
            }

            else
            {
                throw new Exception("not allowed");
            }
        }

        // skriv ut spelplanen
        public void Print()
        {
            Console.WriteLine("\n    A B C D E F G H I J\n  +--------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + " |");
                for (int col = 0; col < 10; col++)
                {
                    ChangeColor(col, row);
                    Console.Write(" " + board[col, row].Symbol);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();

            }
        }

        public bool ChangeColor(int row, int col)
        {
            if (board[row, col].Symbol == (char)Square.GameSymbol.Flagged)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (board[row, col].Symbol == '1')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (board[row, col].Symbol == '2')
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (board[row, col].Symbol == '3')
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (board[row, col].Symbol == '4')
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (board[row, col].Symbol == '5')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (board[row, col].Symbol == '6')
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (board[row, col].Symbol == '7')
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            else if (board[row, col].Symbol == '8')
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            else if (board[row, col].Symbol == (char)Square.GameOverSymbol.ExplodedMine)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (board[row, col].Symbol == (char)Square.GameOverSymbol.FlaggedMine)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (board[row, col].Symbol == (char)Square.GameOverSymbol.Mine)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (board[row, col].Symbol == (char)Square.GameOverSymbol.MisplacedFlag)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            return true;
        }
    }
}
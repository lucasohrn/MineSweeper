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
        public bool PlayerWon => false;

        // skriver ut om spelaren förlorar
        public bool GameOver => false;

        // Försökt röja en ruta. Returnerar false om ogiltigt drag, annars true
        public bool TrySweep(int row, int col)
        {
            if (board[row, col].TrySweep(GameOver))
            {
                sweepedCount++;
                if (board[row, col].Symbol == (char)Square.GameSymbol.SweepedZeroCloseMine)
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

                                /*
                        0,0 1,0 2,0 3,0
                        0,1 1,1 2,1
                        0,2 1,2 2,2
                        */
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
                if (board[row, col].Symbol == (char)Square.GameOverSymbol.ExplodedMine)
                {
                    //GameOver = true;
                }
                return true;
            }

            return false;
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
                return false;
            }

            else if (flagCount == 25)
            {
                //Implementation för flagcount när den är 25
            }

            else
            {
                throw new Exception("not allowed");
            }
        }

        // skriv ut spelplanen
        public void Print()
        {
            Console.WriteLine("   A B C D E F G H I J\n+-----------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + "|");
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(" " + board[col, row].Symbol);
                }
                Console.WriteLine();
            }
        }
    }
}
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
            // Med nästlade for-loopar så går algoritmen igenom alla platser i brädet
            // OM platsen är en mina
            //    Så ökar värdet på alla 8 platser runt om minan med 1.
            //    Detta med hjälp av två till for-loopar.
            //    men endast OM värdet på tempRow och tempCol är mellan 0-9. För att inte rendera platser som är utanför brädet
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    // Loopar igenom alla platser i brädet
                    if (board[row, col].BoobyTrapped)   // OM en ruta i brädet är en mina
                    {
                        int tempRow = row - 1;              // Ta värdet på raden där minan är och minskar med 1, tilldelar variabeln tempRow värdet
                        for (int i = 0; i < 3; i++)         // Går in i en ny for-loop som körs tre gånger för att ta kolla de horisontella angränsande rutorna till minan
                        {
                            int tempCol = col - 1;              // Ta värdet på columen där minan är och minskar med 1, tilldelar variabeln tempCol värdet
                            for (int j = 0; j < 3; j++)         // Går in i en ny for-loop som körs tre gånger för att ta kolla de vertikala angränsande rutorna till minan
                            {
                                if (tempRow < 0 || tempCol < 0 || tempRow > 9 || tempCol > 9)   // OM rutan algoritmen kollar är utanför brädet så hoppa över
                                {
                                }
                                else if (!board[tempRow, tempCol].BoobyTrapped)                 // ANNARS OM rutan inte har en mina
                                {
                                    board[tempRow, tempCol].IncrementCloseMineCount();              // Anropa metoden IncrementCloseMineCount() för att öka värdet med 1 för rutan
                                }
                                tempCol++;  // Öka variabeln tempCols värde med 1
                            }
                            tempRow ++;   // Öka variabeln tempRow värde med 1
                        }
                    }
                }
            }

            flagCount = 0;      // flagg räkningen tilldelas värde 0
            sweepedCount = 0;   // röjar räkningen tilldelas värde 0
        }

        // Metod för att skriva ut om spelaren har vunnit
        public bool PlayerWon()
        {
            Print();                                // Anropar metoden Print() för att skriva ut en sista gång
            Console.WriteLine("\nWELL DONE!\n");    // Skriv ut
            return true;
        }

        // Metod för att skriva ut om spelaren har förlorat
        public bool GameOver()
        {
            Console.WriteLine("\n    A B C D E F G H I J\n  +--------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + " |");
                for (int col = 0; col < 10; col++)
                {
                    // Går igenom brädet och ändrar rutornas värde på GameOver till true för att
                    // symbolerna ska ändras och sedan skrivas ut
                    board[col, row].GameOver = true;
                    Console.Write(" " + board[col, row].Symbol);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nGAME OVER\n"); // Skriv ut
            return true;
        }

        // Rekursiv metod som testar att sweepa en ruta, flera rutor OMrutan användaren valt att röja inte har någon angränsande mina/minor
        // Tar in två parametrar som inehåller värdet av vald rad och column
        public bool TrySweep(int row, int col)
        {
            if (board[row, col].TrySweep()) // OM anropet av metoden board[row, col].Trysweep() returnerar true, fortsätt
            {
                sweepedCount++;                 // Ökar sweepedCount med 1

                if (sweepedCount == 90)             // OM sweepedCount är lika med 90
                {
                    PlayerWon();                        // Anropar PlayerWon()
                    return false;                       // returnerar false    
                }

                else if (board[row, col].Symbol == (char)Square.GameSymbol.SweepedZeroCloseMine)    // ANNARS OM board[row,col].Symbol är samma som '.'
                {
                    int tempRow = row - 1;                  // Ta värdet på raden där minan är och minskar med 1, tilldelar variabeln tempRow värdet

                    for (int i = 0; i < 3; i++)             // Går in i en ny for-loop som körs tre gånger för att ta kolla de horisontella angränsande rutorna till minan
                    {
                        int tempCol = col - 1;                  // Ta värdet på columen där minan är och minskar med 1, tilldelar variabeln tempCol värdet

                        for (int j = 0; j < 3; j++)             // Går in i en ny for-loop som körs tre gånger för att ta kolla de vertikala angränsande rutorna till minan
                        {
                            if (tempRow < 0 || tempCol < 0 || tempRow > 9 || tempCol > 9)   // OM rutan algoritmen kollar är utanför brädet så hoppa över
                            {
                            }

                            else    // ANNARS
                            {
                                if (board[tempRow, tempCol].Symbol == (char)Square.GameSymbol.NotSweeped)   // OM symbolen på rutan är 'X', inte sweept
                                {
                                    TrySweep(tempRow, tempCol);     // Metoden vi är inne i anropar sig själv med två in argument som är den temporära raden och columnen
                                                                    // Detta för att se om grannarnas grannar till användarens röjda ruta har grannar som
                                }                                   // också är osvepta och inte har några intilliggande minor
                            }
                            tempCol++;  // Ökar variabeln tempCols värde med 1
                        }
                        tempRow ++;   //Ökar variabeln tempRows värde med 1
                    }
                }
                return true;    // Returnerar true
            }

            else                        // ANNARS
            {
                GameOver();             // GameOver() anropas
                return false;           // Returnerar false
            }
        }

        // Metod som testar att flagga en ruta. Tar in två parametrar som inehåller värdet av vald rad och column
        public bool TryFlag(int row, int col)
        {
            if (flagCount < 25)     // OM flagCount är mindre än 25
            {
                if (board[row, col].TryFlag())  // OM anropet av metoden board[row, col].TryFlag() returnerar true, fortsätt
                {
                    flagCount++;                    // Ökar flagCount med 1
                    return true;                    // Returnerar true
                }
                else                            // ANNARS
                {
                    flagCount--;                    // Minskar flagCount med 1
                }
                return true;                    // Returnerar true
            }

            else if (flagCount == 25)           // ANNARS OM flagCount är lika med 25
            {
                if (board[row, col].Symbol == (char)Square.GameSymbol.Flagged) // OM board[row, col].symbol är 'F'
                {
                    board[row, col].TryFlag();                                      // Anropar metoden board[row, col].TryFlag()
                    flagCount--;                                                    // Minskar flagCount med 1
                }
                else                                                            // ANNARS
                {
                    throw new Exception("not allowed");                             // Kastar nytt felmedellande
                }
                return true;
            }

            else                                                                // ANNARS
            {
                throw new Exception("not allowed");                                 // Kastar nytt felmedellande
            }
        }

        // Metod som skriver ut spelplanen
        public void Print()
        {
            // Loopar igenom alla platser i brädet med nästlade for-loopar
            Console.WriteLine("\n    A B C D E F G H I J\n  +--------------------");
            for (int row = 0; row < 10; row++)
            {
                Console.Write(row + " |");
                for (int col = 0; col < 10; col++)
                {
                    Console.Write(" " + board[col, row].Symbol); // Skriv ut symbolen
                }
                Console.WriteLine();

            }
        }
    }
}
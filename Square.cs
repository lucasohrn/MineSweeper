using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    // Typ för en ruta på spelplanen.
    struct Square
    {
        // enum symboler för 
        public enum GameSymbol
        {
            Flagged = 'F',
            NotSweeped = 'X',
            SweepedZeroCloseMine = '.'
        }

        public enum GameOverSymbol
        {
            ExplodedMine = 'M',
            FlaggedMine = 'ɯ',
            Mine = 'm',
            MisplacedFlag = 'Ⅎ'
        }

        private int closeMineCount; // Antal minor på intilliggande rutor
        private bool flagged, boobyTrapped, sweeped;
        private char symbol;

        // Konstruktor som initierar en ny ruta på spelplanen.
        public Square(bool isBoobyTrapped)
        {
            closeMineCount = 0;
            flagged = false;
            boobyTrapped = isBoobyTrapped;
            sweeped = false;
            symbol = (char)Square.GameSymbol.NotSweeped;
        }

        private int CloseMineCount
        {
            get { return closeMineCount; }
            set { closeMineCount = value; }
        }

        // Enbart läsbar egenskap som säger om rutan är flaggad för tillfället.
        public bool IsFlagged => flagged;

        // Enbart läsbar egenskap som säger om rutan är minerad.
        public bool BoobyTrapped => boobyTrapped;

        // Enbart läsbar egenskap som säger om rutan har blivit röjd. 
        public bool IsSweeped => sweeped;

        // Enbart läsbar egenskap som är symbolen som representerar rutan just nu 
        // om spelplanen skall ritas ut.
        public char Symbol => symbol;

        // Enbart skrivbar egenskap som tilldelas true för alla rutor om spelaren 
        // röjer en minerad ruta 
        public bool GameOver
        {
            set
            {
                if (value)
                {
                    if (flagged && boobyTrapped)
                    {
                        symbol = (char)Square.GameOverSymbol.FlaggedMine;
                    }

                    if (flagged && !boobyTrapped)
                    {
                        symbol = (char)Square.GameOverSymbol.MisplacedFlag;
                    }

                    if (!sweeped && boobyTrapped && !flagged)
                    {
                        symbol = (char)Square.GameOverSymbol.Mine;
                    }

                    if (sweeped && boobyTrapped)
                    {
                        symbol = (char)Square.GameOverSymbol.ExplodedMine;
                    }

                    if (!sweeped && !boobyTrapped && !flagged)
                    {
                        TrySweep();
                    }
                }
            }
        }

        // Öka räknaren av minor på intilliggande rutor med 1.
        public void IncrementCloseMineCount()
        {
            closeMineCount += 1;
        }

        // Försök att flagga rutan. Returnerar false om ogiltigt drag, annars true.
        public bool TryFlag()
        {
            if (sweeped)                                                    // OM rutan är sweeped
            {
                throw new Exception("not allowed");                             // Kastar nytt felmedellande
            }
            else                                                            // ANNARS
            {
                if (flagged)                                                // OM rutan redan är flaggad
                {
                    symbol = (char)Square.GameSymbol.NotSweeped;                // Ändrar symbolen 'F' ut till 'X'
                    flagged = !flagged;                                         // värdet bytes från flaggad till inte flaggad
                    return false;                                               // returnerar false
                }
                else                                                        // ANNARS
                {
                    symbol = (char)Square.GameSymbol.Flagged;                   // Ändrar symbolen från 'X' till 'F'
                    flagged = !flagged;                                         // Värdet byts
                    return true;                                                // returnerar true
                }
            }
        }

        // Försök röja rutan. Returnerar false om ogiltigt drag, annars true.
        public bool TrySweep()
        {
            if (!sweeped && !flagged && !boobyTrapped)                      // OM positionen inte är sweeped, flagged eller boobyTrapped
            {
                sweeped = true;                                                 // Rutans variabel sweeped blir true
                if (closeMineCount == 0)                                        // OM closeMineCount är 0
                {
                    symbol = (char)Square.GameSymbol.SweepedZeroCloseMine;          // Rutans värde ändras till '.'
                }

                else                                                            // ANNARS
                {
                    symbol = char.Parse(closeMineCount.ToString());                 // Med hjälp av Parse, förvandla symbol till sträng
                }
                return true;                                                    // Returnerar true
            }

            else if (boobyTrapped && !flagged)                              // ANNARS OM rutan är boobyTrapped och inte flaggad
            {
                sweeped = true;                                                 // Sveper rutan på brädet
                GameOver = true;                                                // GamerOver blir true som avslutar spelet med "Game Over" medelande
                return false;                                                   // Returnera false
            }

            else                                                            // ANNARS
            {
                throw new Exception("not allowed");                             // Kastar nytt felmedellande
            }
        }
    }
}
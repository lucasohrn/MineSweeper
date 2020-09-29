  
using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{
    // Typ för en ruta på spelplanen.
    struct Square
    {
        enum GameSymbol
        { 
            Flagged = 'F',
            NotSweeped = 'X',
            SweepedZeroCloseMine = '.'
        }

        enum GameOverSymbol
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
            this.boobyTrapped = isBoobyTrapped;
            sweeped = false;
            symbol = 'X';
        }

        // Enbart läsbar egenskap som säger om rutan är flaggad för tillfället.
        public bool IsFlagged => false; // Stubbe

        // Enbart läsbar egenskap som säger om rutan är minerad.
        public bool BoobyTrapped => false; // Stubbe

        // Enbart läsbar egenskap som säger om rutan har blivit röjd. 
        public bool IsSweeped => false; // Stubbe

        // Enbart läsbar egenskap som är symbolen som representerar rutan just nu 
        // om spelplanen skall ritas ut.
        public char Symbol => (char)GameSymbol.NotSweeped; // Stubbe
        
        // Enbart skrivbar egenskap som tilldelas true för alla rutor om spelaren 
        // röjer en minerad ruta 
        public bool GameOver
        { 
            set { } // Stubbe
        }

        // Öka räknaren av minor på intilliggande rutor med 1.
        public void IncrementCloseMineCount() // Stubbe
        {  
        }

        // Försök att flagga rutan. Returnerar false om ogiltigt drag, annars true.
        public bool TryFlag() // Stubbe
        {
            return true;
        }

        // Försök röja rutan. Returnerar false om ogiltigt drag, annars true.
        public bool TrySweep() // Stubbe
        {
            return true;
        }
    }
}
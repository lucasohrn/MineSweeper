using System;
using System.Collections.Generic;
using System.Text;

namespace MineSweeper
{

    struct Board
    {
        private Square[,] board;
        private int flagCount, sweepedCount;

        // Konstruktor som initaliserar ny spelplan
        public Board(string[] args) // Stubbe
        {
            board = null;
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
            return true;
        }

        // försök flaga, returnera false om ogiltigt drag, annars true
        public bool TryFlag(int row, int col)
        {
            return true;
        }

        // skriv ut spelplanen
        public void Print()
        {
            
        }
    }
}
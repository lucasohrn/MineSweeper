using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MineSweeper
{
    enum Bokstäver
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J
    }
    // Typ för minröjspelet. 
    struct MineSweeper
    {
        public Board board;
        public bool quit;
        public bool playerWon;
        public bool gameOver;
        
        // Läs ett nytt kommando från användaren med giltig syntax och 
        // ett känt kommandotecken.
        static private string ReadCommand(string inmatning) // Stubbe
        {
            string val = ReturneraVal(inmatning);
            return val; 
        }

        // Kör spelet efter initering. Metoden returnerar när spelet tar 
        // slut genom att något av följande händer:
        // - Spelaren avslutade spelet med kommandot 'q'.
        // - Spelaren förlorade spelet genom att röja en minerad ruta. 
        // - Spelaren vann spelet genom att alla ej minerade rutor är röjda.
        public void Run(Board board)
        {
            while (!(quit || playerWon || gameOver))
            {
                try
                {
                    int row = 0;
                    int col = 0;
                    string val = null;
                    string inmatning = null;

                    board.Print();
                
                    Console.Write("> ");
                    inmatning = Console.ReadLine();
                    inmatning = inmatning.ToUpper();

                    if (inmatning.Length == 4)
                    {
                        val = ReadCommand(inmatning);

                        string temp = ReturneraRad(inmatning);
                        col = ReturneraColumn(inmatning);
                        
                        Bokstäver bokstäver = (Bokstäver)Enum.Parse(typeof(Bokstäver), temp);
                        row = Convert.ToInt32(bokstäver);

                    }

                    else if (inmatning == "Q")
                    {
                        quit = true;
                    }
                
                    else
                    {
                        throw new Exception("SYNTAX ERROR");
                    }

                    if (val == "R")
                    {
                        board.TrySweep(row, col);
                    }

                    else if (val == "F")
                    {
                        board.TryFlag(row, col);
                    }

                    if (gameOver)
                    {
                        
                    }

                    else if (board.PlayerWon)
                    {
                        playerWon = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n" + ex.Message + "\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }

        

        private static string ReturneraVal(string gissning)
        {
            char temp = gissning[0];
            string val = Convert.ToString(temp);
            val = val.ToUpper();

            return val;
        }

        private static string ReturneraRad(string gissning)
        {
            char temp = gissning[2];
            string rad = Convert.ToString(temp);
            rad = rad.ToUpper();

            if (Regex.IsMatch(rad, @"^[A-Z]+$"))
            {
                return rad;
            }
            else
            {
                throw new Exception("SYNTAX ERROR");
            }
            
        }

        private static int ReturneraColumn(string gissning)
        {
            try
            {
                char temp = gissning[3];
                string tempTvå = Convert.ToString(temp);
                int column = Convert.ToInt32(tempTvå);
                return column;
            }
            catch
            {
                throw new Exception("SYNTAX ERROR");
            }
            
        }
    }
}
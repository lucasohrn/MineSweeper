using System;
using System.Collections.Generic;
using System.Text;

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
        private Board board;
        private bool quit;

        // Konstruktor som initierare ett nytt spel med en slumpmässig spelplan.
        public MineSweeper(string[] args)
        {
            board = new Board(args);
            quit = false;
        }

        // Läs ett nytt kommando från användaren med giltig syntax och 
        // ett känt kommandotecken.
        static private string ReadCommand() // Stubbe
        {
            return null; 
        }

        // Kör spelet efter initering. Metoden returnerar när spelet tar 
        // slut genom att något av följande händer:
        // - Spelaren avslutade spelet med kommandot 'q'.
        // - Spelaren förlorade spelet genom att röja en minerad ruta. 
        // - Spelaren vann spelet genom att alla ej minerade rutor är röjda.
        public void Run() // Stubbe
        {
            while (!(quit || board.PlayerWon || board.GameOver))
            {


                Console.Write("> ");
                        
                        string svar = Console.ReadLine();
                        string val = ReturneraVal(svar);
                        string temp = ReturneraRad(svar);
                        int column = ReturneraColumn(svar);

                        Bokstäver bokstäver = (Bokstäver)Enum.Parse(typeof(Bokstäver), temp);
                        int rad = Convert.ToInt32(bokstäver);
                break;
            }
        }

        public static string ReturneraVal(string gissning)
        {
            char temp = gissning[0];
            string val = Convert.ToString(temp);
            val = val.ToUpper();

            if (val == "R")
                val = "*";
            else if (val == "F")
                val = "F";

            return val;
        }

        public static string ReturneraRad(string gissning)
        {
            char temp = gissning[2];
            string rad = Convert.ToString(temp);
            rad = rad.ToUpper();
            return rad;
        }

        public static int ReturneraColumn(string gissning)
        {
            char temp = gissning[3];
            string tempTvå = Convert.ToString(temp);
            int column = Convert.ToInt32(tempTvå);
            return column;
        }
    }
}
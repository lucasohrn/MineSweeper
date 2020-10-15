using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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
        public bool quit;
        public bool gameIsOver;

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
            while (!(quit || gameIsOver))
            {
                bool checkInput = true;
                board.Print();

                while (checkInput)
                {
                    try
                    {
                        int row = 0;
                        int col = 0;
                        string val = null;
                        string inmatning = null;



                        Console.Write("\n> ");
                        inmatning = Console.ReadLine();
                        inmatning = inmatning.ToUpper();

                        if (inmatning.Length == 4)
                        {
                            string temp = ReturneraRad(inmatning);
                            col = ReturneraColumn(inmatning);

                            Bokstäver bokstäver = (Bokstäver)Enum.Parse(typeof(Bokstäver), temp);
                            row = Convert.ToInt32(bokstäver);

                            val = ReadCommand(inmatning);

                        }

                        else if (inmatning.Length == 1)
                        {
                            if (inmatning == "Q")
                            {
                                quit = true;
                            }
                            else if (Regex.IsMatch(inmatning, @"^[A-Z]+$"))
                            {
                                throw new Exception("unknown command");
                            }
                            else
                            {
                                throw new Exception("syntax error");
                            }
                        }

                        else if (inmatning.Length < 4 || inmatning.Length > 4)
                        {
                            throw new Exception("syntax error");
                        }

                        else
                        {
                            throw new Exception("unknown command");
                        }

                        if (val == "R")
                        {
                            if (!board.TrySweep(row, col))
                                gameIsOver = true;
                        }

                        else if (val == "F")
                        {
                            board.TryFlag(row, col);
                        }

                        else
                        {
                            if (!quit)
                                throw new Exception("unknown command");
                        }

                        checkInput = false;
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                    }
                }
            }
        }



        private static string ReturneraVal(string gissning)
        {
            try
            {
                char temp = gissning[0];
                string val = Convert.ToString(temp);
                val = val.ToUpper();

                return val;
            }

            catch
            {
                throw new Exception("unknown command");
            }

        }

        private static string ReturneraRad(string gissning)
        {
            char temp = gissning[2];
            string rad = Convert.ToString(temp);
            rad = rad.ToUpper();

            if (Regex.IsMatch(rad, @"^[A-J]+$"))
            {
                return rad;
            }
            else
            {
                throw new Exception("syntax error");
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
                throw new Exception("syntax error");
            }

        }
    }
}
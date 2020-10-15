using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MineSweeper
{
    // Skapar ett enum för bokstäver (raden i brädet)
    // Detta för att kunna konvertera en bokstav till en siffra
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

        // Metod som körs så länge inte:
        // - Spelaren avslutade spelet med kommandot 'q'. Detta kollas med bool variabeln quit
        // - Spelaren förlorade spelet genom att röja en minerad ruta. Detta kollas med bool variabeln gameIsOver
        // - Spelaren vann spelet genom att alla ej minerade rutor är röjda. Detta kollas med bool variabeln gameIsOver
        public void Run(Board board)
        {
            // Så länge spelet inte avslutas, förlorats eller vunnit
            while (!(quit || gameIsOver))
            {
                bool checkInput = true;
                board.Print();

                // Så länge inputen är fel
                while (checkInput)
                {
                    try
                    {
                        // Deklarerar variabler
                        int row = 0;
                        int col = 0;
                        string val = null;
                        string inmatning = null;



                        Console.Write("\n> ");              // Skriv ut
                        inmatning = Console.ReadLine();     // Variabeln inmatning tilldelas användarens inmatning
                        inmatning = inmatning.ToUpper();    // Värdet på variabeln blir till stora bokstäver

                        if (inmatning.Length == 4)  // OM användarens input är 4 karaktärer i längd
                        {
                            string temp = ReturneraRad(inmatning);  // Anropar metoden ReturneraRad() med inmatning som argument. Deklarerar variabeln temp som får returnerings värdet
                            col = ReturneraColumn(inmatning);       // Anropar metoden ReturneraColumn() med inmatning som argument. Deklarerar variabeln col som får returnerings värdet

                            Bokstäver bokstäver = (Bokstäver)Enum.Parse(typeof(Bokstäver), temp); // Med hjälv av Parse konverteras strängen till siffror
                            row = Convert.ToInt32(bokstäver);   // row = den konverterade strängen 

                            val = ReturneraVal(inmatning);  // Anropar metoden ReturneraVal() med inmatning som argument. Deklarerar variabeln val som får returnerings värdet


                        }

                        else if (inmatning.Length == 1)  // ANNARS OM inputen är 1 karaktär
                        {
                            if (inmatning == "Q")           // OM input är = "Q"
                            {
                                quit = true;                    // Avsluta spelet
                            }
                            else if (Regex.IsMatch(inmatning, @"^[A-Z]+$")) // ANNARS OM input är mellan A-Z, stora bokstäver
                            {
                                throw new Exception("unknown command");         // Kastar nytt felmedellande
                            }
                            else                                            // ANNARS
                            {
                                throw new Exception("syntax error");            // Kastar nytt felmedellande
                            }
                        }

                        else if (inmatning.Length < 4 || inmatning.Length > 4)  // ANNARS OM inputen inte är 4 karaktärer i längd
                        {
                            throw new Exception("syntax error");                    // Kastar nytt felmedellande
                        }

                        else                                                    // ANNARS
                        {
                            throw new Exception("unknown command");                 // Kastar nytt felmedellande
                        }

                        if (val == "R")                                         // OM spelaren väljer "R"
                        {
                            if (!board.TrySweep(row, col))                          // OM TrySweep() returnerar false
                                gameIsOver = true;                                      // gameIsOver blir lika med true och spelets while-loop kommer att avslutas
                        }

                        else if (val == "F")                                    // ANNARS OM spelaren väljer "F"  
                        {
                            board.TryFlag(row, col);                                // Anropar metoden TryFlag() med två argument, raden och columen
                        }

                        else                                                    // ANNARS
                        {
                            if (!quit)                                              // OM användaren inte vill avsluta
                                throw new Exception("unknown command");                 // Kastar nytt felmedellande
                        }

                        checkInput = false; // Variabeln checkInput blir falsk om användarens inmatning var av korrekt format 
                    }

                    catch (Exception ex)                        // Fångar alla throws
                    {
                        Console.WriteLine("\n" + ex.Message);   // Skriver ut felmedellandet gällande specifikt fall
                    }
                }
            }
        }


        // Metod som returnerar användarens inputval (röja, flagga)
        private static string ReturneraVal(string inmatning)    // Tar in parametern inmatning som inehåller 4 karaktärer
        {
            try // Testar om inmatning är korrekt
            {
                char temp = inmatning[0];               // Deklarerar variabeln temp av typen char som tilldelas värdet av indexplats 0 i strängen inmatning
                string val = Convert.ToString(temp);    // Deklarerar en ny sträng val som får värdet av temp om konverteringen lyckas

                return val;                             // Returnerar valet (flagga = "F", röja = "R")
            }

            catch
            {
                throw new Exception("unknown command"); // Kastar nytt felmedellande
            }

        }

        // Metod som returnerar användarens input rad (A-J)
        private static string ReturneraRad(string inmatning) // Tar in parametern inmatning som inehåller 4 karaktärer
        {
            char temp = inmatning[2];                   // Deklarerar variabeln temp av typen char som tilldelas värdet av indexplats 2 i strängen inmatning
            string rad = Convert.ToString(temp);        // Deklarerar en ny sträng rad som får värdet av temp om konverteringen lyckas 

            if (Regex.IsMatch(rad, @"^[A-J]+$"))        // Om raden är inom A-J. Lösning för att endast de bokstäverna som finns kan användas
            {
                return rad;                             // Returnerar valet (A-J)
            }
            else                                        // ANNARS
            {
                throw new Exception("syntax error");    // Kastar nytt felmedellande
            }

        }
        // Metod som returnerar användarens input column (0-9)
        private static int ReturneraColumn(string inmatning)    // Tar in parametern inmatning som innehåller 4 karaktärer
        {
            try // Testar om inmatning är korrekt
            {
                char temp = inmatning[3];                       // Deklarerar variabeln temp av typen char som tilldelas värdet av indexplats 3 i strängen inmatning
                string tempTvå = Convert.ToString(temp);        // Deklarerar en ny sträng tempTvå som får värdet av temp om konverteringen lyckas
                int column = Convert.ToInt32(tempTvå);          // Deklarerar en int med namn column som får värdet av tempTvå om konverteringen lyckas
                return column;                                  // Returnerar valet (0-9)
            }
            catch
            {
                throw new Exception("syntax error");            // Kastar nytt felmedellande
            }

        }
    }
}
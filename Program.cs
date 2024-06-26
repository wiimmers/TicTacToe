/*
    Tic Tac Toe project to showcase C# skills and Object Oriented Programming
    Project started and finished on 06/26/2024
    Author: Nick 'wiimmers' Wimmers
    Github: https://github.com/wiimmers
    LinkedIn: https://www.linkedin.com/in/nicholas-wimmers-0165a027b/
*/

using System;

using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;
using TicTacToe;
using System.Security;
using System.Security.Cryptography;
using System.Formats.Asn1;
using System.Runtime.InteropServices;

namespace TicTacToe
{
    class Game
    {
        public static void Main() 
        {
            MainMenu(); 
        }

        public static void MainMenu() 
        {
            char [][] places = [['1', '2', '3'], ['4', '5', '6'], ['7', '8', '9']];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to wiimmers' .NET TicTacToe game!");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n1. Play Game");
            Console.WriteLine("2. Rules"); 
            Console.WriteLine("3. Exit");
            string? s = Console.ReadLine();
            int select;  
            bool success = int.TryParse(s, out select);

            if (success) 
            {
                switch (select)
                {
                    case 1:
                        start(places); 
                        break;
                    case 2:
                        Rules.ruleTypes(places); 
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                
            }
        }

        public static void start(char [][] places)
        {
            var player1 = ("", false);
            var player2 = ("", false); 

            Console.ForegroundColor = ConsoleColor.Cyan; 
            string? xPlayer = null; 
            while (string.IsNullOrWhiteSpace(xPlayer)) {
                Console.WriteLine("Please input the name of the player using 'X'");
                xPlayer = Console.ReadLine()?.Trim();
            }
            player1.Item1 = xPlayer; 

            Console.ForegroundColor = ConsoleColor.Cyan; 
            string? oPlayer = null; 
            while (string.IsNullOrWhiteSpace(oPlayer)) {
                Console.WriteLine("Please input the name of the player using 'O'");
                oPlayer = Console.ReadLine()?.Trim();
            } 
            player2.Item1 = oPlayer;

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Player using 'X' is {xPlayer}");
            Console.WriteLine($"Player using 'O' is {oPlayer}");

            Random rand = new Random();
            bool startingPlayer = rand.Next(2) == 0; 

            if (startingPlayer) 
            {   
                player1.Item2 = true; 
                Console.WriteLine($"{xPlayer} will go first"); 
            }
            else 
            {
                player2.Item2 = true;
                Console.WriteLine($"{oPlayer} will go first");
            }

            play(places, player1, player2);
        }

        static void play(char [][] places, (string, bool) xPlayer, (string, bool) oPlayer) 
        {
            Board.drawBoard(places);
            if (xPlayer.Item2 == true) 
            {
                SelectSpace(places, xPlayer, oPlayer, 'X');   
            }
            else 
            {
                SelectSpace(places, oPlayer, xPlayer, 'O'); 
            }

            if (CheckWinner(places))
            {
                Console.WriteLine("Congrats you win!"); 
                MainMenu(); 
            }
        }

        public static void SelectSpace(char [][] places, (string, bool) playing, (string, bool) off, char playerChar)
        {
            Console.WriteLine($"Select a valid space {playing.Item1}"); 
            string? s = Console.ReadLine(); 
            char space; 
            bool success = char.TryParse(s, out space);

            if (success)
            {
                OccupySpace(places, playerChar, space);
                playing.Item2 = false; 
                off.Item2 = true; 

                if (playerChar == 'X')
                {
                    play(places, playing, off);
                }
                else 
                {
                    play(places, off, playing); 
                }
            }
            else 
            {
                Console.WriteLine("Invalid space selection please, select a number 1-9");
                SelectSpace(places, playing, off, playerChar);
            }
        }

        public static void OccupySpace(char [][] places, char player, int space)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (places[i][j] == space)
                    {
                        places[i][j] = player;                      
                    }
                }
            }
        }

        public static bool CheckWinner(char[][] places)
        {
            // Check rows for a win
            for (int i = 0; i < 3; i++)
            {
                if (places[i][0] == places[i][1] && places[i][1] == places[i][2] && places[i][0] != ' ')
                {
                    return true;
                }
            }

            // Check columns for a win
            for (int j = 0; j < 3; j++)
            {
                if (places[0][j] == places[1][j] && places[1][j] == places[2][j] && places[0][j] != ' ')
                {
                    return true;
                }
            }

            // Check diagonals for a win
            if (places[0][0] == places[1][1] && places[1][1] == places[2][2] && places[0][0] != ' ')
            {
                return true;
            }

            if (places[0][2] == places[1][1] && places[1][1] == places[2][0] && places[0][2] != ' ')
            {
                return true;
            }

            return false;
        }
    }

    class Rules 
    {
        public static void ruleTypes(char [][] places) 
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Do you want to learn the rules for this version of Tic Tac Toe, or the global rules of Tic Tac Toe?\n");
            
            Console.ForegroundColor = ConsoleColor.White; 
            Console.WriteLine("1. Global Rules");
            Console.WriteLine("2. This game"); 
            Console.WriteLine("3. Main Menu");
            string? s = Console.ReadLine();
            int select;  
            bool success = int.TryParse(s, out select);

            if (success) 
            {
                switch (select)
                {
                    case 1:
                        globalRules(); 
                        break;
                    case 2:
                        thisGame(places); 
                        break;
                    case 3: 
                        Game.MainMenu();
                        break;
                    default:
                        break;
                }
                
            }

        }
        static void thisGame(char [][] places)
        {
            Console.WriteLine("\nEach space has a corresponding number attached to it");         

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j != 1)
                    {
                        Console.Write("  " + places[i][j] + "  ");
                    }
                    else 
                    {
                        Console.Write("|  " + places[i][j] + "  |");
                    }
                    
                }
                Console.WriteLine("");
                if (i != 2) {
                    Console.WriteLine("_____|_____|_____");
                }
            }
            Console.WriteLine("     |     |     ");

            Console.WriteLine("The player will select the space by entering the corresponding number, and their letter ('X' or 'O') will be played in that space");

            Game.MainMenu();
        }

        static void globalRules()
        {
            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("I'm surprised you haven't played Tic Tac Toe before but I'd be glad to explain!\n");
            Console.WriteLine("Each player is assigned an 'X' or an 'O'.");
            Console.WriteLine("Then, each player takes turns picking one of the nine places and placing their 'X' or 'O' in said space.");
            Console.WriteLine("A winner is declared when a player has gotten 3 of their letters in a row.");
            
            Console.ForegroundColor = ConsoleColor.White;

            Game.MainMenu();
        }
    }
    class Board
    {
        public static void drawBoard(char [][] places)
        {   
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j != 1)
                    {
                        Console.Write("  " + places[i][j] + "  ");
                    }
                    else 
                    {
                        Console.Write("|  " + places[i][j] + "  |");
                    }
                    
                }

                Console.WriteLine("");

                if (i != 2) {
                    Console.WriteLine("_____|_____|_____");
                }

            }

            Console.WriteLine("     |     |     ");
        }
    }
}
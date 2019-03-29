using System;
using System.Collections;
using System.Collections.Generic;
using TreasureMap.DAL;

namespace TreasureMap
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ToContinue = true;
            while (ToContinue)
            {
                var choice = PlayOrNot();
                switch (choice)
                {
                    case "1":
                        DisplayData();
                        break;
                    case "q":
                    case "Q":
                        ToContinue = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide, l'application va fermer...");
                        ToContinue = false;
                        break;
                }
            }
        }

       

        ///<summary>
        ///Display the entrance page with a menu
        ///</summary>
        ///<returns>Return gamer choice</returns>
        static string PlayOrNot()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("BIENVENUE DANS LA CARTE AU TRESOR\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Voulez-vous jouer à la carte au trésor ?");
            Console.WriteLine("1. Oui");
            Console.WriteLine("Q. Non et je veux quitter le jeu");
            Console.WriteLine("\nVotre choix: ");

            return Console.ReadLine();
        }

        static void DisplayData()
        {
            Console.Clear();
            Console.WriteLine("DONNEES DU FICHIER D'ENTREE\n");
        }
    }
}

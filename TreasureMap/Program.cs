using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreasureMap.Business;
using TreasureMap.DAL;

namespace TreasureMap
{
    class Program
    {
        static IServiceData service = new ServiceData();

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
            Console.WriteLine("BIENVENUE DANS LA CARTE AUX TRESORS\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Voulez-vous jouer à la carte aux trésors ?");
            Console.WriteLine("1. Oui");
            Console.WriteLine("Q. Non et je veux quitter le jeu");
            Console.WriteLine("\nVotre choix: ");

            return Console.ReadLine();
        }

        /// <summary>
        /// Display the data from the entrance file
        /// </summary>

        static void DisplayData()
        {
            Console.Clear();
            Console.WriteLine("DONNEES DU FICHIER D'ENTREE\n");

            DisplayMap(service.GetMap());
            DisplayMountainList(service.GetMountains());
            DisplayTreasureList(service.GetTreasures());
            DisplayAdventurerList(service.GetAdventurers());
            Console.WriteLine();
            Console.WriteLine("Faire partir les aventuriers à la recherche des trésors (O/N) ?");
            var adventureOrNot = Console.ReadLine();
            switch(adventureOrNot)
            {
                case "O":
                case "o":
                    //AdventurersOnTheGo();
                    break;
                case "N":
                case "n":
                    BackToTheMenu();
                    break;
                default:
                    Console.WriteLine("Choix invalide, l'application va fermer...");
                    break;
            }
            Console.ReadKey();
        }

        

        static void DisplayMap(Map map)
        {
            Console.WriteLine($"La carte fait {map.WidthBoxesNumber} cases en largeur " +
                $"et {map.HeightBoxesNumber} cases en hauteur");
            Console.WriteLine();
        }

        static void DisplayMountainList(IEnumerable<Mountain>MountainList)
        {
            Console.WriteLine("La liste des montagnes est la suivante :");
            foreach (var mountain in MountainList)
            {
                Console.WriteLine($"Montagne avec les coordonnées ({mountain.MountainHorizontalAxis},{mountain.MountainVerticalAxis})");
            }
            Console.WriteLine();
        }

        static void DisplayTreasureList(IEnumerable<Treasure>TreasureList)
        {
            Console.WriteLine("La liste des trésors est la suivante :");
            foreach(var treasure in TreasureList)
            {
                Console.WriteLine($"{treasure.TreasureNumber} trésor(s) avec les coordonnées " +
                    $"({treasure.TreasureHorizontalAxis},{treasure.TreasureVerticalAxis})");
            }
            Console.WriteLine();
        }

        static void DisplayAdventurerList(IEnumerable<Adventurer>AdventurerList)
        {
            Console.WriteLine($"{AdventurerList.Count()} Chercheur(s) de trésors en compétition:");
            foreach (var adventurer in AdventurerList)
            {
                Console.WriteLine($"{adventurer.Name} part des coordonnées " +
                    $"({adventurer.AdventurerHorizontalAxis},{adventurer.AdventurerVerticalAxis})" +
                    $" en direction de {adventurer.Orientation}");
            }
            Console.WriteLine();
        }

        private static void BackToTheMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>Appuyer sur une touche pour revenir au menu...");
            Console.ReadKey();
        }

        //private static void AdventurersOnTheGo(IEnumerable<Adventurer>AdventurerList)
        //{
        //    foreach (var adventurer in AdventurerList)
        //    {
        //        var move = adventurer.Movement;
        //        foreach(var letter in move)
        //        {

        //        }
        //    }
        //}




    }
}

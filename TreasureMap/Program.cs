using System;
using System.Collections.Generic;
using System.Linq;
using TreasureMap.Business;
using TreasureMap.DAL;

namespace TreasureMap
{
    class Program
    {
        static IServiceData service = new ServiceData();

        public static bool ToMove { get; set; }

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
            switch (adventureOrNot)
            {
                case "O":
                case "o":
                    AdventurersOnTheGo(service.GetAdventurers());
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

        static void DisplayMountainList(IEnumerable<Mountain> MountainList)
        {
            Console.WriteLine("La liste des montagnes est la suivante :");
            foreach (var mountain in MountainList)
            {
                Console.WriteLine($"Montagne avec les coordonnées ({mountain.MountainHorizontalAxis},{mountain.MountainVerticalAxis})");
            }
            Console.WriteLine();
        }

        static void DisplayTreasureList(IEnumerable<Treasure> TreasureList)
        {
            Console.WriteLine("La liste des trésors est la suivante :");
            foreach (var treasure in TreasureList)
            {
                Console.WriteLine($"{treasure.TreasureNumber} trésor(s) avec les coordonnées " +
                    $"({treasure.TreasureHorizontalAxis},{treasure.TreasureVerticalAxis})");
            }
            Console.WriteLine();
        }

        static void DisplayAdventurerList(IEnumerable<Adventurer> AdventurerList)
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


        private static void AdventurersOnTheGo(IEnumerable<Adventurer> AdventurerList)
        {
            //Determination of the maximum movement number
            int maxMovementNumber = 0;
            foreach (var adventurer in AdventurerList)
            {
                var movementNumber = adventurer.Movement.Length;
                if (movementNumber > maxMovementNumber)
                {
                    maxMovementNumber = movementNumber;
                }
            }
            Console.WriteLine(maxMovementNumber);


            for (int i = 0; i < maxMovementNumber; i++)
            {
                foreach (var adventurer in AdventurerList)
                {
                    if (i < adventurer.MovementNumber)
                    {
                        int hAxis = adventurer.AdventurerHorizontalAxis;
                        int vAxis = adventurer.AdventurerVerticalAxis;

                        if(adventurer.Movement[i] =='A')
                        {
                            NextBox(adventurer);

                            bool ToMove = true;
                            if (adventurer.Orientation =="S")
                            {
                                
                                CheckMountain(service.GetMountains(),hAxis,vAxis);
                                adventurer.AdventurerHorizontalAxis = adventurer.AdventurerHorizontalAxis;
                                adventurer.AdventurerVerticalAxis = adventurer.AdventurerVerticalAxis + 1;
                            }
                        }




                        if ((adventurer.Orientation == "S" || adventurer.Orientation == "N" || adventurer.Orientation == "E" || adventurer.Orientation == "O")
                            && adventurer.Movement[i] == 'A')
                        {
                            //FindMountain(service.GetMountains(), service.GetAdventurers());    
                            //si montagne avec verticalaxis = adventurerVerticalAxis et avec horizontalaxis = adventurerHorizontalAxis
                            adventurer.AdventurerVerticalAxis = adventurer.AdventurerVerticalAxis + 1;
                            adventurer.AdventurerHorizontalAxis = adventurer.AdventurerHorizontalAxis;
                            continue;
                        }

                        Console.WriteLine($"{adventurer.Name} - {adventurer.Movement[i]}");

                        if (adventurer.Orientation == "S" && adventurer.Movement[i] == 'G')
                        {
                            continue;

                        }
                        if (adventurer.Orientation == "S" && adventurer.Movement[i] == 'D')
                        {
                            continue;
                        }
                        if (adventurer.Orientation == "N")
                        {
                            continue;
                        }
                    }
                    else
                        continue;
                }
            }

        }

        private static Array NextBox(Adventurer adventurer)
        {
            int[] xy = new int [] {adventurer.AdventurerHorizontalAxis, adventurer.AdventurerVerticalAxis};
            if (adventurer.Orientation == "E")
            {
                xy[0] = adventurer.AdventurerHorizontalAxis + 1;
            }
            else if (adventurer.Orientation == "O")
            {
                xy[0] = adventurer.AdventurerHorizontalAxis - 1;
            }
            else if (adventurer.Orientation == "S")
            {
                xy[1] = adventurer.AdventurerVerticalAxis + 1;
            }
            else if (adventurer.Orientation == "N")
            {
                xy[1] = adventurer.AdventurerVerticalAxis - 1;
            }
            else
            {
                throw new MessageException("L'orientation doit être : N pour nord, S pour Sud,  ");
            }




            return xy;
        }

        private static bool CheckMountain(IEnumerable<Mountain> MountainList, int hAxis, int vAxis)
        {
            foreach (var mountain in MountainList)
            {
                if (mountain.MountainHorizontalAxis == hAxis && mountain.MountainVerticalAxis)
            }

            
            return ToMove = false ;
        }
    }
}

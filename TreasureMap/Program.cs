using System;
using System.Collections.Generic;
using System.Linq;
using TreasureMap.Business;
using TreasureMap.DAL;

namespace TreasureMap
{
    class Program
    {
        public static IServiceData service = new ServiceData();

        public static bool ToMove { get; set; }

        public static bool Moving { get; set; }

        public static void Main(string[] args)
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

            DataMap myDataMap = new DataMap();

            Map myMap = myDataMap.GetDataMap();

            DisplayMap(myMap);
            DisplayMountainList(myMountains);
            DisplayTreasureList(mytreasures);
            DisplayAdventurerList(myAdventurers);
            Console.WriteLine();
            Console.WriteLine("Faire partir les aventuriers à la recherche des trésors (O/N) ?");
            var adventureOrNot = Console.ReadLine();
            switch (adventureOrNot)
            {
                case "O":
                case "o":
                    AdventurersOnTheGo(myAdventurers, myMap, myMountains, mytreasures);
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


        private static void AdventurersOnTheGo(IEnumerable<Adventurer> AdventurerList, Map myMap, IEnumerable<Mountain> MountainList, IEnumerable<Treasure> TreasureList)
        {
            //Determination of the maximum movement number
            int maxMovementNumber = 0;
            foreach (var adventurer in AdventurerList)
            {
                var movementNumber = adventurer.Movement.Length;
                if (movementNumber > maxMovementNumber)
                {
                    maxMovementNumber = movementNumber;
                    Console.WriteLine($"Séquences initiales:");
                    Console.WriteLine($"{adventurer.Name} - " +
                        $"{adventurer.Orientation} - " +
                        $"{adventurer.Movement} - " +
                        $"{adventurer.MovementNumber}");
                }
            }
            Console.WriteLine($"Nombre de mouvements maximal : {maxMovementNumber}");


            for (int i = 0; i < maxMovementNumber; i++)
            {
                foreach (var adventurer in AdventurerList)
                {
                    if (i < adventurer.MovementNumber)
                    {
                        if (adventurer.Orientation == "S" && adventurer.Movement[i] == 'G')
                        {
                            adventurer.Orientation = "E";
                        }
                        else if (adventurer.Orientation == "S" && adventurer.Movement[i] == 'D')
                        {
                            adventurer.Orientation = "O";
                        }
                        else if (adventurer.Orientation == "N" && adventurer.Movement[i] == 'G')
                        {
                            adventurer.Orientation = "O";
                        }
                        else if (adventurer.Orientation == "N" && adventurer.Movement[i] == 'D')
                        {
                            adventurer.Orientation = "E";
                        }
                        else if (adventurer.Orientation == "E" && adventurer.Movement[i] == 'G')
                        {
                            adventurer.Orientation = "N";
                        }
                        else if (adventurer.Orientation == "E" && adventurer.Movement[i] == 'D')
                        {
                            adventurer.Orientation = "S";
                        }
                        else if (adventurer.Orientation == "O" && adventurer.Movement[i] == 'G')
                        {
                            adventurer.Orientation = "S";
                        }
                        else if (adventurer.Orientation == "O" && adventurer.Movement[i] == 'D')
                        {
                            adventurer.Orientation = "N";
                        }
                        else if (adventurer.Movement[i] == 'A')
                        {
                            if (AllowToMove(adventurer, myMap, MountainList, AdventurerList, NextBox(adventurer)))
                            {
                                if (CheckTreasure(TreasureList, NextBox(adventurer)[0], NextBox(adventurer)[1]) == true)
                                {
                                    adventurer.TreasureFound = adventurer.TreasureFound + 1;
                                    SelectTreasure(TreasureList, NextBox(adventurer)[0], NextBox(adventurer)[1]).TreasureNumber 
                                        = SelectTreasure(TreasureList, NextBox(adventurer)[0], NextBox(adventurer)[1]).TreasureNumber - 1;
                                }
                                adventurer.AdventurerHorizontalAxis = NextBox(adventurer)[0];
                                adventurer.AdventurerVerticalAxis = NextBox(adventurer)[1];
                            }
                        }
                        Console.WriteLine($"Tour n° {i + 1}");
                        Console.WriteLine($"{adventurer.Name} - {adventurer.Movement[i]} - " +
                            $"{adventurer.Orientation} - {adventurer.AdventurerHorizontalAxis} - " +
                            $"{adventurer.AdventurerVerticalAxis} - " +
                            $"Nombre de trésors trouvés : {adventurer.TreasureFound}");
                    }
                    else
                        continue;
                }
            }

        }
        /// <summary>
        /// Method to know the coordinates of the next box where the adventurer will go
        /// </summary>
        /// <param name="adventurer"></param>
        /// <returns>The return is an array of the coordinates of the next box where the adventurer will go</returns>
        private static int[] NextBox(Adventurer adventurer)
        {
            int[] xy = new int[] { adventurer.AdventurerHorizontalAxis, adventurer.AdventurerVerticalAxis };
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
                throw new MessageException($"La valeur {adventurer.Orientation} n'est pas valide.\n" +
                    $"L'orientation doit être N pour Nord, E pour Est, O pour Ouest et S pour Sud.");
            }
            return xy;
        }

        private static bool AllowToMove(Adventurer adventurer, Map myMap,
            IEnumerable<Mountain> MountainList, IEnumerable<Adventurer> AdventurerList, int[] nextBoxCoordinates)
        {
            bool Moving = true;

            //Move not authorized 
            //1. When x<0 ou x>map width ou y<0 ou y>map height
            if (nextBoxCoordinates[0] < 0
                || nextBoxCoordinates[0] > myMap.WidthBoxesNumber - 1
                || nextBoxCoordinates[1] < 0
                || nextBoxCoordinates[1] > myMap.HeightBoxesNumber - 1)
            {
                Moving = false;
            }

            //2. When there is a mountain in the box where the adventurer will go
            if (CheckMountain(MountainList, nextBoxCoordinates[0], nextBoxCoordinates[1]) == true)
            {
                Moving = false;
            }

            //3. When there is an adventurer in the box where the adventurer will go
            if (CheckAdventurer(AdventurerList, nextBoxCoordinates[0], nextBoxCoordinates[1]) == true)
            {
                Moving = false;
            }
            return Moving;
        }

        /// <summary>
        /// Check Mountain checks if there is a mountain in the box where the adventurer will go
        /// </summary>
        /// <param name="MountainList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>Check mountain returns true if there is a mountain in the box where the adventurer will go</returns>
        private static bool CheckMountain(IEnumerable<Mountain> MountainList, int hAxis, int vAxis)
        {
            bool boxWithMountain = false;
            foreach (var mountain in MountainList)
            {
                if ((mountain.MountainHorizontalAxis == hAxis) && (mountain.MountainVerticalAxis == vAxis))
                {
                    boxWithMountain = true;
                }
            }
            return boxWithMountain;
        }

        /// <summary>
        /// Checks if there is an adventurer in the box where the adventurer will go
        /// </summary>
        /// <param name="AdventurerList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>returns true if there is an adventurer in the box where the adventurer will go</returns>
        private static bool CheckAdventurer(IEnumerable<Adventurer> AdventurerList, int hAxis, int vAxis)
        {
            bool boxWithAdventurer = false;
            foreach (var adventurer in AdventurerList)
            {
                if ((adventurer.AdventurerHorizontalAxis == hAxis) && (adventurer.AdventurerVerticalAxis == vAxis))
                {
                    boxWithAdventurer = true;
                }
            }
            return boxWithAdventurer;
        }

        /// <summary>
        /// Checks if there is some treasures in the box where the adventurer will go
        /// </summary>
        /// <param name="TreasureList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>Returns true if there is a treasure in the box where the adventurer will go</returns>
        private static bool CheckTreasure(IEnumerable<Treasure> TreasureList, int hAxis, int vAxis)
        {
            bool boxWithTreasure = false;
            foreach (var treasure in TreasureList)
            {
                if ((treasure.TreasureHorizontalAxis == hAxis) && (treasure.TreasureVerticalAxis == vAxis))
                {
                    boxWithTreasure = true;
                }
            }
            return boxWithTreasure;
        }

        private static Treasure SelectTreasure(IEnumerable<Treasure> TreasureList, int hAxis, int vAxis)
        {
            Treasure selectedTreasure = new Treasure();

            foreach (var treasure in TreasureList)
            {
                if ((treasure.TreasureHorizontalAxis == hAxis) && (treasure.TreasureVerticalAxis == vAxis))
                {
                    selectedTreasure = treasure;
                }
            }
            return selectedTreasure;
        }

    }
}

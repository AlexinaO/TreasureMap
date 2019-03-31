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
        ///Displays the entrance page with a menu
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
        /// Displays the data from the entrance file
        /// </summary>
        static void DisplayData()
        {
            Console.Clear();
            Console.WriteLine("DONNEES DU FICHIER D'ENTREE\n");

            DataMap myDataMap = new DataMap();

            Map myMap = myDataMap.GetDataMap();

            DisplayMap(myMap);
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
                    AdventurersOnTheGo(service.GetAdventurers(), myMap, service.GetMountains(), service.GetTreasures());
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

        /// <summary>
        /// Displays the data for the map
        /// </summary>
        /// <param name="map"></param>
        static void DisplayMap(Map map)
        {
            Console.WriteLine($"La carte fait {map.WidthBoxesNumber} cases en largeur " +
                $"et {map.HeightBoxesNumber} cases en hauteur");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the data for the mountains
        /// </summary>
        /// <param name="MountainList"></param>
        static void DisplayMountainList(IEnumerable<Mountain> MountainList)
        {
            Console.WriteLine("La liste des montagnes est la suivante :");
            foreach (var mountain in MountainList)
            {
                Console.WriteLine($"Montagne avec les coordonnées ({mountain.MountainHorizontalAxis},{mountain.MountainVerticalAxis})");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the data for the treasures
        /// </summary>
        /// <param name="TreasureList"></param>
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

        /// <summary>
        /// Displays the data for the adventurers
        /// </summary>
        /// <param name="AdventurerList"></param>
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

        /// <summary>
        /// Displays a sentence to ask the user to push a key to come back to the menu
        /// </summary>
        private static void BackToTheMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n>Appuyer sur une touche pour revenir au menu...");
            Console.ReadKey();
        }


        /// <summary>
        /// To Move or not the adventurers and write an exit file
        /// </summary>
        /// <param name="AdventurerList"></param>
        /// <param name="myMap"></param>
        /// <param name="MountainList"></param>
        /// <param name="TreasureList"></param>
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


            //Loop for the game
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
                        else
                        {
                            throw new MessageException($"{adventurer.Name} n'a aucun mouvement valide.");
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
            DataMap exitDataMap = new DataMap()
            {
                currentMap = myMap,
                adventurers = AdventurerList.ToList(),
                mountains = MountainList.ToList(),
                treasures = TreasureList.ToList()
            };
            service.CreateExitData(exitDataMap);
            Console.WriteLine("Le fichier de sortie a été enregistré avec succès dans le répertoire suivant:");
            Console.WriteLine("TreasureMap\\bin\\Debug");
            BackToTheMenu();
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

        /// <summary>
        /// Determines if the adventurer can move or not
        /// </summary>
        /// <param name="adventurer"></param>
        /// <param name="myMap"></param>
        /// <param name="MountainList"></param>
        /// <param name="AdventurerList"></param>
        /// <param name="nextBoxCoordinates"></param>
        /// <returns>Returns true if the adventurer can move</returns>
        private static bool AllowToMove(Adventurer adventurer, Map myMap,
            IEnumerable<Mountain> MountainList, IEnumerable<Adventurer> AdventurerList, int[] nextBoxCoordinates)
        {
            bool Moving = true;

            //1. When an adventurer will go out of the map, he can't move
            if (nextBoxCoordinates[0] < 0
                || nextBoxCoordinates[0] > myMap.WidthBoxesNumber - 1
                || nextBoxCoordinates[1] < 0
                || nextBoxCoordinates[1] > myMap.HeightBoxesNumber - 1)
            {
                Moving = false;
            }

            //2. when a mountain is in the box in which the adventurer has to come
            if (CheckMountain(MountainList, nextBoxCoordinates[0], nextBoxCoordinates[1]) == true)
            {
                Moving = false;
            }

            //3. when an adventurer is in the box in which another adventurer has to come
            if (CheckAdventurer(AdventurerList, nextBoxCoordinates[0], nextBoxCoordinates[1]) == true)
            {
                Moving = false;
            }
            return Moving;
        }

        /// <summary>
        /// Checks if there is a mountain in the box where the adventurer has to come
        /// </summary>
        /// <param name="MountainList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>Returns true if there is a mountain in the box where the adventurer has to come</returns>
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
        /// Checks if there is an adventurer in the box in which another adventurer has to come
        /// </summary>
        /// <param name="AdventurerList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>returns true if there is an adventurer in the box in which another adventurer has to come</returns>
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
        /// Checks if there is some treasures in the box in which the adventurer has to come
        /// </summary>
        /// <param name="TreasureList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>Returns true if there is a treasure in which the adventurer has to come</returns>
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

        /// <summary>
        /// Selects the treasure in the box in which the adventurer has to come
        /// </summary>
        /// <param name="TreasureList"></param>
        /// <param name="hAxis"></param>
        /// <param name="vAxis"></param>
        /// <returns>Returns the selected treasure</returns>
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

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TreasureMap.DAL
{
    public class DataMap : IDataMap
    {
        const string FilePath = "TreasureMap.txt";
        const char FieldSeparator = '-';

        public Map currentMap;
        public List<Adventurer> adventurers;
        public List<Treasure> treasures;
        public List<Mountain> mountains;

        public Map GetDataMap()
        {
            if (this.currentMap == null)
            {
                ReadFile();
            }
            return this.currentMap;
        }

        public IEnumerable<Adventurer> GetAdventurersList()
        {
            if (this.adventurers == null)
            {
                ReadFile();
            }
            return adventurers;
        }

        public IEnumerable<Treasure> GetTreasuresList()
        {
            if (this.treasures == null)
            {
                ReadFile();
            }
            return treasures;
        }

        public IEnumerable<Mountain> GetMountainsList()
        {
            if (this.mountains == null)
            {
                ReadFile();
            }
            return mountains;
        }

        /// <summary>
        /// Method to save the data obtained at the end of the game in a exit txt file
        /// </summary>
        /// <param name="exitDataMap"></param>
        public void SaveExitData(DataMap exitDataMap)
        {
            if (this.currentMap != null)
            {
                this.WriteExitFile();
            }
            else
            {
                throw new MessageException("Il n'y a pas de carte valide donc impossible de créer le fichier de sortie");
            }
        }

        /// <summary>
        /// Method to read the txt file containing the data before the beginning of the game
        /// </summary>
        private void ReadFile()
        {
            this.currentMap = new Map();
            this.adventurers = new List<Adventurer>();
            this.mountains = new List<Mountain>();
            this.treasures = new List<Treasure>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                {
                    foreach (var textLine in lines)
                    {
                        if (textLine.StartsWith("#"))
                        {
                            continue;
                        }
                        else
                        {
                            var fields = textLine.Split(FieldSeparator);

                            if (textLine.StartsWith("C"))
                            {
                                if (this.currentMap.Code == null)
                                {
                                    currentMap.Code = fields[0];
                                    currentMap.WidthBoxesNumber = int.Parse(fields[1]);
                                    currentMap.HeightBoxesNumber = int.Parse(fields[2]);
                                    continue;
                                }
                                else
                                {
                                    throw new MessageException("A Map already exists in the file. Please correct the file to have only one map.");
                                }
                            }
                            if (textLine.StartsWith("M"))
                            {
                                var mountain = new Mountain();
                                mountain.Code = fields[0];
                                mountain.MountainHorizontalAxis = int.Parse(fields[1]);
                                mountain.MountainVerticalAxis = int.Parse(fields[2]);

                                mountains.Add(mountain);
                                continue;
                            }
                            if (textLine.StartsWith("T"))
                            {
                                var treasure = new Treasure();
                                treasure.Code = fields[0];
                                treasure.TreasureHorizontalAxis = int.Parse(fields[1]);
                                treasure.TreasureVerticalAxis = int.Parse(fields[2]);
                                treasure.TreasureNumber = int.Parse(fields[3]);

                                treasures.Add(treasure);
                                continue;
                            }
                            if (textLine.StartsWith("A"))
                            {
                                var adventurer = new Adventurer();
                                adventurer.Code = fields[0];
                                adventurer.Name = fields[1];
                                adventurer.AdventurerHorizontalAxis = int.Parse(fields[2]);
                                adventurer.AdventurerVerticalAxis = int.Parse(fields[3]);
                                adventurer.Orientation = fields[4];
                                adventurer.Movement = fields[5];

                                adventurers.Add(adventurer);
                                continue;
                            }
                        }
                    }
                }
            }
            else
            {
                throw new MessageException("Le fichier TreasureMap.txt n'existe pas dans le dossier TreasureMap\\bin\\Debug");
            }
        }

        /// <summary>
        /// Method to write the exit data which will be saved in a txt file
        /// </summary>
        private void WriteExitFile()
        {
            var contentFile = new StringBuilder();
            const string exitFileName = "FICHIER DE SORTIE DE LA CARTE AUX TRESORS";
            const string exitFilePath = "TreasureMapExitFile.txt";
            const string commentaryMap = "# {C comme Carte} - {Nb. de case en largeur} - {Nb. de case en hauteur}";
            const string commentaryMountain = "# {M comme Montagne} - {Axe horizontal} - {Axe vertical}";
            const string commentaryTreasure = "# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}";
            const string commentaryAdventurer = "# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axe vertical} " +
                "- {Orientation} - {Nb.trésors ramassés}";

            contentFile.AppendLine(exitFileName);

            //Write lines for the map
            contentFile.AppendLine(commentaryMap);
            contentFile.AppendLine(string.Join(FieldSeparator.ToString(),
                currentMap.Code,
                currentMap.WidthBoxesNumber,
                currentMap.HeightBoxesNumber));

            //Write lines for the mountains
            contentFile.AppendLine(commentaryMountain);
            foreach (var mountain in this.mountains)
            {
                contentFile.AppendLine(string.Join(FieldSeparator.ToString(),
                    mountain.Code,
                    mountain.MountainHorizontalAxis,
                    mountain.MountainVerticalAxis));
            }

            //Write lines for the treasures
            contentFile.AppendLine(commentaryTreasure);
            foreach (var treasure in this.treasures)
            {
                contentFile.AppendLine(string.Join(FieldSeparator.ToString(),
                    treasure.Code,
                    treasure.TreasureHorizontalAxis,
                    treasure.TreasureVerticalAxis,
                    treasure.TreasureNumber));
            }

            //Write lines for the adventurers
            contentFile.AppendLine(commentaryAdventurer);
            foreach (var adventurer in this.adventurers)
            {
                contentFile.AppendLine(string.Join(FieldSeparator.ToString(),
                    adventurer.Code,
                    adventurer.Name,
                    adventurer.AdventurerHorizontalAxis,
                    adventurer.AdventurerVerticalAxis,
                    adventurer.Orientation,
                    adventurer.TreasureFound));
            }
            File.WriteAllText(exitFilePath, contentFile.ToString());
        }
    }
}

using System.Collections.Generic;
using System.IO;

namespace TreasureMap.DAL
{
    public class DataMap : IDataMap
    {
        const string FilePath = "TreasureMap.txt";
        const string FieldSeparator = " - ";

        private Map Map;
        private List<Adventurer> adventurers;
        private List<Treasure> treasures;
        private List<Mountain> mountains;

        public Map GetDataMap()
        {
            if (this.Map == null)
            {
                ReadFile();
            }
            return Map;
        }

        public IEnumerable<Adventurer> GetAdventurersList()
        {
            if (this.adventurers == null)
            {
                ReadFile();
            }
            return adventurers;
        }

        public IEnumerable<Treasure>GetTreasuresList()
        {
            if (this.treasures == null)
            {
                ReadFile();
            }
            return treasures;
        }

        public IEnumerable<Mountain> GetMountainsList()
        {
            if(this.mountains == null)
            {
                ReadFile();
            }
            return mountains;
        }

        public void ExitFile(Map map, Adventurer adventurers, Treasure treasures, Mountain mountains)
        {


        }

        private void ReadFile()
        {
            this.Map = new Map();
            this.adventurers = new List<Adventurer>();
            this.mountains = new List<Mountain>();
            this.treasures = new List<Treasure>();
            if (File.Exists(FilePath))
            {
                using (var reader = new StreamReader(FilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] readText = File.ReadAllLines(FilePath);
                        foreach (var textLine in readText)
                            if (textLine.StartsWith("#"))
                            {
                                continue;
                            }
                            else
                            {
                                var fields = textLine.Split(FieldSeparator);

                                if (textLine.StartsWith("C"))
                                {
                                    if (this.Map == null)
                                    {
                                        var map = new Map();
                                        map.Code = fields[0];
                                        map.WidthBoxesNumber = int.Parse(fields[1]);
                                        map.HeightBoxesNumber = int.Parse(fields[2]);
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
                                }
                                if (textLine.StartsWith("T"))
                                {
                                    var treasure = new Treasure();
                                    treasure.Code = fields[0];
                                    treasure.TreasureHorizontalAxis = int.Parse(fields[1]);
                                    treasure.TreasureVerticalAxis = int.Parse(fields[2]);
                                    treasure.TreasureNumber = int.Parse(fields[3]);

                                    treasures.Add(treasure);
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
                                }
                            }
                    }
                }
            }
        }
    }
}

﻿using System.Collections.Generic;
using System.IO;

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

        public void ExitFile(Map map, Adventurer adventurers, Treasure treasures, Mountain mountains)
        {


        }

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

    }

}

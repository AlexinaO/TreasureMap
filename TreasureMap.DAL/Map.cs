using System.Collections.Generic;

namespace TreasureMap.DAL
{
    public class Map
    {
        const char MapCode = 'C';

        public int WidthBoxesNumber { get; set; }
        public int HeightBoxesNumber { get; set; }

        public List<Adventurer> Adventurers { get; set; }
        public List<Mountain> Moutains { get; set; }
        public List<Treasure> Treasures { get; set; }
    }
}

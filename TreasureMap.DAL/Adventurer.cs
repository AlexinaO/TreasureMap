namespace TreasureMap.DAL
{
    public class Adventurer
    {
        const char AdventurerCode = 'A';
        public string Name { get; set; }
        public int AdventurerHorizontalAxis { get; set; }
        public int AdventurerVerticalAxis { get; set; }
        public string Orientation { get; set; }
        public string Movement { get; set; }
    }
}

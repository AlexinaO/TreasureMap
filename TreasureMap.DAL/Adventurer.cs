namespace TreasureMap.DAL
{
    public class Adventurer
    {
		public string Code { get; set; }
        public string Name { get; set; }
        public int AdventurerHorizontalAxis { get; set; }
        public int AdventurerVerticalAxis { get; set; }
        public string Orientation { get; set; }
        public string Movement { get; set; }
    }
}

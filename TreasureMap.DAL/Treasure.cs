using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.DAL
{
    public class Treasure
    {
		public string Code { get; set; }
        public int TreasureVerticalAxis { get; set; }
        public int TreasureHorizontalAxis { get; set; }
        public int TreasureNumber { get; set; }
    }
}

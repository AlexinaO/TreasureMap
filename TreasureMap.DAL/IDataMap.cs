using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.DAL
{
    public interface IDataMap
    {
        Map GetDataMap();

        IEnumerable<Adventurer> GetAdventurersList();

        IEnumerable<Treasure> GetTreasuresList();

        IEnumerable<Mountain> GetMountainsList();

        void ExitFile(Map map, Adventurer adventurer, Treasure treasure, Mountain mountain);
    }
}

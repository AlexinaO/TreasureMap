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

        /// <summary>
        /// Method to save the data obtained after the game which will be write in a text file
        /// </summary>
        /// <param name="exitDataMap"></param>
        void SaveExitData (DataMap exitDataMap);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.DAL;

namespace TreasureMap.Business
{
    public class ServiceData : IServiceData
    {
        private readonly IDataMap data;

        public ServiceData()
        {
            this.data = new DataMap();
        }

        public Map GetMap()
        {
            return this.data.GetDataMap();
        }

        public IEnumerable<Adventurer> GetAdventurers()
        {
            return this.data.GetAdventurersList();
        }

        public IEnumerable<Treasure> GetTreasures()
        {
            return this.data.GetTreasuresList();
        }

        public IEnumerable<Mountain> GetMountains()
        {
            return this.data.GetMountainsList();
        }

        /// <summary>
        /// Method to create the data which will be write in the exit txt file
        /// </summary>
        /// <param name="exitDataMap"></param>
        public void CreateExitData(DataMap exitDataMap)
        {
            if(exitDataMap.currentMap == null)
            {
                throw new MessageException("Il n'y a pas de carte aux trésors valide.");
            }
            if (exitDataMap.adventurers == null)
            {
                throw new MessageException("Il n'y a pas de liste d'aventuriers valide.");
            }
            if(exitDataMap.treasures == null)
            {
                throw new MessageException("Il n'y a pas de liste de trésors valide " +
                    "donc les aventuriers ne pourront pas récolter de trésors.");
            }
            this.data.SaveExitData(exitDataMap);
        }
    }
}

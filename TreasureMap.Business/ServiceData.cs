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
    }
}

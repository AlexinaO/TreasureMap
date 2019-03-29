using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.DAL;

namespace TreasureMap.Business
{
    public interface IServiceData
    {
        Map GetMap();
        IEnumerable<Treasure> GetTreasures();
        IEnumerable<Adventurer> GetAdventurers();
        IEnumerable<Mountain> GetMountains();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.DAL
{
    public interface IDataMap
    {
        IEnumerable<Map> GetData();

        void ExitFile(Map map);
    }
}

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
    }
}

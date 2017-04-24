using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITADcodes.ServiceModel
{
    public class Selection
    {
        public string location { get; set; }
        public string type { get; set; }
        public int radius { get; set; }
        public string transport { get; set; }
        public int foodIndex { get; set; }
        public int typeIndex { get; set; }

        public Selection(string location,string type,int radius,string transport,int foodIndex,int typeIndex)
        {
            this.location = location;
            this.type = type;
            this.radius = radius;
            this.transport = transport;
            this.typeIndex = typeIndex;
            this.foodIndex = foodIndex;
        }
    }
}

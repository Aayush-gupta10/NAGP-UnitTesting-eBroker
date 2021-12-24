using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker_App_ControllerLayer.DTOs
{
    public class SellEquities
    {
        public int TraderID { get; set; }

        public int EquityID { get; set; }

        public double NumberofEquity { get; set; }
    }
}

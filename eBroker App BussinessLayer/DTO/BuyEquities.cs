using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_BussinessLayer.DTO
{
    public class BuyEquities
    {
        public int TraderID { get; set; }

        public int EquityID { get; set; }

        public double NumberofEquity { get; set; }

        public DateTime RequestedDateTime { get; set; }
    }
}

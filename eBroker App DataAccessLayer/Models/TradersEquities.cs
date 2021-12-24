using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_DataAccessLayer.Models
{
    public class TradersEquities
    {

        public int TradersEquitiesID { get; set; }
        public int TraderID { get; set; }

        public int EquityID { get; set; }

        public double NumberofEquity { get; set; }


    }
}

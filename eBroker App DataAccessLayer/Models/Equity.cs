using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_DataAccessLayer.Models
{
    public class Equity
    {
        public int EquityID { get; set; }

        public string EquityName { get; set; }

        public double EquityValue { get; set; }

        public virtual ICollection<TradersEquities> tradersEquities { get; set; }


    }
}

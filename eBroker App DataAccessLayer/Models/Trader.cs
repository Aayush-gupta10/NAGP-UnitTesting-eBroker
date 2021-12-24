using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_DataAccessLayer.Models
{
    public class Trader
    {
        public int TraderID { get; set; }

        public string Name { get; set; }

        public double Fund { get; set; }

        public virtual ICollection<TradersEquities> tradersEquities { get; set; }
    }
}

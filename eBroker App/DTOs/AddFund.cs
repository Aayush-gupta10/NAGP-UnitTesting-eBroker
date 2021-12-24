using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker_App_ControllerLayer.DTOs
{
    public class AddFund
    {
        public int TraderID { get; set; }

        public double FundAmount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace eBroker_App_DataAccessLayer
{
    public class TraderEquityDBContext: DbContext
    {
        public TraderEquityDBContext(DbContextOptions<TraderEquityDBContext> options)
            : base(options) { }

        public DbSet<Models.Trader> Trader { get; set; }

        public DbSet<Models.Equity> Equity { get; set; }

        public DbSet<Models.TradersEquities>  TradersEquities { get; set; }
    }
}

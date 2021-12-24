using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace eBroker_App_DataAccessLayer
{
    [ExcludeFromCodeCoverage]
    public class eBroker_Data
    {
            public static void Initialize(IServiceProvider serviceProvider)
            {
                using (var context = new TraderEquityDBContext(
                    serviceProvider.GetRequiredService<DbContextOptions<TraderEquityDBContext>>()))
                {
                    // Look for any board games.
                    if (context.Trader.Any())
                    {
                        return;   // Data was already seeded
                    }

                context.Trader.AddRange(
                    new Models.Trader
                    {
                        TraderID = 1,
                        Fund = 20000,
                        Name = "Trader 1"
                    },
                    new Models.Trader
                    {
                        TraderID = 2,
                        Fund = 20000,
                        Name = "Trader 2"
                    }, new Models.Trader
                    {
                        TraderID = 3,
                        Fund = 20000,
                        Name = "Trader 3"
                    }, new Models.Trader
                    {
                        TraderID = 4,
                        Fund = 20000,
                        Name = "Trader 4"
                    });

                context.Equity.AddRange(
                    new Models.Equity
                    {
                        EquityID = 1,
                        EquityName = "HDFC",
                        EquityValue = 100
                    },
                    new Models.Equity
                    {
                        EquityID = 2,
                        EquityName = "Axis",
                        EquityValue = 500
                    }, new Models.Equity
                    {
                        EquityID = 3,
                        EquityName = "SBI",
                        EquityValue = 1000
                    }, new Models.Equity
                    {
                        EquityID = 4,
                        EquityName = "BoB",
                        EquityValue = 2000
                    });

                context.TradersEquities.AddRange(
                    new Models.TradersEquities
                    {
                        TradersEquitiesID = 1,
                        TraderID = 1,
                        EquityID = 1,
                        NumberofEquity = 5
                    },
                    new Models.TradersEquities
                    {
                        TradersEquitiesID = 2,
                        TraderID = 1,
                        EquityID = 1,
                        NumberofEquity = 5
                    }, new Models.TradersEquities
                    {
                        TradersEquitiesID = 3,
                        TraderID = 1,
                        EquityID = 1,
                        NumberofEquity = 5
                    }, new Models.TradersEquities
                    {
                        TradersEquitiesID = 4,
                        TraderID = 1,
                        EquityID = 1,
                        NumberofEquity = 5
                    }) ;

                context.SaveChanges();
                }
            }
    }
}

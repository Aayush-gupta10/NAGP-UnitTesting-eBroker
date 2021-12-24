using eBroker_App_BussinessLayer.DTO;
using eBroker_App_DataAccessLayer;
using eBroker_App_DataAccessLayer.Models;
using eBroker_App_DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker_App_BussinessLayer
{
    public class eBrokerService : IeBrokerService
    {
        private readonly IRepository<Trader> _traderrepository;
        private readonly IRepository<Equity> _equityrepository;
        private readonly IRepository<TradersEquities> _traderEquitiesrepository;
   
        public eBrokerService(IRepository<Trader> traderrepository , IRepository<Equity> equityrepository, IRepository<TradersEquities> traderEquitiesrepository)
        {
            _traderrepository = traderrepository;
            _equityrepository = equityrepository;
            _traderEquitiesrepository = traderEquitiesrepository;
        }
        public bool BuyEquities(BuyEquities buyEquities)
        {
            // Check for correct time and day
            if (!Helper.checkDateAndTiime(buyEquities.RequestedDateTime)) 
                return false;

            var equity = _equityrepository.Get(buyEquities.EquityID);
            var trader = _traderrepository.Get(buyEquities.TraderID);

            if (equity == null || trader == null) 
                return false;

            if (trader.Fund < equity.EquityValue * buyEquities.NumberofEquity) 
                return false;

            var allTraderEquity = _traderEquitiesrepository.GetAll().ToList();
            var traderEquity = allTraderEquity.Where(x => x.TraderID == buyEquities.TraderID && x.EquityID == buyEquities.EquityID).FirstOrDefault();
            if (traderEquity == null)
            {
                var maxId = allTraderEquity.Max(x => x.TradersEquitiesID);
                var traderEquityData = new TradersEquities { TraderID = buyEquities.TraderID, EquityID = buyEquities.EquityID, NumberofEquity = buyEquities.NumberofEquity, TradersEquitiesID = maxId + 1 };
                _traderEquitiesrepository.Add(traderEquityData);
            }
            else
            {
                traderEquity.NumberofEquity += buyEquities.NumberofEquity;
                _traderEquitiesrepository.Update(traderEquity, traderEquity.TradersEquitiesID);
            }

            var sumToBeDeducted = equity.EquityValue * buyEquities.NumberofEquity;
            trader.Fund -= sumToBeDeducted;
            _traderrepository.Update(trader, trader.TraderID);

            return true;

        }

        public bool SellEquities(SellEquities request)
        {
            // Check for correct time and day
            if (!Helper.checkDateAndTiime(request.RequestedDateTime)) return false;

            var equity = _equityrepository.Get(request.EquityID);
            var trader = _traderrepository.Get(request.TraderID);

            if (equity == null || trader == null) return false;

            var allTraderEquity = _traderEquitiesrepository.GetAll().ToList();
            var traderEquity = allTraderEquity.Where(x => x.TraderID == request.TraderID && x.EquityID == request.EquityID).FirstOrDefault();

            if (traderEquity == null || traderEquity.NumberofEquity < request.NumberofEquity) return false;

            var sumToBeAdded = equity.EquityValue * request.NumberofEquity;
            var deduction = sumToBeAdded * 0.0005 > 20 ? sumToBeAdded * 0.0005 : 20;
            sumToBeAdded -= deduction;
            trader.Fund += sumToBeAdded;

            if (trader.Fund < 0) 
                return false;

            traderEquity.NumberofEquity -= request.NumberofEquity;
            _traderEquitiesrepository.Update(traderEquity, traderEquity.TradersEquitiesID);

            
            _traderrepository.Update(trader, trader.TraderID);
            return true;
        }

        public bool AddFund(AddFund request)
        {
            var trader = _traderrepository.Get(request.TraderID);

            if (trader == null) return false;

            if (request.FundAmount > 100000) request.FundAmount -= request.FundAmount * 0.0005;

            trader.Fund += request.FundAmount;
            _traderrepository.Update(trader, trader.TraderID);
            return true;
        }

    }
}

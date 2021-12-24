using eBroker_App_BussinessLayer;
using eBroker_App_BussinessLayer.DTO;
using eBroker_App_DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eBroker_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraderController : ControllerBase
    {
        private IeBrokerService _ebrokerService;
        public TraderController(IeBrokerService ebrokerService)
        {
            _ebrokerService = ebrokerService;

        }

        //[HttpGet("get-all-traders-equities")]
        //public List<Trader> GetAllTradersEquities()
        //{

        //    return _ebrokerService.GetAllTraderWithEquities();

        //}

        [HttpPost("buy-equities")]

        public string BuyEquities([FromBody] eBroker_App_ControllerLayer.DTOs.BuyEquities buyEquities)
        {
            BuyEquities buyEquities1 = new BuyEquities
            {
                EquityID = buyEquities.EquityID,
                NumberofEquity = buyEquities.NumberofEquity,
                TraderID = buyEquities.TraderID
            };

            bool res = _ebrokerService.BuyEquities(buyEquities1);
            string output = "";
            if (res)
            {
                output = "Trader buys equities successfully";
            }
            else
            {
                output = "Trader failed to buys equities";
            }
            return output;
        }

        [HttpPost("sell-equities")]

        public string SellEquities([FromBody] eBroker_App_ControllerLayer.DTOs.SellEquities sellEquities)
        {
            SellEquities sellEquities1 = new SellEquities
            {
                EquityID = sellEquities.EquityID,
                NumberofEquity = sellEquities.NumberofEquity,
                TraderID = sellEquities.TraderID
            };

            bool res = _ebrokerService.SellEquities(sellEquities1);
            string output = "";
            if (res)
            {
                output = "Trader sells equities successfully";
            }
            else
            {
                output = "Trader failed to sell equities";
            }
            return output;
        }

        [HttpPost("add-fund")]

        public string AddFund([FromBody] eBroker_App_ControllerLayer.DTOs.AddFund addFund)
        {
            AddFund addFund1 = new AddFund
            {
                TraderID = addFund.TraderID,
                FundAmount = addFund.FundAmount
            };

            bool res = _ebrokerService.AddFund(addFund1);
            string output = "";
            if (res)
            {
                output = "Fund Added successfully";
            }
            else
            {
                output = "Funds does not added successfully";
            }
            return output;
        }


    }
}

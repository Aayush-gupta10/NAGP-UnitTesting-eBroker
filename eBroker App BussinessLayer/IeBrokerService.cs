using eBroker_App_BussinessLayer.DTO;
using eBroker_App_DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBroker_App_BussinessLayer
{
    public interface IeBrokerService
    {
        bool BuyEquities(DTO.BuyEquities buyEntities);
        bool SellEquities(SellEquities request);

        bool AddFund(AddFund request);

      
    }
}

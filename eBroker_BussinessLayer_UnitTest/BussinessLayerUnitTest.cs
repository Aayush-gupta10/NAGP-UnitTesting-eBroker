using eBroker_App_BussinessLayer;
using eBroker_App_BussinessLayer.DTO;
using eBroker_App_DataAccessLayer.Models;
using eBroker_App_DataAccessLayer.Repository;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace eBroker_BussinessLayer_UnitTest
{
    public class BussinessLayerUnitTest
    {
        private readonly Mock<IRepository<Trader>> _traderrepository;
        private readonly Mock<IRepository<Equity>> _equityrepository;
        private readonly Mock<IRepository<TradersEquities>> _traderEquitiesrepository;

        private readonly eBrokerService _eBrokerService;

        public BussinessLayerUnitTest()
        {
            _traderrepository = new Mock<IRepository<Trader>>();
            _equityrepository = new Mock<IRepository<Equity>>();
            _traderEquitiesrepository = new Mock<IRepository<TradersEquities>>();

            _eBrokerService = new eBrokerService(_traderrepository.Object, _equityrepository.Object, _traderEquitiesrepository.Object);
        }

        [Fact]
        public void BuyEquities_should_return_false_if_invalid_dateTime()
        {
            // Act + Arrange
            bool res = _eBrokerService.BuyEquities(new BuyEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 25, 10, 0, 0) });

            // Assert
            Assert.True(!res);

        }

        [Fact]
        public void BuyEquities_should_return_false_if_trader_not_exist()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity());
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(null as Trader);

            // Act 
            bool res = _eBrokerService.BuyEquities(new BuyEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 10, 0, 0) });

            // Assert
            Assert.True(!res);
        }

        [Fact]
        public void BuyEquities_should_return_false_if_enquity_not_exist()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(null as Equity);
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader());

            // Act 
            bool res = _eBrokerService.BuyEquities(new BuyEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 10, 0, 0) });

            // Assert

            Assert.True(!res);
        }

        [Fact]
        public void BuyEquities_should_return_false_if_trader_have_insufficient_fund()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 100 });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 50 });

            // Act
            bool res = _eBrokerService.BuyEquities(new BuyEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 10, 0, 0) });

            // Assert

            Assert.True(!res);
        }

        [Fact]
        public void BuyEquities_should_return_true_for_trader_buys_new_equity()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 100, EquityName = "AXIS Mutual Fund" });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 500, Name = "Trader1" });
            _traderEquitiesrepository.Setup(x => x.GetAll()).Returns(new List<TradersEquities>() { new TradersEquities { TraderID = 1, EquityID = 2, NumberofEquity = 1, TradersEquitiesID = 1 } });

            // Act
            bool res = _eBrokerService.BuyEquities(new BuyEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 10, 0, 0) });

            // Assert
            Assert.True(res);
            _traderEquitiesrepository.Verify(x => x.Add(It.IsAny<TradersEquities>()), Times.Once);
            _traderEquitiesrepository.Verify(x => x.Update(It.IsAny<TradersEquities>(), It.IsAny<int>()), Times.Never);
            _traderrepository.Verify(x => x.Update(It.IsAny<Trader>(), It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void BuyEquities_should_return_success_when_trader_buys_exisiting_equity()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 10, EquityName = "AXIS Mutual Fund" });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 500, Name = "B" });
            _traderEquitiesrepository.Setup(x => x.GetAll()).Returns(new List<TradersEquities>() { new TradersEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, TradersEquitiesID = 1 } });

            // Act
            bool res = _eBrokerService.BuyEquities(new BuyEquities{ TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 10, 0, 0) });

            // Assert
            Assert.True(res);
            _traderEquitiesrepository.Verify(x => x.Add(It.IsAny<TradersEquities>()), Times.Never);
            _traderEquitiesrepository.Verify(x => x.Update(It.IsAny<TradersEquities>(), It.IsAny<int>()), Times.Once);
            _traderrepository.Verify(x => x.Update(It.IsAny<Trader>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void SellEquities_should_return_false_for_invalid_dateTime()
        {
            // Act + Arrange
            bool res = _eBrokerService.SellEquities(new SellEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 18, 10, 0, 0) });

            // Assert
            Assert.False(res);
        }

        [Fact]
        public void SellEquities_should_return_error_if_trader_is_invalid()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity());
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(null as Trader);

            // Act 
            bool res = _eBrokerService.SellEquities(new SellEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 10, 0, 0) });

            // Assert
            Assert.False(res);
        }

        [Fact]
        public void SellEquities_should_return_false_if_enquity_is_invalid()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(null as Equity);
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader());

            // Act 
            bool res = _eBrokerService.SellEquities(new SellEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24, 0, 0, 0) });

            // Assert
            Assert.False(res);
        }

        

        [Fact]
        public void SellEquities_should_return_false_if_trader_does_not_have_requested_equity()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 10, EquityName = "ICICI Mutual Fund" });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 500, Name = "Trader1" });
            _traderEquitiesrepository.Setup(x => x.GetAll()).Returns(new List<TradersEquities>() { new TradersEquities { TraderID = 1, EquityID = 2, NumberofEquity = 1, TradersEquitiesID = 1 } });

            // Act
            bool res = _eBrokerService.SellEquities(new SellEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24,10,0,0) });

            // Assert
            Assert.False(res);
            
        }

        [Fact]
        public void SellEquities_should_return_false_when_trader_have_insufficient_equities()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 10, EquityName = "ICICI Mutual Fund" });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 500, Name = "Trader1" });
            _traderEquitiesrepository.Setup(x => x.GetAll()).Returns(new List<TradersEquities>() { new TradersEquities { TraderID = 1, EquityID = 1, NumberofEquity = 0.5, TradersEquitiesID = 1 } });

            // Act
            bool res = _eBrokerService.SellEquities(new SellEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24,10,0,0) });

            // Assert
            Assert.False(res);
            
        }

        [Fact]
        public void SellEquities_should_return_false_when_trader_doesnot_have_sufficientfund_after_brokerage_deduction()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 10, EquityName = "ICICI Mutual Fund" });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 5, Name = "Trader1" });
            _traderEquitiesrepository.Setup(x => x.GetAll()).Returns(new List<TradersEquities>() { new TradersEquities { TraderID = 1, EquityID = 1, NumberofEquity = 2, TradersEquitiesID = 1 } });

            // Act
            bool res = _eBrokerService.SellEquities(new SellEquities{ TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24,10,0,0) });

            // Assert
            Assert.False(res);
            
        }

        [Fact]
        public void SellEquities_should_return_true_if_trader_sells_equity_successfully()
        {
            // Arrange
            _equityrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Equity() { EquityID = 1, EquityValue = 100, EquityName = "ICICI Mutual Fund" });
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader { TraderID = 1, Fund = 200, Name = "Trader1" });
            _traderEquitiesrepository.Setup(x => x.GetAll()).Returns(new List<TradersEquities>() { new TradersEquities { TraderID = 1, EquityID = 1, NumberofEquity = 2, TradersEquitiesID = 1 } });

            // Act
            bool res = _eBrokerService.SellEquities(new SellEquities { TraderID = 1, EquityID = 1, NumberofEquity = 1, RequestedDateTime = new DateTime(2021, 12, 24,10,0,0) });

            // Assert
            Assert.True(res);
            _traderEquitiesrepository.Verify(x => x.Update(It.IsAny<TradersEquities>(), It.IsAny<int>()), Times.Once);
            _traderrepository.Verify(x => x.Update(It.IsAny<Trader>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void AddFund_should_return_false_if_trader_not_exist()
        {
            // Arrange
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(null as Trader);

            // Act 
            bool res = _eBrokerService.AddFund(new AddFund { TraderID = 1, FundAmount = 100 });

            // Assert
            Assert.False(res);
            
        }

        [Fact]
        public void AddFund_should_return_true_if_funds_added_successfully()
        {
            // Arrange
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader());

            // Act 
            bool res = _eBrokerService.AddFund(new AddFund { TraderID = 1, FundAmount = 100 });

            // Assert
            Assert.True(res);
            
        }

        [Fact]
        public void AddFund_should_return_true_if_funds_added_after_deducting_brokerage()
        {
            // Arrange
            _traderrepository.Setup(x => x.Get(It.IsAny<int>())).Returns(new Trader());

            // Act 
            bool res = _eBrokerService.AddFund(new AddFund { TraderID = 1, FundAmount = 500000 });

            // Assert
            Assert.True(res);
            
        }
    }
}
    

using eBroker_App.Controllers;
using eBroker_App_BussinessLayer;
using eBroker_App_BussinessLayer.DTO;
using Moq;
using Xunit;

namespace eBroker_App_ControllerLayer_Unit_Test
{
    public class ControllerLayer_UnitTest
    {
        private readonly Mock<IeBrokerService> _eBrokerService;
        private readonly TraderController _controller;
        public ControllerLayer_UnitTest()
        {
            _eBrokerService = new Mock<IeBrokerService>();
            _controller = new TraderController(_eBrokerService.Object);
        }


        [Fact]
        public void BuyEquities_return_Trader_buys_equities_successfully()
        {
            // Arrange
            _eBrokerService.Setup(x => x.BuyEquities(It.IsAny<BuyEquities>())).Returns(true);

            // Act
            var res = _controller.BuyEquities(new eBroker_App_ControllerLayer.DTOs.BuyEquities { EquityID = 1, NumberofEquity = 2, TraderID = 3 });

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Trader buys equities successfully", res);
            _eBrokerService.Verify(x => x.BuyEquities(It.IsAny<BuyEquities>()), Times.Once);
        }

        [Fact]
        public void BuyEquities_return_Trader_failed_to_buys_equities()
        {
            // Arrange
            _eBrokerService.Setup(x => x.BuyEquities(It.IsAny<BuyEquities>())).Returns(false);

            // Act
            var res = _controller.BuyEquities(new eBroker_App_ControllerLayer.DTOs.BuyEquities { EquityID = 1, NumberofEquity = 2, TraderID = 3 });

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Trader failed to buys equities", res);
            _eBrokerService.Verify(x => x.BuyEquities(It.IsAny<BuyEquities>()), Times.Once);
        }

        [Fact]
        public void SellEquities_return_Trader_sells_equities_successfully()
        {
            // Arrange
            _eBrokerService.Setup(x => x.SellEquities(It.IsAny<SellEquities>())).Returns(true);

            // Act
            var res = _controller.SellEquities(new eBroker_App_ControllerLayer.DTOs.SellEquities { EquityID = 1, NumberofEquity = 2, TraderID = 3 });

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Trader sells equities successfully", res);
            _eBrokerService.Verify(x => x.SellEquities(It.IsAny<SellEquities>()), Times.Once);
        }

        [Fact]
        public void SellEquities_return_Trader_failed_to_sell_equities()
        {
            // Arrange
            _eBrokerService.Setup(x => x.SellEquities(It.IsAny<SellEquities>())).Returns(false);

            // Act
            var res = _controller.SellEquities(new eBroker_App_ControllerLayer.DTOs.SellEquities { EquityID = 1, NumberofEquity = 2, TraderID = 3 });

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Trader failed to sell equities", res);
            _eBrokerService.Verify(x => x.SellEquities(It.IsAny<SellEquities>()), Times.Once);
        }

        [Fact]
        public void AddFunds_return_Funds_added_successfully()
        {
            // Arrange
            _eBrokerService.Setup(x => x.AddFund(It.IsAny<AddFund>())).Returns(true);

            // Act
            var res = _controller.AddFund(new eBroker_App_ControllerLayer.DTOs.AddFund {FundAmount = 500, TraderID = 3 });

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Fund Added successfully", res);
            _eBrokerService.Verify(x => x.AddFund(It.IsAny<AddFund>()), Times.Once);
        }

        [Fact]
        public void AddFunds_return_Funds_doesnot_added_successfully()
        {
            // Arrange
            _eBrokerService.Setup(x => x.AddFund(It.IsAny<AddFund>())).Returns(false);

            // Act
            var res = _controller.AddFund(new eBroker_App_ControllerLayer.DTOs.AddFund { FundAmount = 500, TraderID = 3 });

            // Assert
            Assert.NotNull(res);
            Assert.Equal("Funds does not added successfully", res);
            _eBrokerService.Verify(x => x.AddFund(It.IsAny<AddFund>()), Times.Once);
        }
    }
}

using eBroker_App_BussinessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBroker_BussinessLayer_UnitTest
{
    public class HelperMethod_UnitTest
    {
        [Fact]
        public void checkDateAndTiime_should_return_false_if_time_is_before_9()
        {
            // Act + Arrange
            var res = Helper.checkDateAndTiime(new DateTime(2021, 12, 27, 7, 0, 0));

            // Assert
            Assert.False(res);
        }

        [Fact]
        public void checkDateAndTiime_should_return_false_if_time_is_after_3()
        {
            // Act + Arrange
            var res = Helper.checkDateAndTiime(new DateTime(2021, 12, 27, 16, 0, 0));

            // Assert
            Assert.False(res);
        }


        [Fact]
        public void checkDateAndTiime_should_return_false_if_Day_is_Sat()
        {
            // Act + Arrange
            var res = Helper.checkDateAndTiime(new DateTime(2021, 12, 25, 10, 0, 0));

            // Assert
            Assert.False(res);
        }

        [Fact]
        public void checkDateAndTiime_should_return_false_if_Day_is_Sunday()
        {
            // Act + Arrange
            var res = Helper.checkDateAndTiime(new DateTime(2021, 12, 25, 10, 0, 0));

            // Assert
            Assert.False(res);
        }

       
        [Fact]
        public void checkDateAndTiime_should_return_true_If_both_day_and_time_is_correct()
        {
            // Act + Arrange
            var res = Helper.checkDateAndTiime(new DateTime(2021, 12, 27, 12, 0, 0));

            // Assert
            Assert.True(res);
        }
    }
}

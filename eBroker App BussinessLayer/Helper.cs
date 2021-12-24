using System;

namespace eBroker_App_BussinessLayer
{
    public static class Helper
    {
        public static bool checkDateAndTiime(DateTime dateTime)
        {
            return (dateTime.Hour >= 9 && dateTime.Hour <= 14 && dateTime.DayOfWeek != DayOfWeek.Sunday && dateTime.DayOfWeek != DayOfWeek.Saturday);
        }

    }
}

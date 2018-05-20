using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Helpers
{
    public static class DeadLineExtensions
    {
        public static int GetDeadLine(this DateTime dateTime)
        {
            var currentDate = DateTime.UtcNow;
            var remainingDay = dateTime.Subtract(currentDate);

            return remainingDay.Days;
        }
    }
}

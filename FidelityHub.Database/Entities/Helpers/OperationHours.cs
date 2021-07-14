using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FidelityHub.Database.Entities.Helpers
{
    public static class OperationHours
    {
        private enum Days
        {
            SUNDAY = 1,
            MONDAY = 2,
            TUESDAY = 3,
            WEDNESDAY = 4,
            THURSDAY = 5,
            FRIDAY = 6,
            SATURDAY = 7
        }

        //ex: 1 0800 1900 2 0800 1900 3 0800 1900 4 0800 1900 5 0800 1900 6 0800 1900 7 0800 1900
        // opening: 10800 20800 30800 40800 50800 60800 70800

        public static int GetDBClosingHours(int hours)
        {
            string strhours = hours.ToString();
            int openingHours = 0;
            int skip = 5;
            int take = 4;
            int daySkip = 9;

            for (var i = 0; i < 7; i++)
            {
                if (i == 0)
                {
                    openingHours = Int32.Parse(new string(strhours.Take(1).ToArray()));
                    openingHours += Int32.Parse(new string(strhours.Skip(skip).Take(take).ToArray()));
                }
                else
                {
                    openingHours += Int32.Parse(new string(strhours.Skip(daySkip * i).Take(1).ToArray()));
                    openingHours += Int32.Parse(new string(strhours.Skip((daySkip * (i + 1)) - take).Take(take).ToArray()));
                }
            }

            return openingHours;
        }

        public static int GetDBOpeningHours(int hours)
        {
            string strhours = hours.ToString();
            int openingHours = 0;
            int take = 5;
            int skip = 9;

            for(var i = 1; i <= 7; i++)
            {
                if (i == 1)
                {
                    openingHours = Int32.Parse(new string(strhours.Take(take).ToArray()));
                }
                else
                {
                    openingHours += Int32.Parse(new string(strhours.Skip(skip * i).Take(take).ToArray()));
                }
            }

            return openingHours;
        }
    }
}

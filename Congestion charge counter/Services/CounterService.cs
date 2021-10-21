using Congestion_charge_counter.Models;
using System;

namespace Congestion_charge_counter.Services
{
    public class CounterService
    {

        public CounterModel FeeCounter(CounterModel counter, FeeModel fees)
        {
            var StartDate = counter.StartDate;
            while (counter.EndDate > StartDate)
            {
                if (StartDate.DayOfWeek == DayOfWeek.Saturday || StartDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    StartDate = StartDate.AddDays(1);
                }
                if (StartDate.TimeOfDay.Hours >= 0 && StartDate.TimeOfDay.TotalHours < 7)
                {
                    StartDate = StartDate.AddHours(7);
                }
                if (StartDate.TimeOfDay.Hours >= 7 && StartDate.TimeOfDay.Hours < 12)
                {
                    StartDate = StartDate.AddMinutes(1);
                    counter.AM_rate += fees.AM_fee;
                    counter.TotalAMTime = counter.TotalAMTime.AddMinutes(1);
                }
                if (StartDate.TimeOfDay.Hours >= 12 && StartDate.TimeOfDay.TotalHours < 19)
                {
                    StartDate = StartDate.AddMinutes(1);
                    counter.PM_rate += fees.PM_fee;
                    counter.TotalPMTime = counter.TotalPMTime.AddMinutes(1);
                }
                if (StartDate.TimeOfDay.Hours >= 19 && StartDate.TimeOfDay.TotalHours < 24)
                {
                    StartDate = StartDate.AddHours(5);
                }

            }
            return counter;
        }
        public CounterModel FeeRounding(CounterModel counter)
        {
            counter.AM_rate = Math.Round(counter.AM_rate, 1, MidpointRounding.AwayFromZero);
            counter.PM_rate = Math.Round(counter.PM_rate, 1, MidpointRounding.AwayFromZero);
            return counter;
        }

        public double TotalSum(double AMfee, double PMfee)
        {
            double fullSum = AMfee + PMfee;
            fullSum = Math.Round(fullSum, 1, MidpointRounding.ToZero);
            return fullSum;
        }
    }
}

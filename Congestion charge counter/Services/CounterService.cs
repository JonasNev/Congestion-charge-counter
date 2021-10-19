using Congestion_charge_counter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congestion_charge_counter.Services
{
    public class CounterService
    {
        public double CountMinutes(CounterModel model)
        {
            var minutes = (model.EndDate - model.StartDate).TotalMinutes;
            var minutesFromDate = (model.EndDate - model.StartDate).TotalMinutes;
            var returnminutes = minutes + minutesFromDate;
            return returnminutes;
        }
    }
}
